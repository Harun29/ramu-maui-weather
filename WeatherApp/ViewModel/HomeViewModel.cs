using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherAPI.Standard.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModel;

[QueryProperty(nameof(LocationName), "location")]
public partial class HomeViewModel : BaseViewModel
{
    IConnectivityService _connectivityService;
    ILocationService _locationService;
    IWeatherService _weatherService;
    IAlertService _alertService;

    [ObservableProperty]
    string locationName;
    [ObservableProperty]
    bool isRefreshing;
    [ObservableProperty]
    WeatherAPI.Standard.Models.Location currentLocation;
    [ObservableProperty]
    Current currentWeather;
    [ObservableProperty]
    ObservableCollection<Hour> weatherForecastHours;
    [ObservableProperty]
    ObservableCollection<Forecastday> weatherForecastDays;
    [ObservableProperty]
    double currentTemp;
    [ObservableProperty]
    string buttonText = "°C";
    [ObservableProperty]
    bool isCelsius = true;
    [ObservableProperty]
    bool isFerenheit = false;
    [ObservableProperty]
    string backgroundUrl;
    public RelayCommand ToggleCelsiusFerenheit { get; set; }
    public HomeViewModel(IConnectivityService connectivityService, ILocationService locationService,
        IWeatherService weatherService, IAlertService alertService)
    {
        Title = "RAMU Weather";

        _connectivityService = connectivityService;
        _locationService = locationService;
        _weatherService = weatherService;
        _alertService = alertService;
        ToggleCelsiusFerenheit = new RelayCommand(ToggleTemperatureUnit);
    }
    [RelayCommand]

    private async Task ToggleFavorites()
    {
        // Toggle between displaying the Home page and the Favorites page
        await Shell.Current.GoToAsync("//FavouritesPage");
    }

    private void ToggleTemperatureUnit()
    {
        IsCelsius = !IsCelsius;
        IsFerenheit = !IsFerenheit;

        if (IsCelsius)
        {
            CurrentTemp = (double)CurrentWeather.TempC;
            ButtonText = "°C";
        }
        else
        {
            CurrentTemp = (double)CurrentWeather.TempF;
            ButtonText = "°F";
        }
    }


    // Overrides OnAppearing event so the API is not called until query params are read
    [RelayCommand]
    void Appearing()
    {
        if (CurrentLocation?.Name != null && CurrentLocation.Name == LocationName) return;

        Task.Run(LoadWeatherData);
    }

    async Task LoadWeatherData()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            if (!_connectivityService.CheckConnection())
                throw new Exception("Please check your internet connection!");

            //Get current location if location is not passed
            var coordinates = String.Empty;
            if (LocationName == null)
                coordinates = await _locationService.GetCurrentLocationQuery();

            // Gets all weather data for location
            var forecastWeather = await _weatherService.GetAllWeatherData(LocationName != null ? LocationName : coordinates);
            SetWeatherData(forecastWeather);
        }
        catch (Exception e)
        {
            _alertService.DisplayAlert(Title, e.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    void SetWeatherData(ForecastJsonResponse forecastWeather)
    {
        // Sets current loaction and weather data
        CurrentLocation = forecastWeather.Location;
        CurrentLocation.Localtime = CurrentLocation.Localtime.Substring(11);
        CurrentWeather = forecastWeather.Current;
        CurrentTemp = (double)CurrentWeather.TempC;
        switch (CurrentWeather.Condition.Text)
        {
            case "Sunny":
                BackgroundUrl = "sunny.png";
                break;
            case "Clear":
                BackgroundUrl = "night.png";
                break;
        }

        if (CurrentWeather.Condition.Text.ToLower().Contains("rain"))
        {
            BackgroundUrl = "rain.png";
        }
        else if (CurrentWeather.Condition.Text.ToLower().Contains("snow"))
        {
            BackgroundUrl = "snow.png";
        }
        else if (CurrentWeather.Condition.Text.ToLower().Contains("thunder"))
        {
            BackgroundUrl = "thunder.png";
        }
        else if (CurrentWeather.Condition.Text.ToLower().Contains("cloud") || CurrentWeather.Condition.Text.ToLower().Contains("overcast"))
        {
            BackgroundUrl = "cloud.png";
        }
        else
        {
            if (CurrentWeather.IsDay == 1)
            {
                BackgroundUrl = "sunny.png";
            }
            else
            {
                BackgroundUrl = "night.png";
            }
        }

        // Sets weather data for next 24 hours
        WeatherForecastHours = SetNext24HoursData(forecastWeather);

        // Sets next 3 days forecast and modifies day property to display day name
        var forecastDays = new ObservableCollection<Forecastday>(forecastWeather.Forecast.Forecastday);
        SetDaysNames(forecastDays);
        WeatherForecastDays = forecastDays;

    }
    static void SetDaysNames(ObservableCollection<Forecastday> forecastDays)
    {
        foreach (var day in forecastDays)
        {
            var date = DateTime.Parse(day.Date);
            var dayName = date.DayOfWeek.ToString();

            day.Date = dayName == DateTime.Now.DayOfWeek.ToString() ? "Today" : dayName;
        }
    }

    static ObservableCollection<Hour> SetNext24HoursData(ForecastJsonResponse forecastWeather)
    {
        // Gets Weather of 2 days from now
        var forecastNext48Hours = new List<Hour>(forecastWeather.Forecast.Forecastday[0].Hour);
        forecastNext48Hours.AddRange(forecastWeather.Forecast.Forecastday[1].Hour);

        // Finds index of next hour
        Func<Hour, bool> predicate = x => x.Time.CompareTo(DateTime.Now.ToString("yyyy-MM-dd HH:mm")) > 0;
        var nextHourIndex = forecastNext48Hours.IndexOf(forecastNext48Hours.Where(predicate).FirstOrDefault());

        // Modify Time so it shows correct format
        foreach (var hour in forecastNext48Hours)
            hour.Time = hour.Time.Substring(11);
        // Sets weather data 24 hours from now
        var output = new ObservableCollection<Hour>(forecastNext48Hours.GetRange(nextHourIndex, 24));

        return output;
    }
}

