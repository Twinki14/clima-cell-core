using Moq;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using ClimaCellCore;
using ClimaCellCore.Services;

namespace ClimaCellCore.Tests.Fixtures
{
    public class ResponseFixture
    {
        // The maximum number of requests that the API key is permitted to make per day/hour.
        public static long RateLimitLimitDay => 1000;
        public static long RateLimitLimitHour => 100;

        // The number of requests remaining in the current rate limit window.
        public static long RateLimitRemainingDay => 950;
        public static long RateLimitRemainingHour => 50;

        public static double Latitude => 42.3026;
        public static double Longitude => -71.1760;

        public static DateTime RealtimeObservationTime => DateTime.Parse($"2020-12-27T12:59:00.754Z", null, DateTimeStyles.AdjustToUniversal);

        public static int NowcastDataPoints = 73;
        public static int HourlyDataPoints = 109;
        public static int DailyDataPoints = 15;

        // in seconds
        public static double ResponseTime => 0.200;

        public ResponseFixture()
        {
            var mockClient = new Mock<IHttpClient>();

            // Setup mockClient to take any string, and return MockRealtimeResponse
            mockClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(MockRealtimeResponse));
            using (var climaRealtimeService = new ClimaCellService("FakeKey", httpClient: mockClient.Object))
            {
                // Remember, all this is doing is returning MockRealtimeResponse whose response content is populated by Realtime.Normal.json
                // so the passed fields do not matter at all
                NormalRealtimeResponse = climaRealtimeService.GetRealtime(Latitude, Longitude, new List<string> { "mockField" }).Result;
            }

            mockClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(MockNowcastResponse));
            using (var climaNowcastService = new ClimaCellService("FakeKey", httpClient: mockClient.Object))
            {
                NormalNowcastResponse = climaNowcastService.GetNowcast(Latitude, Longitude, new List<string> { "mockField" }).Result;
            }

            mockClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(MockHourlyResponse));
            using (var climaHourlyService = new ClimaCellService("FakeKey", httpClient: mockClient.Object))
            {
                NormalHourlyResponse = climaHourlyService.GetHourly(Latitude, Longitude, new List<string> { "mockField" }).Result;
            }

            mockClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(MockDailyResponse));
            using (var climaDailyService = new ClimaCellService("FakeKey", httpClient: mockClient.Object))
            {
                NormalDailyResponse = climaDailyService.GetDaily(Latitude, Longitude, new List<string> { "mockField" }).Result;
            }

            // BadRequest / Unauthorized request
            mockClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(MockBadRequestResponse));
            using (var climaDailyService = new ClimaCellService("FakeKey", httpClient: mockClient.Object))
            {
                BadRequestRealtimeResponse = climaDailyService.GetRealtime(Latitude, Longitude, new List<string> { "mockField" }).Result;
            }

            mockClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(MockInvalidAPIKeyRequest));
            using (var climaDailyService = new ClimaCellService("FakeKey", httpClient: mockClient.Object))
            {
                UnauthorizedRealtimeResponse = climaDailyService.GetRealtime(Latitude, Longitude, new List<string> { "mockField" }).Result;
            }
        }

        public Realtime NormalRealtimeResponse { get; }
        public Nowcast NormalNowcastResponse { get; }
        public Hourly NormalHourlyResponse { get; }
        public Daily NormalDailyResponse { get; }

        public Realtime BadRequestRealtimeResponse { get; }
        public Realtime UnauthorizedRealtimeResponse { get; }

        private static HttpResponseMessage MockRealtimeResponse
        {
            get
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(File.ReadAllText($"{AppContext.BaseDirectory}/Data/Realtime.Normal.json"))
                };

                var respondingTime = DateTime.Now.AddSeconds(ResponseTime);

                response.Headers.Add("X-RateLimit-Limit-day", $"{RateLimitLimitDay}");
                response.Headers.Add("X-RateLimit-Limit-hour", $"{RateLimitLimitHour}");
                response.Headers.Add("X-RateLimit-Remaining-day", $"{RateLimitRemainingDay}");
                response.Headers.Add("X-RateLimit-Remaining-hour", $"{ RateLimitRemainingHour}");
                response.Headers.TryAddWithoutValidation("Date", $"{respondingTime.ToUniversalTime().ToString("s", CultureInfo.InvariantCulture)}");
                response.Headers.Add("Cache-Control", "no-store");
                return response;
            }
        }

        private static HttpResponseMessage MockNowcastResponse
        {
            get
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(File.ReadAllText($"{AppContext.BaseDirectory}/Data/Nowcast.Normal.json"))
                };

                var respondingTime = DateTime.Now.AddSeconds(ResponseTime);

                response.Headers.Add("X-RateLimit-Limit-day", $"{RateLimitLimitDay}");
                response.Headers.Add("X-RateLimit-Limit-hour", $"{RateLimitLimitHour}");
                response.Headers.Add("X-RateLimit-Remaining-day", $"{RateLimitRemainingDay}");
                response.Headers.Add("X-RateLimit-Remaining-hour", $"{ RateLimitRemainingHour}");
                response.Headers.TryAddWithoutValidation("Date", $"{respondingTime.ToUniversalTime().ToString("s", CultureInfo.InvariantCulture)}");
                response.Headers.Add("Cache-Control", "no-store");
                return response;
            }
        }

        private static HttpResponseMessage MockHourlyResponse
        {
            get
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(File.ReadAllText($"{AppContext.BaseDirectory}/Data/Hourly.Normal.json"))
                };

                var respondingTime = DateTime.Now.AddSeconds(ResponseTime);

                response.Headers.Add("X-RateLimit-Limit-day", $"{RateLimitLimitDay}");
                response.Headers.Add("X-RateLimit-Limit-hour", $"{RateLimitLimitHour}");
                response.Headers.Add("X-RateLimit-Remaining-day", $"{RateLimitRemainingDay}");
                response.Headers.Add("X-RateLimit-Remaining-hour", $"{ RateLimitRemainingHour}");
                response.Headers.TryAddWithoutValidation("Date", $"{respondingTime.ToUniversalTime().ToString("s", CultureInfo.InvariantCulture)}");
                response.Headers.Add("Cache-Control", "no-store");
                return response;
            }
        }

        private static HttpResponseMessage MockDailyResponse
        {
            get
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(File.ReadAllText($"{AppContext.BaseDirectory}/Data/Daily.Normal.json"))
                };

                var respondingTime = DateTime.Now.AddSeconds(ResponseTime);

                response.Headers.Add("X-RateLimit-Limit-day", $"{RateLimitLimitDay}");
                response.Headers.Add("X-RateLimit-Limit-hour", $"{RateLimitLimitHour}");
                response.Headers.Add("X-RateLimit-Remaining-day", $"{RateLimitRemainingDay}");
                response.Headers.Add("X-RateLimit-Remaining-hour", $"{ RateLimitRemainingHour}");
                response.Headers.TryAddWithoutValidation("Date", $"{respondingTime.ToUniversalTime().ToString("s", CultureInfo.InvariantCulture)}");
                response.Headers.Add("Cache-Control", "no-store");
                return response;
            }
        }

        private static HttpResponseMessage MockBadRequestResponse
        {
            get
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ReasonPhrase = "BadRequest",
                    Content = new StringContent(File.ReadAllText($"{AppContext.BaseDirectory}/Data/400.BadRequest.json"))
                };
                return response;
            }
        }

        private static HttpResponseMessage MockInvalidAPIKeyRequest
        {
            get
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ReasonPhrase = "Unauthorized",
                    Content = new StringContent(File.ReadAllText($"{AppContext.BaseDirectory}/Data/401.Unauthorized.json"))
                };
                return response;
            }
        }
    }
}