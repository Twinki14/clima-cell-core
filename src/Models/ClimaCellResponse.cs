namespace ClimaCellCore.Models
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class ClimaCellResponse
    {
        public string AttributionLine => "Powered by ClimaCell";
        public string DataSource => "https://www.climacell.co/";
        public bool IsSuccessStatus { get; set; }
        public HttpStatusCode ResponseStatusCode { get; set; }
        public string ResponseReasonPhrase { get; set; }
        public ResponseHeader Headers { get; set; }

        /// <summary>
        ///     Translates the object props from an HttpResponseMessage.
        /// </summary>
        /// <param name="response">The HttpResponseMessage to translate from.</param>
        public void TranslateFromHttpMessage(HttpResponseMessage response)
        {
            IsSuccessStatus = response.IsSuccessStatusCode;
            ResponseStatusCode = response.StatusCode;
            ResponseReasonPhrase = response.ReasonPhrase;

            Headers = new ResponseHeader();
            Headers.TranslateFromHttpMessage(response);
        }
    }

    public class ResponseHeader
    {
        public long? RateLimitLimitDay { get; set; }
        public long? RateLimitLimitHour { get; set; }
        public long? RateLimitRemainingDay { get; set; }
        public long? RateLimitRemainingHour { get; set; }
        public string ResponseTime { get; set; }
        public CacheControlHeaderValue CacheControl { get; set; }

        /// <summary>
        ///     Translates the header props from a HttpResponseMessage headers.
        /// </summary>
        /// <param name="response">The HttpResponseMessage to translate from.</param>
        public void TranslateFromHttpMessage(HttpResponseMessage response)
        {
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
