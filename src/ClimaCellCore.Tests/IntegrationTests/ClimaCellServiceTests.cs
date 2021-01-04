using Xunit;
using System;
using ClimaCellCore.Tests.Fixtures;
using ClimaCellCore.Tests.Attributes;

namespace ClimaCellCore.Tests.IntegrationTests
{
    [Trait("Category", "SkipWhenLiveUnitTesting")]
    [Trait("Category", "IntegrationTests")]
    public sealed class ClimaCellServiceIntegrationTests : IDisposable
    {
        private const string _apiEnvVar = "CLIMACELL_API_KEY";
        private string _apiKey = null;
        private const double _latitude = 42.3026;
        private const double _longitude = -71.1760;
        private readonly ClimaCellService _service;
        private bool disposedValue;

        public ClimaCellServiceIntegrationTests()
        {
            _apiKey = Environment.GetEnvironmentVariable(_apiEnvVar);
            Assert.False(string.IsNullOrWhiteSpace(_apiKey), $"You must set the environment variable {_apiEnvVar}");
            _service = new ClimaCellService(_apiKey, jsonSerializerService: new JsonMissingMemberFixture());
        }

        [SkipIfNotRequested]
        public async void Realtime_NotMissingMembers()
        {
            var resp = await _service.GetRealtime(_latitude, _longitude, ResponseFixture.RealtimeFields, new OptionalParamters { UnitSystem = "us" });
            Assert.True(resp.Response.IsSuccessStatus);
            Assert.Equal("F", resp.Temp.Units);
        }

        [SkipIfNotRequested]
        public async void Realtime_JsonGermanCulture()
        {
            using var cultureJsonService = new ClimaCellService(_apiKey, jsonSerializerService: new JsonGermanCultureFixture());
            var resp = await cultureJsonService.GetRealtime(_latitude, _longitude, ResponseFixture.RealtimeFields, new OptionalParamters { UnitSystem = "si" });
            Assert.True(resp.Response.IsSuccessStatus);
            Assert.Equal("C", resp.Temp.Units);
        }

        [SkipIfNotRequested]
        public async void Nowcast_NotMissingMembers()
        {
            var resp = await _service.GetNowcast(_latitude, _longitude, ResponseFixture.NowcastFields, new OptionalParamters { NowcastTimeStep = 5, UnitSystem = "us" });

            Assert.True(resp.Response.IsSuccessStatus);
            Assert.NotEmpty(resp.DataPoints);
            Assert.Equal("F", resp.DataPoints[0].Temp.Units);
        }

        [SkipIfNotRequested]
        public async void Nowcast_JsonGermanCulture()
        {
            using var cultureJsonService = new ClimaCellService(_apiKey, jsonSerializerService: new JsonGermanCultureFixture());
            var resp = await cultureJsonService.GetNowcast(_latitude, _longitude, ResponseFixture.NowcastFields, new OptionalParamters { UnitSystem = "si" });

            Assert.True(resp.Response.IsSuccessStatus);
            Assert.NotEmpty(resp.DataPoints);
            Assert.Equal("C", resp.DataPoints[0].Temp.Units);
        }

        [SkipIfNotRequested]
        public async void Hourly_NotMissingMembers()
        {
            var resp = await _service.GetHourly(_latitude, _longitude, ResponseFixture.HourlyFields, new OptionalParamters { UnitSystem = "us" });

            Assert.True(resp.Response.IsSuccessStatus);
            Assert.NotEmpty(resp.DataPoints);
            Assert.Equal("F", resp.DataPoints[0].Temp.Units);
        }

        [SkipIfNotRequested]
        public async void Hourly_JsonGermanCulture()
        {
            using var cultureJsonService = new ClimaCellService(_apiKey, jsonSerializerService: new JsonGermanCultureFixture());
            var resp = await cultureJsonService.GetHourly(_latitude, _longitude, ResponseFixture.HourlyFields, new OptionalParamters { UnitSystem = "si" });

            Assert.True(resp.Response.IsSuccessStatus);
            Assert.NotEmpty(resp.DataPoints);
            Assert.Equal("C", resp.DataPoints[0].Temp.Units);
        }

        [SkipIfNotRequested]
        public async void Daily_NotMissingMembers()
        {
            var resp = await _service.GetDaily(_latitude, _longitude, ResponseFixture.DailyFields, new OptionalParamters { UnitSystem = "us" });

            Assert.True(resp.Response.IsSuccessStatus);
            Assert.NotEmpty(resp.DataPoints);
            Assert.Equal("F", resp.DataPoints[0].Temp.Min.Units);
        }

        [SkipIfNotRequested]
        public async void Daily_JsonGermanCulture()
        {
            using var cultureJsonService = new ClimaCellService(_apiKey, jsonSerializerService: new JsonGermanCultureFixture());
            var resp = await cultureJsonService.GetDaily(_latitude, _longitude, ResponseFixture.DailyFields, new OptionalParamters { UnitSystem = "si" });

            Assert.True(resp.Response.IsSuccessStatus);
            Assert.NotEmpty(resp.DataPoints);
            Assert.Equal("C", resp.DataPoints[0].Temp.Min.Units);
        }

        /// <summary>
        ///     Public access to start disposing of the class instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ClimaCellServiceIntegrationTests()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Dispose of resources used by the class.
        /// </summary>
        /// <param name="disposing">If the class is disposing managed resources.</param>
        private void Dispose(bool disposing)
        {
            if (disposedValue)
            {
                return;
            }

            if (disposing)
            {
                _service.Dispose();
            }

            disposedValue = true;
        }
    }
}