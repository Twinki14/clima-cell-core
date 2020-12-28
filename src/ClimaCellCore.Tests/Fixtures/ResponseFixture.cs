namespace ClimaCellCore.Tests.Fixtures
{
    using Moq;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ClimaCellCore;
    using ClimaCellCore.Services;
    using System.Collections.Generic;

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

        /** Fields:
            temp
            feels_like
            dewpoint
            wind_speed
            wind_gust
            baro_pressure
            visibility
            humidity
            wind_direction
            precipitation
            precipitation_type
            cloud_cover
            cloud_ceiling
            cloud_base
            surface_shortwave_radiation
            sunrise
            sunset
            moon_phase
            weather_code
            epa_aqi
            epa_primary_pollutant
            china_aqi
            china_primary_pollutant
            pm25
            pm10
            o3
            no2
            co
            so2
            epa_health_concern
            china_health_concern
            pollen_tree
            pollen_weed
            pollen_grass
            observation_time 
        **/
        public Realtime NormalRealtimeResponse { get; }

        /** TimeStep 5 minutes, 73 expected objects
            Fields:
                temp
                feels_like
                dewpoint
                wind_speed
                wind_gust
                baro_pressure
                visibility
                precipitation
                cloud_cover
                cloud_ceiling
                cloud_base
                surface_shortwave_radiation
                humidity
                wind_direction
                precipitation_type
                sunrise
                sunset
                epa_aqi
                epa_primary_pollutant
                china_aqi
                china_primary_pollutant
                pm25
                pm10
                o3
                no2
                co
                so2
                epa_health_concern
                china_health_concern
                pollen_tree
                pollen_weed
                pollen_grass
                observation_time
                weather_code
        **/
        public Nowcast NormalNowcastResponse { get; }

        /** Expected 110 objects
            Fields:
                temp
                precipitation
                precipitation_type
                precipitation_probability
                feels_like
                humidity
                baro_pressure
                dewpoint
                wind_speed
                wind_gust
                wind_direction
                visibility
                cloud_cover
                cloud_ceiling
                cloud_base
                surface_shortwave_radiation
                sunrise
                sunset
                moon_phase
                weather_code
                observation_time
                epa_aqi
                epa_primary_pollutant
                china_aqi
                china_primary_pollutant
                pm25
                pm10
                o3
                no2
                co
                so2
                epa_health_concern
                china_health_concern
                pollen_tree
                pollen_weed
                pollen_grass
        **/
        public Hourly NormalHourlyResponse { get; }

        /** Expected 15 objects
            Fields:
                temp
                precipitation_accumulation
                precipitation
                precipitation_probability
                feels_like
                humidity
                baro_pressure
                dewpoint
                wind_speed
                wind_direction
                visibility
                sunrise
                sunset
                weather_code
                observation_time
        **/
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

                response.Headers.Add("X-RateLimit-Limit-day", RateLimitLimitDay.ToString(CultureInfo.InvariantCulture));
                response.Headers.Add("X-RateLimit-Remaining-day", RateLimitRemainingDay.ToString(CultureInfo.InvariantCulture));
                response.Headers.Add("X-RateLimit-Remaining-hour", RateLimitRemainingHour.ToString(CultureInfo.InvariantCulture));
                //response.Headers.Add("Date", DateTime.Now.AddSeconds(ResponseTime).ToUniversalTime().ToString("s", CultureInfo.InvariantCulture));
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

                response.Headers.Add("X-RateLimit-Limit-day", RateLimitLimitDay.ToString(CultureInfo.InvariantCulture));
                response.Headers.Add("X-RateLimit-Remaining-day", RateLimitRemainingDay.ToString(CultureInfo.InvariantCulture));
                response.Headers.Add("X-RateLimit-Remaining-hour", RateLimitRemainingHour.ToString(CultureInfo.InvariantCulture));
                //response.Headers.Add("Date", DateTime.Now.AddSeconds(ResponseTime).ToString(CultureInfo.InvariantCulture));
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

                response.Headers.Add("X-RateLimit-Limit-day", RateLimitLimitDay.ToString(CultureInfo.InvariantCulture));
                response.Headers.Add("X-RateLimit-Remaining-day", RateLimitRemainingDay.ToString(CultureInfo.InvariantCulture));
                response.Headers.Add("X-RateLimit-Remaining-hour", RateLimitRemainingHour.ToString(CultureInfo.InvariantCulture));
                //response.Headers.Add("Date", DateTime.Now.AddSeconds(ResponseTime).ToString(CultureInfo.InvariantCulture));
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

                response.Headers.Add("X-RateLimit-Limit-day", RateLimitLimitDay.ToString(CultureInfo.InvariantCulture));
                response.Headers.Add("X-RateLimit-Remaining-day", RateLimitRemainingDay.ToString(CultureInfo.InvariantCulture));
                response.Headers.Add("X-RateLimit-Remaining-hour", RateLimitRemainingHour.ToString(CultureInfo.InvariantCulture));
                //response.Headers.Add("Date", DateTime.Now.AddSeconds(ResponseTime).ToString(CultureInfo.InvariantCulture));
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