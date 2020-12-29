using System;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using ClimaCellCore.Services;
using ClimaCellCore.Models;
using static System.FormattableString;
using System.Collections.Generic;

namespace ClimaCellCore
{
    /// <summary>
    ///     Wrapper service for the climacell API.
    /// </summary>
    public class ClimaCellService : IDisposable
    {
        private readonly string apiKey;
        private readonly Uri baseUri;
        private readonly IHttpClient httpClient;
        private readonly IJsonSerializerService jsonSerializerService;

        /// <summary>
        ///     climacell data endpoints
        /// </summary>
        private protected static class Endpoint
        {
            public static readonly string Realtime = "realtime";
            public static readonly string NowCast = "nowcast";
            public static readonly string Hourly = "forecast/hourly";
            public static readonly string Daily = "forecast/daily";
        }

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
        ///     A realtime call provides observational data at the present time, down to the minute.
        /// </summary>
        /// <param name="latitude">Latitude to request data for in decimal degrees, -87 to 89.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees, -180 to 180.</param>
        /// <param name="fields">Fields to request from climacell.</param>
        /// <param name="paramters">Optional request paramaters <see cref="OptionalParamters"/>.</param>
        /// <returns>A climacell <see cref="Realtime"/> response.</returns>
        public async Task<Realtime> GetRealtime(double latitude, double longitude, List<string> fields, OptionalParamters paramters = null)
        {
            if (fields.Count <= 0)
            {
                throw new ArgumentException($"{nameof(fields)} cannot be empty.");
            }

            var query = BuildRequestUri(latitude, longitude, fields, paramters, Endpoint.Realtime);
            var response = await httpClient.HttpRequestAsync($"{baseUri}{query}").ConfigureAwait(false);

            return await Realtime.Deserialize(response, jsonSerializerService);
        }

        /// <summary>
        ///     A nowcast call provides forecasting data on a minute-­by-­minute basis.
        /// </summary>
        /// <param name="latitude">Latitude to request data for in decimal degrees, -87 to 89.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees, -180 to 180.</param>
        /// <param name="fields">Fields to request from climacell.</param>
        /// <param name="paramters">Optional request paramaters <see cref="OptionalParamters"/>.</param>
        /// <returns>A climacell <see cref="Nowcast"/> response.</returns>
        public async Task<Nowcast> GetNowcast(double latitude, double longitude, List<string> fields, OptionalParamters paramters = null)
        {
            if (fields.Count <= 0)
            {
                throw new ArgumentException($"{nameof(fields)} cannot be empty.");
            }

            var query = BuildRequestUri(latitude, longitude, fields, paramters, Endpoint.NowCast);
            var response = await httpClient.HttpRequestAsync($"{baseUri}{query}").ConfigureAwait(false);

            return await Nowcast.Deserialize(response, jsonSerializerService);
        }

        /// <summary>
        ///    A hourly call provides a global hourly forecast, up to 108 hours (4.5 days) out.
        /// </summary>
        /// <param name="latitude">Latitude to request data for in decimal degrees, -87 to 89.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees, -180 to 180.</param>
        /// <param name="fields">Fields to request from climacell.</param>
        /// <param name="paramters">Optional request paramaters <see cref="OptionalParamters"/>.</param>
        /// <returns>A climacell <see cref="Hourly"/> response.</returns>
        public async Task<Hourly> GetHourly(double latitude, double longitude, List<string> fields, OptionalParamters paramters = null)
        {
            if (fields.Count <= 0)
            {
                throw new ArgumentException($"{nameof(fields)} cannot be empty.");
            }

            var query = BuildRequestUri(latitude, longitude, fields, paramters, Endpoint.Hourly);
            var response = await httpClient.HttpRequestAsync($"{baseUri}{query}").ConfigureAwait(false);

            return await Hourly.Deserialize(response, jsonSerializerService);
        }

        /// <summary>
        ///     A daily call provides a global daily forecast with summaries up to 15 days out.
        /// </summary>
        /// <param name="latitude">Latitude to request data for in decimal degrees, -87 to 89.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees, -180 to 180.</param>
        /// <param name="fields">Fields to request from climacell.</param>
        /// <param name="paramters">Optional request paramaters <see cref="OptionalParamters"/>.</param>
        /// <returns>A climacell <see cref="Daily"/> response.</returns>
        public async Task<Daily> GetDaily(double latitude, double longitude, List<string> fields, OptionalParamters paramters = null)
        {
            if (fields.Count <= 0)
            {
                throw new ArgumentException($"{nameof(fields)} cannot be empty.");
            }

            var query = BuildRequestUri(latitude, longitude, fields, paramters, Endpoint.Daily);
            var response = await httpClient.HttpRequestAsync($"{baseUri}{query}").ConfigureAwait(false);

            return await Daily.Deserialize(response, jsonSerializerService);
        }

        /// <summary>
        ///     Build a request uri for a climacell weather endpoint.
        /// </summary>
        /// <param name="latitude">Latitude to request data for in decimal degrees.</param>
        /// <param name="longitude">Longitude to request data for in decimal degrees.</param>
        /// <param name="fields">Fields to request from climacell.</param>
        /// <param name="paramters">Optional request paramaters <see cref="OptionalParamters"/>.</param>
        /// <param name="endPoint">Target endpoint.</param>
        /// <returns>A uri constructed for use with climacells' 3.0 api.</returns>
        private string BuildRequestUri(double latitude, double longitude, List<string> fields, OptionalParamters paramters, string endPoint)
        {
            var fieldString = Uri.EscapeUriString(string.Join(",", fields));
            var queryString = new StringBuilder($"{endPoint}?");

            queryString.Append(Invariant($"lat={latitude:N4}&lon={longitude:N4}"));
            queryString.Append($"&fields={fieldString}");
            queryString.Append($"&apikey={this.apiKey}");

            if (paramters?.ForecastStartTime != null)
            {
                queryString.Append($"&start_time={paramters.ForecastStartTime.Value.ToUniversalTime().ToString("s", CultureInfo.InvariantCulture)}");
            }

            if (paramters?.ForecastEndTime != null)
            {
                queryString.Append($"&end_time={paramters.ForecastEndTime.Value.ToUniversalTime().ToString("s", CultureInfo.InvariantCulture)}");
            }

            if (paramters?.NowcastTimeStep != null)
            {
                queryString.Append($"&timestep={paramters.NowcastTimeStep}");
            }

            if (!string.IsNullOrWhiteSpace(paramters?.UnitSystem))
            {
                queryString.Append($"&unit_system={paramters.UnitSystem}");
            }

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