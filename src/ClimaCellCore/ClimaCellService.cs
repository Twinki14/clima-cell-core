using System;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using ClimaCellCore.Services;
using ClimaCellCore.Models;
using static System.FormattableString;

namespace ClimaCellCore
{
    public class ClimaCellService : IDisposable
    {
        private readonly string apiKey;
        private readonly Uri baseUri;
        private readonly IHttpClient httpClient;
        private readonly IJsonSerializerService jsonSerializerService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClimaCellService"/> class. A wrapper for the
        ///     <see href="https://developer.climacell.co/">climacell API</see>.
        /// </summary>
        /// <param name="apiKey">A climacell API key.</param>
        /// <param name="baseUri">Base URI for the climacell API. Defaults to https://api.climacell.co/v3/weather/.</param>
        /// <param name="httpClient">An optional HTTP client to contact an API with (useful for mocking data for testing).</param>
        /// <param name="jsonSerializerService">An optional JSON Serializer to handle converting the response string to an object.</param>
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
        ///     Create and submit a request for climacell's 'Realtime' present endpoint.
        /// </summary>
        /// <param name="latitude">Latitude to request data for in decimal degrees, -87 to 89.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees, -180 to 180.</param>
        /// <param name="unitSystem">Unit system.</param>
        /// <param name="fields">Fields to request from climacell.</param>
        /// <returns>A climacell Realtime response.</returns>
        /// <remarks>Will not check if the passed data layer field(s) can be used in the Realtime climacell endpoint.</remarks>
        public async Task<Realtime> GetRealtime(double latitude, double longitude, string unitSystem, params string[] fields)
        {
            if (fields.Length <= 0)
            {
                return new Realtime() { Response = new ClimaCellResponse() { IsSuccessStatus = false, ReasonPhrase = $"{nameof(fields)} cannot be empty." } };
            }

            var query = BuildRequestUri(latitude, longitude, Endpoint.Realtime, unitSystem ?? UnitSystem.Si, fields);
            var response = await httpClient.HttpRequestAsync($"{baseUri}{query}").ConfigureAwait(false);

            return await Realtime.Deserialize(response, jsonSerializerService);
        }

        /// <summary>
        ///     Create and submit a request for climacell's 'Nowcast' historical endpoint.
        /// </summary>
        /// <param name="latitude">Latitude to request data for in decimal degrees, -87 to 89.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees, -180 to 180.</param>
        /// <param name="timestep">Time step between each observation, in minutes, 1-60.</param>
        /// <param name="startTime">Starting observation time, passing null will start the observation(s) at the current time.</param>
        /// <param name="endTime">Ending observation time, passing null will use the maximum observation time the endpoint supports.</param>
        /// <param name="unitSystem">Unit system.</param>
        /// <param name="fields">Fields to request from climacell.</param>
        /// <returns>A climacell 'Nowcast' response.</returns>
        public async Task<Nowcast> GetNowcast(double latitude, double longitude, int timestep, DateTime? startTime, DateTime? endTime, string unitSystem, params string[] fields)
        {
            if (fields.Length <= 0)
            {
                return new Nowcast() { Response = new ClimaCellResponse() { IsSuccessStatus = false, ReasonPhrase = $"{nameof(fields)} cannot be empty." } };
            }

            var query = BuildRequestUri(latitude, longitude, Endpoint.NowCast, unitSystem ?? UnitSystem.Si, fields, timestep, startTime, endTime);
            var response = await httpClient.HttpRequestAsync($"{baseUri}{query}").ConfigureAwait(false);

            return await Nowcast.Deserialize(response, jsonSerializerService);
        }

        /// <summary>
        ///     Create and submit a request for climacell's 'Hourly' historical endpoint.
        /// </summary>
        /// <param name="latitude">Latitude to request data for in decimal degrees, -87 to 89.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees, -180 to 180.</param>
        /// <param name="startTime">Starting observation time, passing null will start the observation(s) at the current time.</param>
        /// <param name="endTime">Ending observation time, passing null will use the maximum observation time the endpoint supports.</param>
        /// <param name="unitSystem">Unit system.</param>
        /// <param name="fields">Fields to request from climacell.</param>
        /// <returns>A climacell 'Hourly' response.</returns>
        public async Task<Hourly> GetHourly(double latitude, double longitude, DateTime? startTime, DateTime? endTime, string unitSystem, params string[] fields)
        {
            if (fields.Length <= 0)
            {
                return new Hourly() { Response = new ClimaCellResponse() { IsSuccessStatus = false, ReasonPhrase = $"{nameof(fields)} cannot be empty." } };
            }

            var query = BuildRequestUri(latitude, longitude, Endpoint.Hourly, unitSystem ?? UnitSystem.Si, fields, null, startTime, endTime);
            var response = await httpClient.HttpRequestAsync($"{baseUri}{query}").ConfigureAwait(false);

            return await Hourly.Deserialize(response, jsonSerializerService);
        }

        /// <summary>
        ///     Create and submit a request for climacell's 'Daily' historical endpoint.
        /// </summary>
        /// <param name="latitude">Latitude to request data for in decimal degrees, -87 to 89.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees, -180 to 180.</param>
        /// <param name="startTime">Starting observation time, passing null will start the observation(s) at the current time.</param>
        /// <param name="endTime">Ending observation time, passing null will use the maximum observation time the endpoint supports.</param>
        /// <param name="unitSystem">Unit system.</param>
        /// <param name="fields">Fields to request from climacell.</param>
        /// <returns>A climacell 'Daily' response.</returns>
        public async Task<Daily> GetDaily(double latitude, double longitude, DateTime? startTime, DateTime? endTime, string unitSystem, params string[] fields)
        {
            if (fields.Length <= 0)
            {
                return new Daily() { Response = new ClimaCellResponse() { IsSuccessStatus = false, ReasonPhrase = $"{nameof(fields)} cannot be empty." } };
            }

            var query = BuildRequestUri(latitude, longitude, Endpoint.Daily, unitSystem ?? UnitSystem.Si, fields, null, startTime, endTime);
            var response = await httpClient.HttpRequestAsync($"{baseUri}{query}").ConfigureAwait(false);

            return await Daily.Deserialize(response, jsonSerializerService);
        }

        /// <summary>
        ///     Build a request uri for a climacell weather endpoint.
        /// </summary>
        /// <param name="latitude">Latitude to request data for in decimal degrees.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees.</param>
        /// <param name="endPoint">Target endpoint.</param>
        /// <param name="unitSystem"></param>
        /// <param name="fields">Fields to request from climacell.</param>
        /// <param name="timestep">Time step between each observation.</param>
        /// <param name="startTime">Starting observation time, passing null will start the observation(s) at the current time.</param>
        /// <param name="endTime">Ending observation time, passing null will use the maximum observation time the endpoint supports.</param>
        /// <returns>A uri constructed for use with climacells' 3.0 api.</returns>
        private string BuildRequestUri(double latitude, double longitude, string endPoint, string unitSystem, string[] fields,
                                        int? timestep = null, DateTime? startTime = null, DateTime? endTime = null)
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