using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ClimaCellCore.Models
{
    public class ClimaCellResponse
    {
        public string AttributionLine => "Powered by ClimaCell";
        public string DataSource => "https://www.climacell.co/";

        public bool IsSuccessStatus { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; }

        public long? RateLimitLimitDay { get; set; }
        public long? RateLimitLimitHour { get; set; }
        public long? RateLimitRemainingDay { get; set; }
        public long? RateLimitRemainingHour { get; set; }
        public string ResponseTime { get; set; }
        public CacheControlHeaderValue CacheControl { get; set; }

        public ClimaCellResponse() => IsSuccessStatus = false;
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
