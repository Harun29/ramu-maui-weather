/*
 * WeatherAPI.Standard
 *
 * This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ).
 */
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using WeatherAPI.Standard;
using WeatherAPI.Standard.Utilities;
using WeatherAPI.Standard.Http.Request;
using WeatherAPI.Standard.Http.Response;
using WeatherAPI.Standard.Http.Client;
using WeatherAPI.Standard.Exceptions;

namespace WeatherAPI.Standard.Controllers
{
    public partial class APIsController: BaseController
    {
        #region Singleton Pattern

        //private static variables for the singleton pattern
        private static object syncObject = new object();
        private static APIsController instance = null;

        /// <summary>
        /// Singleton pattern implementation
        /// </summary>
        internal static APIsController Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (null == instance)
                    {
                        instance = new APIsController();
                    }
                }
                return instance;
            }
        }

        #endregion Singleton Pattern

        /// <summary>
        /// Current weather or realtime weather API method allows a user to get up to date current weather information in json and xml. The data is returned as a Current Object.Current object contains current or realtime weather information for a given city.
        /// </summary>
        /// <param name="q">Required parameter: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to learn more.</param>
        /// <param name="lang">Optional parameter: Returns 'condition:text' field in API in the desired language. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to check 'lang-code'.</param>
        /// <return>Returns the Models.CurrentJsonResponse response from the API call</return>
        public Models.CurrentJsonResponse GetRealtimeWeather(string q, string lang = null)
        {
            Task<Models.CurrentJsonResponse> t = GetRealtimeWeatherAsync(q, lang);
            APIHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Current weather or realtime weather API method allows a user to get up to date current weather information in json and xml. The data is returned as a Current Object.Current object contains current or realtime weather information for a given city.
        /// </summary>
        /// <param name="q">Required parameter: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to learn more.</param>
        /// <param name="lang">Optional parameter: Returns 'condition:text' field in API in the desired language. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to check 'lang-code'.</param>
        /// <return>Returns the Models.CurrentJsonResponse response from the API call</return>
        public async Task<Models.CurrentJsonResponse> GetRealtimeWeatherAsync(string q, string lang = null)
        {
            //the base uri for api requests
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/current.json");

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "q", q },
                { "lang", lang },
                { "key", Configuration.Key }
            },ArrayDeserializationFormat,ParameterSeparator);


            //validate and preprocess url
            string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string,string>()
            {
                { "user-agent", "APIMATIC 2.0" },
                { "accept", "application/json" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 400)
                throw new APIException(@"Error code 1003: Parameter 'q' not provided.Error code 1005: API request url is invalid.Error code 1006: No location found matching parameter 'q'Error code 9999: Internal application error.", _context);

            if (_response.StatusCode == 401)
                throw new APIException(@"Error code 1002: API key not provided.Error code 2006: API key provided is invalid", _context);

            if (_response.StatusCode == 403)
                throw new APIException(@"Error code 2007: API key has exceeded calls per month quota.<br />Error code 2008: API key has been disabled.", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<Models.CurrentJsonResponse>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Forecast weather API method returns upto next 10 day weather forecast and weather alert as json. The data is returned as a Forecast Object.<br />Forecast object contains astronomy data, day weather forecast and hourly interval weather information for a given city.
        /// </summary>
        /// <param name="q">Required parameter: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to learn more.</param>
        /// <param name="days">Required parameter: Number of days of weather forecast. Value ranges from 1 to 10</param>
        /// <param name="dt">Optional parameter: Date should be between today and next 10 day in yyyy-MM-dd format</param>
        /// <param name="unixdt">Optional parameter: Please either pass 'dt' or 'unixdt' and not both in same request.<br />unixdt should be between today and next 10 day in Unix format</param>
        /// <param name="hour">Optional parameter: Must be in 24 hour. For example 5 pm should be hour=17, 6 am as hour=6</param>
        /// <param name="lang">Optional parameter: Returns 'condition:text' field in API in the desired language. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to check 'lang-code'.</param>
        /// <return>Returns the Models.ForecastJsonResponse response from the API call</return>
        public Models.ForecastJsonResponse GetForecastWeather(
                string q,
                int days,
                DateTime? dt = null,
                int? unixdt = null,
                int? hour = null,
                string lang = null)
        {
            Task<Models.ForecastJsonResponse> t = GetForecastWeatherAsync(q, days, dt, unixdt, hour, lang);
            APIHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Forecast weather API method returns upto next 10 day weather forecast and weather alert as json. The data is returned as a Forecast Object.<br />Forecast object contains astronomy data, day weather forecast and hourly interval weather information for a given city.
        /// </summary>
        /// <param name="q">Required parameter: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to learn more.</param>
        /// <param name="days">Required parameter: Number of days of weather forecast. Value ranges from 1 to 10</param>
        /// <param name="dt">Optional parameter: Date should be between today and next 10 day in yyyy-MM-dd format</param>
        /// <param name="unixdt">Optional parameter: Please either pass 'dt' or 'unixdt' and not both in same request.<br />unixdt should be between today and next 10 day in Unix format</param>
        /// <param name="hour">Optional parameter: Must be in 24 hour. For example 5 pm should be hour=17, 6 am as hour=6</param>
        /// <param name="lang">Optional parameter: Returns 'condition:text' field in API in the desired language. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to check 'lang-code'.</param>
        /// <return>Returns the Models.ForecastJsonResponse response from the API call</return>
        public async Task<Models.ForecastJsonResponse> GetForecastWeatherAsync(
                string q,
                int days,
                DateTime? dt = null,
                int? unixdt = null,
                int? hour = null,
                string lang = null)
        {
            //the base uri for api requests
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/forecast.json");

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "q", q },
                { "days", days },
                { "dt", (dt.HasValue) ? dt.Value.ToString("yyyy'-'MM'-'dd") : null },
                { "unixdt", unixdt },
                { "hour", hour },
                { "lang", lang },
                { "key", Configuration.Key }
            },ArrayDeserializationFormat,ParameterSeparator);


            //validate and preprocess url
            string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string,string>()
            {
                { "user-agent", "APIMATIC 2.0" },
                { "accept", "application/json" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 400)
                throw new APIException(@"Error code 1003: Parameter 'q' not provided.Error code 1005: API request url is invalid.Error code 1006: No location found matching parameter 'q'Error code 9999: Internal application error.", _context);

            if (_response.StatusCode == 401)
                throw new APIException(@"Error code 1002: API key not provided.Error code 2006: API key provided is invalid", _context);

            if (_response.StatusCode == 403)
                throw new APIException(@"Error code 2007: API key has exceeded calls per month quota.<br />Error code 2008: API key has been disabled.", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<Models.ForecastJsonResponse>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// History weather API method returns historical weather for a date on or after 1st Jan, 2015 as json. The data is returned as a Forecast Object.
        /// </summary>
        /// <param name="q">Required parameter: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to learn more.</param>
        /// <param name="dt">Required parameter: Date on or after 1st Jan, 2015 in yyyy-MM-dd format</param>
        /// <param name="unixdt">Optional parameter: Please either pass 'dt' or 'unixdt' and not both in same request.<br />unixdt should be on or after 1st Jan, 2015 in Unix format</param>
        /// <param name="endDt">Optional parameter: Date on or after 1st Jan, 2015 in yyyy-MM-dd format'end_dt' should be greater than 'dt' parameter and difference should not be more than 30 days between the two dates.</param>
        /// <param name="unixendDt">Optional parameter: Date on or after 1st Jan, 2015 in Unix Timestamp format<br />unixend_dt has same restriction as 'end_dt' parameter. Please either pass 'end_dt' or 'unixend_dt' and not both in same request. e.g.: unixend_dt=1490227200</param>
        /// <param name="hour">Optional parameter: Must be in 24 hour. For example 5 pm should be hour=17, 6 am as hour=6</param>
        /// <param name="lang">Optional parameter: Returns 'condition:text' field in API in the desired language. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to check 'lang-code'.</param>
        /// <return>Returns the Models.HistoryJsonResponse response from the API call</return>
        public Models.HistoryJsonResponse GetHistoryWeather(
                string q,
                DateTime dt,
                int? unixdt = null,
                DateTime? endDt = null,
                int? unixendDt = null,
                int? hour = null,
                string lang = null)
        {
            Task<Models.HistoryJsonResponse> t = GetHistoryWeatherAsync(q, dt, unixdt, endDt, unixendDt, hour, lang);
            APIHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// History weather API method returns historical weather for a date on or after 1st Jan, 2015 as json. The data is returned as a Forecast Object.
        /// </summary>
        /// <param name="q">Required parameter: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to learn more.</param>
        /// <param name="dt">Required parameter: Date on or after 1st Jan, 2015 in yyyy-MM-dd format</param>
        /// <param name="unixdt">Optional parameter: Please either pass 'dt' or 'unixdt' and not both in same request.<br />unixdt should be on or after 1st Jan, 2015 in Unix format</param>
        /// <param name="endDt">Optional parameter: Date on or after 1st Jan, 2015 in yyyy-MM-dd format'end_dt' should be greater than 'dt' parameter and difference should not be more than 30 days between the two dates.</param>
        /// <param name="unixendDt">Optional parameter: Date on or after 1st Jan, 2015 in Unix Timestamp format<br />unixend_dt has same restriction as 'end_dt' parameter. Please either pass 'end_dt' or 'unixend_dt' and not both in same request. e.g.: unixend_dt=1490227200</param>
        /// <param name="hour">Optional parameter: Must be in 24 hour. For example 5 pm should be hour=17, 6 am as hour=6</param>
        /// <param name="lang">Optional parameter: Returns 'condition:text' field in API in the desired language. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to check 'lang-code'.</param>
        /// <return>Returns the Models.HistoryJsonResponse response from the API call</return>
        public async Task<Models.HistoryJsonResponse> GetHistoryWeatherAsync(
                string q,
                DateTime dt,
                int? unixdt = null,
                DateTime? endDt = null,
                int? unixendDt = null,
                int? hour = null,
                string lang = null)
        {
            //the base uri for api requests
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/history.json");

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "q", q },
                { "dt", dt.ToString("yyyy'-'MM'-'dd") },
                { "unixdt", unixdt },
                { "end_dt", (endDt.HasValue) ? endDt.Value.ToString("yyyy'-'MM'-'dd") : null },
                { "unixend_dt", unixendDt },
                { "hour", hour },
                { "lang", lang },
                { "key", Configuration.Key }
            },ArrayDeserializationFormat,ParameterSeparator);


            //validate and preprocess url
            string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string,string>()
            {
                { "user-agent", "APIMATIC 2.0" },
                { "accept", "application/json" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 400)
                throw new APIException(@"Error code 1003: Parameter 'q' not provided.Error code 1005: API request url is invalid.Error code 1006: No location found matching parameter 'q'Error code 9999: Internal application error.", _context);

            if (_response.StatusCode == 401)
                throw new APIException(@"Error code 1002: API key not provided.Error code 2006: API key provided is invalid", _context);

            if (_response.StatusCode == 403)
                throw new APIException(@"Error code 2007: API key has exceeded calls per month quota.<br />Error code 2008: API key has been disabled.", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<Models.HistoryJsonResponse>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// WeatherAPI.com Search or Autocomplete API returns matching cities and towns as an array of Location object.
        /// </summary>
        /// <param name="q">Required parameter: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to learn more.</param>
        /// <return>Returns the List<Models.SearchJsonResponse> response from the API call</return>
        public List<Models.SearchJsonResponse> SearchAutocompleteWeather(string q)
        {
            Task<List<Models.SearchJsonResponse>> t = SearchAutocompleteWeatherAsync(q);
            APIHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// WeatherAPI.com Search or Autocomplete API returns matching cities and towns as an array of Location object.
        /// </summary>
        /// <param name="q">Required parameter: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to learn more.</param>
        /// <return>Returns the List<Models.SearchJsonResponse> response from the API call</return>
        public async Task<List<Models.SearchJsonResponse>> SearchAutocompleteWeatherAsync(string q)
        {
            //the base uri for api requests
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/search.json");

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "q", q },
                { "key", Configuration.Key }
            },ArrayDeserializationFormat,ParameterSeparator);


            //validate and preprocess url
            string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string,string>()
            {
                { "user-agent", "APIMATIC 2.0" },
                { "accept", "application/json" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 400)
                throw new APIException(@"Error code 1003: Parameter 'q' not provided.Error code 1005: API request url is invalid.Error code 1006: No location found matching parameter 'q'Error code 9999: Internal application error.", _context);

            if (_response.StatusCode == 401)
                throw new APIException(@"Error code 1002: API key not provided.Error code 2006: API key provided is invalid", _context);

            if (_response.StatusCode == 403)
                throw new APIException(@"Error code 2007: API key has exceeded calls per month quota.<br />Error code 2008: API key has been disabled.", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<List<Models.SearchJsonResponse>>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// IP Lookup API method allows a user to get up to date information for an IP address.
        /// </summary>
        /// <param name="q">Required parameter: Pass IP address.</param>
        /// <return>Returns the Models.IpJsonResponse response from the API call</return>
        public Models.IpJsonResponse GetIpLookup(string q)
        {
            Task<Models.IpJsonResponse> t = GetIpLookupAsync(q);
            APIHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// IP Lookup API method allows a user to get up to date information for an IP address.
        /// </summary>
        /// <param name="q">Required parameter: Pass IP address.</param>
        /// <return>Returns the Models.IpJsonResponse response from the API call</return>
        public async Task<Models.IpJsonResponse> GetIpLookupAsync(string q)
        {
            //the base uri for api requests
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/ip.json");

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "q", q },
                { "key", Configuration.Key }
            },ArrayDeserializationFormat,ParameterSeparator);


            //validate and preprocess url
            string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string,string>()
            {
                { "user-agent", "APIMATIC 2.0" },
                { "accept", "application/json" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 400)
                throw new APIException(@"Error code 1003: Parameter 'q' not provided.Error code 1005: API request url is invalid.Error code 1006: No location found matching parameter 'q'Error code 9999: Internal application error.", _context);

            if (_response.StatusCode == 401)
                throw new APIException(@"Error code 1002: API key not provided.Error code 2006: API key provided is invalid", _context);

            if (_response.StatusCode == 403)
                throw new APIException(@"Error code 2007: API key has exceeded calls per month quota.<br />Error code 2008: API key has been disabled.", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<Models.IpJsonResponse>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Return Location Object
        /// </summary>
        /// <param name="q">Required parameter: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to learn more.</param>
        /// <return>Returns the Models.TimezoneJsonResponse response from the API call</return>
        public Models.TimezoneJsonResponse GetTimeZone(string q)
        {
            Task<Models.TimezoneJsonResponse> t = GetTimeZoneAsync(q);
            APIHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Return Location Object
        /// </summary>
        /// <param name="q">Required parameter: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to learn more.</param>
        /// <return>Returns the Models.TimezoneJsonResponse response from the API call</return>
        public async Task<Models.TimezoneJsonResponse> GetTimeZoneAsync(string q)
        {
            //the base uri for api requests
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/timezone.json");

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "q", q },
                { "key", Configuration.Key }
            },ArrayDeserializationFormat,ParameterSeparator);


            //validate and preprocess url
            string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string,string>()
            {
                { "user-agent", "APIMATIC 2.0" },
                { "accept", "application/json" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 400)
                throw new APIException(@"Error code 1003: Parameter 'q' not provided.Error code 1005: API request url is invalid.Error code 1006: No location found matching parameter 'q'Error code 9999: Internal application error.", _context);

            if (_response.StatusCode == 401)
                throw new APIException(@"Error code 1002: API key not provided.Error code 2006: API key provided is invalid", _context);

            if (_response.StatusCode == 403)
                throw new APIException(@"Error code 2007: API key has exceeded calls per month quota.<br />Error code 2008: API key has been disabled.", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<Models.TimezoneJsonResponse>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

        /// <summary>
        /// Return Location and Astronomy Object
        /// </summary>
        /// <param name="q">Required parameter: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to learn more.</param>
        /// <param name="dt">Required parameter: Date on or after 1st Jan, 2015 in yyyy-MM-dd format</param>
        /// <return>Returns the Models.AstronomyJsonResponse response from the API call</return>
        public Models.AstronomyJsonResponse GetAstronomy(string q, DateTime dt)
        {
            Task<Models.AstronomyJsonResponse> t = GetAstronomyAsync(q, dt);
            APIHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Return Location and Astronomy Object
        /// </summary>
        /// <param name="q">Required parameter: Pass US Zipcode, UK Postcode, Canada Postalcode, IP address, Latitude/Longitude (decimal degree) or city name. Visit [request parameter section](https://www.weatherapi.com/docs/#intro-request) to learn more.</param>
        /// <param name="dt">Required parameter: Date on or after 1st Jan, 2015 in yyyy-MM-dd format</param>
        /// <return>Returns the Models.AstronomyJsonResponse response from the API call</return>
        public async Task<Models.AstronomyJsonResponse> GetAstronomyAsync(string q, DateTime dt)
        {
            //the base uri for api requests
            string _baseUri = Configuration.BaseUri;

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/astronomy.json");

            //process optional query parameters
            APIHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "q", q },
                { "dt", dt.ToString("yyyy'-'MM'-'dd") },
                { "key", Configuration.Key }
            },ArrayDeserializationFormat,ParameterSeparator);


            //validate and preprocess url
            string _queryUrl = APIHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string,string>()
            {
                { "user-agent", "APIMATIC 2.0" },
                { "accept", "application/json" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = ClientInstance.Get(_queryUrl,_headers);

            //invoke request and get response
            HttpStringResponse _response = (HttpStringResponse) await ClientInstance.ExecuteAsStringAsync(_request).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request,_response);

            //Error handling using HTTP status codes
            if (_response.StatusCode == 400)
                throw new APIException(@"Error code 1003: Parameter 'q' not provided.Error code 1005: API request url is invalid.Error code 1006: No location found matching parameter 'q'Error code 9999: Internal application error.", _context);

            if (_response.StatusCode == 401)
                throw new APIException(@"Error code 1002: API key not provided.Error code 2006: API key provided is invalid", _context);

            if (_response.StatusCode == 403)
                throw new APIException(@"Error code 2007: API key has exceeded calls per month quota.<br />Error code 2008: API key has been disabled.", _context);

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            try
            {
                return APIHelper.JsonDeserialize<Models.AstronomyJsonResponse>(_response.Body);
            }
            catch (Exception _ex)
            {
                throw new APIException("Failed to parse the response: " + _ex.Message, _context);
            }
        }

    }
} 