namespace ClimaCellCore.Services
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using ClimaCellCore.Models;
    using ClimaCellCore.Constants;
    using static System.FormattableString;
    using System.Globalization;

    public class ClimaCellService : IDisposable
    {
        private readonly string apiKey;
        private readonly Uri baseUri;
        private readonly IHttpClient httpClient;
        private readonly IJsonSerializerService jsonSerializerService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClimaCellService" /> class. A wrapper for the
        ///     ClimaCellCore API.
        /// </summary>
        /// <param name="apiKey">A ClimaCellCore API key.</param>
        /// <param name="baseUri">Base URI for the ClimaCellCore API. Defaults to https://api.climacell.co/v3/weather/ .</param>
        /// <param name="httpClient"> An optional HTTP client to contact an API with (useful for mocking data for testing).</param>
        /// <param name="jsonSerializerService"> An optional JSON Serializer to handle converting the response string to an object.</param>
        public ClimaCellService(string apiKey, Uri baseUri = null, IHttpClient httpClient = null, IJsonSerializerService jsonSerializerService = null)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException($"{nameof(apiKey)} cannot be empty.");
            }

            this.apiKey = apiKey;
            this.baseUri = baseUri ?? new Uri("https://api.climacell.co/v3/weather/");
            this.httpClient = httpClient ?? new ZipHttpClient();
            this.jsonSerializerService = jsonSerializerService ?? new JsonNetJsonSerializerService();
        }

        /// <summary>
        ///     Make a request for realtime weather data.
        /// </summary>
        /// <param name="latitude">Latitude to request data for in decimal degrees, -87 to 89.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees, -180 to 180.</param>
        /// <param name="unitSystem"></param>
        /// <param name="fields"></param>
        /// <returns>A ClimaCellCore Realtime response with the API headers and data.</returns>
        /// <remarks>Will not check if the passed data layer field(s) can be used in the Realtime climacell endpoint.</remarks>
        public async Task<Realtime> GetRealtime(double latitude, double longitude, string unitSystem, params string[] fields)
        {
            if (fields.Length <= 0)
            {
                return new Realtime
                {
                        IsSuccessStatus = false,
                        ResponseReasonPhrase = $"{nameof(fields)} cannot be empty.",
                };
            }

            var query = BuildRequestUri(latitude, longitude, Endpoint.Realtime, unitSystem ?? UnitSystem.Si, fields);
            var response = await httpClient.HttpRequestAsync($"{baseUri}{query}").ConfigureAwait(false);
            var responseContent = response.Content?.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new Realtime
                {
                    IsSuccessStatus = response.IsSuccessStatusCode,
                    ResponseStatusCode = response.StatusCode,
                    ResponseReasonPhrase = response.ReasonPhrase,
                };
            }

            Realtime realtimeResponse;
            try
            {
                realtimeResponse = await jsonSerializerService.DeserializeJsonAsync<Realtime>(responseContent).ConfigureAwait(false);
                realtimeResponse.TranslateFromHttpMessage(response);
            }
            catch (FormatException e)
            {
                realtimeResponse = new Realtime
                {
                    IsSuccessStatus = false,
                    ResponseReasonPhrase = $"Error parsing results: {e?.InnerException?.Message ?? e.Message}",
                };
            }
            return realtimeResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="latitude">Latitude to request data for in decimal degrees, -87 to 89.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees, -180 to 180.</param>
        /// <param name="timestep">Time step in minutes, 1-60.</param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="unitSystem"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public async Task<Nowcast[]> GetNowcast(double latitude, double longitude, int timestep, DateTime? startTime, DateTime? endTime, string unitSystem, params string[] fields)
        {
            if (fields.Length <= 0)
            {
                return new Nowcast[1]
                {
                    new Nowcast
                    {
                        IsSuccessStatus = false,
                        ResponseReasonPhrase = $"{nameof(fields)} cannot be empty.",
                    }
                };
            }

            var query = BuildRequestUri(latitude, longitude, Endpoint.NowCast, unitSystem ?? UnitSystem.Si, fields, timestep, startTime, endTime);
            var response = await httpClient.HttpRequestAsync($"{baseUri}{query}").ConfigureAwait(false);
            var responseContent = response.Content?.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new Nowcast[1]
                {
                    new Nowcast
                    {
                        IsSuccessStatus = response.IsSuccessStatusCode,
                        ResponseStatusCode = response.StatusCode,
                        ResponseReasonPhrase = response.ReasonPhrase,
                    }
                };
            }

            Nowcast[] nowcastResponse;
            try
            {
                nowcastResponse = await jsonSerializerService.DeserializeJsonAsync<Nowcast[]>(responseContent).ConfigureAwait(false);
                foreach (Nowcast resp in nowcastResponse)
                    resp.TranslateFromHttpMessage(response);
            }
            catch (FormatException e)
            {
                nowcastResponse = new Nowcast[1]
                {
                    new Nowcast
                    {
                        IsSuccessStatus = false,
                        ResponseReasonPhrase = $"Error parsing results: {e?.InnerException?.Message ?? e.Message}",
                    }
                };
            }
            return nowcastResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="unitSystem"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public async Task<Hourly[]> GetHourly(double latitude, double longitude, DateTime? startTime, DateTime? endTime, string unitSystem, params string[] fields)
        {
            if (fields.Length <= 0)
            {
                return new Hourly[1]
                {
                    new Hourly
                    {
                        IsSuccessStatus = false,
                        ResponseReasonPhrase = $"{nameof(fields)} cannot be empty.",
                    }
                };
            }

            var query = BuildRequestUri(latitude, longitude, Endpoint.Hourly, unitSystem ?? UnitSystem.Si, fields, null, startTime, endTime);
            var response = await httpClient.HttpRequestAsync($"{baseUri}{query}").ConfigureAwait(false);
            var responseContent = response.Content?.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new Hourly[1]
                {
                    new Hourly
                    {
                        IsSuccessStatus = response.IsSuccessStatusCode,
                        ResponseStatusCode = response.StatusCode,
                        ResponseReasonPhrase = response.ReasonPhrase,
                    }
                };
            }

            Hourly[] hourlyResponse;
            try
            {
                hourlyResponse = await jsonSerializerService.DeserializeJsonAsync<Hourly[]>(responseContent).ConfigureAwait(false);
                foreach (Hourly resp in hourlyResponse)
                    resp.TranslateFromHttpMessage(response);
            }
            catch (FormatException e)
            {
                hourlyResponse = new Hourly[1]
                {
                    new Hourly
                    {
                        IsSuccessStatus = false,
                        ResponseReasonPhrase = $"Error parsing results: {e?.InnerException?.Message ?? e.Message}",
                    }
                };
            }
            return hourlyResponse;
        }
        }

        /// <summary>
        ///     Build a request uri for a climacell weather endpoint.
        /// </summary>
        /// <param name="latitude">Latitude to request data for in decimal degrees.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees.</param>
        /// <param name="fields"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private string BuildRequestUri(double latitude, double longitude, string endPoint, string unitSystem, string[] fields, int? timestep = null, DateTime? startTime = null, DateTime? endTime = null)
        {
            var fieldString = Uri.EscapeUriString(String.Join(",", fields));
            var queryString = new StringBuilder($"{endPoint}?");

            queryString.Append(Invariant($"lat={latitude:N4}&lon={longitude:N4}"));
            queryString.Append($"&unit_system={unitSystem}");
            queryString.Append($"&fields={fieldString}");

            queryString.Append((timestep.HasValue) ? $"&timestep={timestep}" : "");
            queryString.Append((startTime.HasValue) ? $"&start_time={startTime.Value.ToUniversalTime().ToString("s", CultureInfo.InvariantCulture)}" : "");
            queryString.Append((endTime.HasValue) ? $"&end_time={endTime.Value.ToUniversalTime().ToString("s", CultureInfo.InvariantCulture)}" : "");

            queryString.Append($"&apikey={this.apiKey}");
            return queryString.ToString();
        }

        /// <summary>
        ///     Destructor
        /// </summary>
        ~ClimaCellService()
        {
            Dispose(false);
        }

        #region IDisposable Support

        private bool disposedValue; // To detect redundant calls

        /// <summary>
        ///     Dispose of resources used by the class.
        /// </summary>
        /// <param name="disposing">If the class is disposing managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
            {
                return;
            }

            if (disposing)
            {
                httpClient.Dispose();
            }

            disposedValue = true;
        }

        /// <summary>
        ///     Public access to start disposing of the class instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
