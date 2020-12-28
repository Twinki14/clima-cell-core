using System;
using Xunit;
using ClimaCellCore.Tests.Fixtures;

namespace ClimaCellCore.Tests.UnitTests
{
    public class RealtimeResponseTests : IClassFixture<ResponseFixture>
    {
        private readonly ResponseFixture _fixture;

        public RealtimeResponseTests(ResponseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Validate_AllKnownFieldsPopulated()
        {
            var response = _fixture.NormalRealtimeResponse;

            Assert.NotNull(response.Temp);
            Assert.NotNull(response.FeelsLike);
            Assert.NotNull(response.Dewpoint);
            Assert.NotNull(response.WindSpeed);
            Assert.NotNull(response.WindGust);
            Assert.NotNull(response.BaroPressure);
            Assert.NotNull(response.Visibility);
            Assert.NotNull(response.Humidity);
            Assert.NotNull(response.WindDirection);
            Assert.NotNull(response.Precipitation);
            Assert.NotNull(response.PrecipitationType);
            Assert.NotNull(response.CloudCover);
            Assert.NotNull(response.CloudCeiling);
            Assert.NotNull(response.CloudBase);
            Assert.NotNull(response.SurfaceShortwaveRadiation);
            Assert.NotNull(response.Sunrise);
            Assert.NotNull(response.Sunset);
            Assert.NotNull(response.MoonPhase);
            Assert.NotNull(response.WeatherCode);
            Assert.NotNull(response.EpaAQI);
            Assert.NotNull(response.EpaPrimaryPollutant);
            Assert.NotNull(response.ChinaAQI);
            Assert.NotNull(response.ChinaPrimaryPollutant);
            Assert.NotNull(response.Pm25);
            Assert.NotNull(response.Pm10);
            Assert.NotNull(response.O3);
            Assert.NotNull(response.NO2);
            Assert.NotNull(response.CO);
            Assert.NotNull(response.SO2);
            Assert.NotNull(response.EpaHealthConcern);
            Assert.NotNull(response.ChinaHealthConcern);
            Assert.NotNull(response.PollenTree);
            Assert.NotNull(response.PollenWeed);
            Assert.NotNull(response.ObservationTime);

            Assert.Equal(response.Latitude, ResponseFixture.Latitude);
            Assert.Equal(response.Longitude, ResponseFixture.Longitude);

            Assert.Equal(response.ObservationTime.Value, ResponseFixture.RealtimeObservationTime);
        }
    }
}
