namespace ClimaCellCore.Services
{
    using ClimaCellCore.Extensions;
    using ClimaCellCore.Models;
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using static System.FormattableString;
    using System.Runtime.Serialization;

    public class ClimaCellService : IDisposable
    {
        private readonly string apiKey;
        private readonly Uri baseUri;
        private readonly IHttpClient httpClient;
        private readonly IJsonSerializerService jsonSerializerService;

        private enum RequestType
        {
            [EnumMember(Value = "realtime")]        Realtime,
            [EnumMember(Value = "nowcast")]         Nowcast,
            [EnumMember(Value = "forecast/hourly")] Hourly,
            [EnumMember(Value = "forecast/daily")]  Daily
        }

        public enum UnitSystem
        {
            [EnumMember(Value = "si")] Si, /// <summary> International System of Units </summary>
            [EnumMember(Value = "us")] Us, /// <summary> US customary units </summary>
        }

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
        /// <param name="latitude">Latitude to request data for in decimal degrees.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees.</param>
        /// <returns>A ClimaCellCore Realtime response with the API headers and data.</returns>
        public async Task<Realtime> GetRealtime(double latitude, double longitude, Enum dataLayerFields, UnitSystem unitSystem = UnitSystem.Si)
        {
            var query = BuildRequestUri(latitude, longitude, dataLayerFields, RequestType.Realtime);
            var response = await httpClient.HttpRequestAsync($"{baseUri}{query}").ConfigureAwait(false);
            var responseContent = response.Content?.ReadAsStringAsync();

            Realtime realtimeResponse = new Realtime
            {
                IsSuccessStatus = response.IsSuccessStatusCode,
                ResponseStatusCode = response.StatusCode,
                ResponseReasonPhrase = response.ReasonPhrase,
            };

            if (!realtimeResponse.IsSuccessStatus)
            {
                return realtimeResponse;
            }

            try
            {
                realtimeResponse = await jsonSerializerService.DeserializeJsonAsync<Realtime>(responseContent).ConfigureAwait(false);
                realtimeResponse.TranslateFromHttpMessage(response);
            }
            catch (FormatException e)
            {
                realtimeResponse = null;
                realtimeResponse.IsSuccessStatus = false;
                realtimeResponse.ResponseReasonPhrase = $"Error parsing results: {e?.InnerException?.Message ?? e.Message}";
            }
           
            return realtimeResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="fields"></param>
        /// <param name="requestType"></param>
        /// <returns></returns>
        private string BuildRequestUri(double latitude, double longitude, Enum dataLayerFields, RequestType requestType, UnitSystem unitSystem = UnitSystem.Si)
        {
            var foundFields = GetFlags(dataLayerFields);
            var foundFieldsCount = foundFields.Count();
            var fieldsBuilder = new StringBuilder();

            uint i = 0;
            foreach (Enum flag in foundFields)
            {
                fieldsBuilder.Append(flag.GetMemberValue());
                if (i < foundFieldsCount - 1)
                {
                    fieldsBuilder.Append(",");
                }
                i++;
            }

            var unitSystemString = unitSystem.GetMemberValue();
            var fieldString = Uri.EscapeUriString(fieldsBuilder.ToString());
            var queryString = new StringBuilder(Invariant($"{requestType.GetMemberValue()}?"));

            queryString.Append($"lat={latitude:N4}&lon={longitude:N4}");
            queryString.Append($"&unit_system={unitSystemString}");
            queryString.Append($"&fields={fieldString}");
            queryString.Append($"&apikey={this.apiKey}");

            return queryString.ToString();
        }

        private static IEnumerable<Enum> GetFlags(Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
            {
                if (input.HasFlag(value))
                {
                    yield return value;
                }
            } 
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
