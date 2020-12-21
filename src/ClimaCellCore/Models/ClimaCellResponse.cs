using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ClimaCellCore.Models
{
    /// <summary>
    ///     Useful httpResponse and climacell metadata from a climacell response.
    /// </summary>
    public class ClimaCellResponse
    {
        public string AttributionLine => "Powered by ClimaCell";
        public string DataSource => "https://www.climacell.co/";

        /// <summary>
        ///     The response from the climacell request is a success status.
        /// </summary>
        public bool IsSuccessStatus { get; set; }

        /// <summary>
        ///     The status code of the response from the climacell request.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        ///     The reason phrase of the response from the climacell request.
        /// </summary>
        public string ReasonPhrase { get; set; }

        /// <summary>
        ///     The maximum number of requests that the used API key in a request is permitted to make per day.
        /// </summary>
        public long? RateLimitLimitDay { get; set; }

        /// <summary>
        ///     The maximum number of requests that the used API key in a request is permitted to make per hour.
        /// </summary>
        public long? RateLimitLimitHour { get; set; }

        /// <summary>
        ///     The number of requests remaining for the used API key in a request for the day.
        /// </summary>
        public long? RateLimitRemainingDay { get; set; }

        /// <summary>
        ///     The number of requests remaining for the used API key in a request for the hour.
        /// </summary>
        public long? RateLimitRemainingHour { get; set; }

        /// <summary>
        ///     The server-side response time of the climacell request.
        /// </summary>
        public string ResponseTime { get; set; }

        /// <summary>
        ///     Represents the value of the Cache-Control header. See <see cref="CacheControlHeaderValue"/>.
        /// </summary>
        public CacheControlHeaderValue CacheControl { get; set; }

        /// <summary>
        ///     Default initalizor for a <see cref="ClimaCellResponse"/>.
        /// </summary>
        public ClimaCellResponse() => IsSuccessStatus = false;

        /// <summary>
        ///     Initializes a new <see cref="ClimaCellResponse"/> instance from a <see cref="HttpResponseMessage"/>.
        /// </summary>
        /// <remarks>Assumes the <see cref="HttpResponseMessage"/> is from a climacell endpoint.</remarks>
        public ClimaCellResponse(HttpResponseMessage response)
        {
            IsSuccessStatus = response.IsSuccessStatusCode;
            StatusCode = response.StatusCode;
            ReasonPhrase = response.ReasonPhrase;

            response.Headers.TryGetValues("X-RateLimit-Limit-day", out var rateLimitLimitDay);
            response.Headers.TryGetValues("X-RateLimit-Limit-hour", out var rateLimitLimitHour);
            response.Headers.TryGetValues("X-RateLimit-Remaining-day", out var rateLimitRemainingDay);
            response.Headers.TryGetValues("X-RateLimit-Remaining-hour", out var rateLimitRemainingHour);
            response.Headers.TryGetValues("Date", out var responseTimeHeader);

            CacheControl = response.Headers.CacheControl;
            RateLimitLimitDay = long.TryParse(rateLimitLimitDay?.FirstOrDefault(), out var rateLimitLimitDayParsed)
                ? (long?)rateLimitLimitDayParsed
                : null;
            RateLimitLimitHour = long.TryParse(rateLimitLimitHour?.FirstOrDefault(), out var rateLimitLimitHourParsed)
                ? (long?)rateLimitLimitHourParsed
                : null;
            RateLimitRemainingDay = long.TryParse(rateLimitRemainingDay?.FirstOrDefault(), out var rateLimitRemainingDayParsed)
                ? (long?)rateLimitRemainingDayParsed
                : null;
            RateLimitRemainingHour = long.TryParse(rateLimitRemainingHour?.FirstOrDefault(), out var rateLimitRemainingHourParsed)
                ? (long?)rateLimitRemainingHourParsed
                : null;
            ResponseTime = responseTimeHeader?.FirstOrDefault();
        }
    }
}
