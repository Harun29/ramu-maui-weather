using WeatherApp.ViewModel;

namespace WeatherApp.View;

public partial class FavouritesPage : ContentPage
{
	public FavouritesPage(FavouritesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        // Get the viewmodel from BindingContext
        FavouritesViewModel viewModel = (FavouritesViewModel)BindingContext;

        // Get the updated text from the search bar
        string searchText = searchBar.Text;

        // Call SearchCity function on the viewmodel
        viewModel.SearchCity(searchText);
    }



}
