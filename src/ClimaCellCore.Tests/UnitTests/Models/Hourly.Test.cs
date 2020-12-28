using Xunit;
using ClimaCellCore.Tests.Fixtures;

namespace ClimaCellCore.Tests.UnitTests
{
    public class HourlyResponseTests : IClassFixture<ResponseFixture>
    {
        private readonly ResponseFixture _fixture;

        public HourlyResponseTests(ResponseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Validate_AllDataPointsPopulated()
        {
            var response = _fixture.NormalHourlyResponse;

            Assert.All(response.DataPoints, data =>
                {
                    Assert.NotNull(data.Temp);
                    Assert.NotNull(data.Precipitation);
                    Assert.NotNull(data.PrecipitationType);
                    Assert.NotNull(data.PrecipitationProbability);
                    Assert.NotNull(data.FeelsLike);
                    Assert.NotNull(data.Humidity);
                    Assert.NotNull(data.BaroPressure);
                    Assert.NotNull(data.Dewpoint);
                    Assert.NotNull(data.WindSpeed);
                    Assert.NotNull(data.WindGust);
                    Assert.NotNull(data.WindDirection);
                    Assert.NotNull(data.Visibility);
                    Assert.NotNull(data.CloudCover);
                    Assert.NotNull(data.CloudCeiling);
                    Assert.NotNull(data.CloudBase);
                    Assert.NotNull(data.SurfaceShortwaveRadiation);
                    Assert.NotNull(data.Sunrise);
                    Assert.NotNull(data.Sunset);
                    Assert.NotNull(data.MoonPhase);
                    Assert.NotNull(data.WeatherCode);
                    Assert.NotNull(data.ObservationTime);
                    Assert.NotNull(data.EpaAQI);
                    Assert.NotNull(data.EpaPrimaryPollutant);
                    Assert.NotNull(data.ChinaAQI);
                    Assert.NotNull(data.ChinaPrimaryPollutant);
                    Assert.NotNull(data.Pm25);
                    Assert.NotNull(data.Pm10);
                    Assert.NotNull(data.O3);
                    Assert.NotNull(data.NO2);
                    Assert.NotNull(data.CO);
                    Assert.NotNull(data.SO2);
                    Assert.NotNull(data.EpaHealthConcern);
                    Assert.NotNull(data.ChinaHealthConcern);
                    Assert.NotNull(data.PollenTree);
                    Assert.NotNull(data.PollenWeed);
                    Assert.NotNull(data.PollenGrass);
                }
            );
        }

        [Fact]
        public void Validate_ExpectedNumberOfDataPoints()
        {
            var response = _fixture.NormalHourlyResponse;

            Assert.NotNull(response.DataPoints);
            Assert.NotEmpty(response.DataPoints);
            Assert.Equal(response.DataPoints.Count, ResponseFixture.HourlyDataPoints);
        }
    }
}