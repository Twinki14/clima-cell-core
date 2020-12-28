using Xunit;
using ClimaCellCore.Tests.Fixtures;

namespace ClimaCellCore.Tests.UnitTests
{
    public class DailyResponseTests : IClassFixture<ResponseFixture>
    {
        private readonly ResponseFixture _fixture;

        public DailyResponseTests(ResponseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Validate_AllDataPointsPopulated()
        {
            var response = _fixture.NormalDailyResponse;

            Assert.All(response.DataPoints, data =>
                {
                    Assert.NotNull(data.Temp);
                    Assert.NotNull(data.PrecipitationAccumulation);
                    Assert.NotNull(data.Precipitation);
                    Assert.NotNull(data.PrecipitationProbability);
                    Assert.NotNull(data.FeelsLike);
                    Assert.NotNull(data.Humidity);
                    Assert.NotNull(data.BaroPressure);
                    Assert.NotNull(data.Dewpoint);
                    Assert.NotNull(data.WindSpeed);
                    Assert.NotNull(data.WindDirection);
                    Assert.NotNull(data.Visibility);
                    Assert.NotNull(data.Sunrise);
                    Assert.NotNull(data.Sunset);
                    Assert.NotNull(data.WeatherCode);
                    Assert.NotNull(data.ObservationTime);
                }
            );
        }

        [Fact]
        public void Validate_ExpectedNumberOfDataPoints()
        {
            var response = _fixture.NormalDailyResponse;

            Assert.NotNull(response.DataPoints);
            Assert.NotEmpty(response.DataPoints);
            Assert.Equal(response.DataPoints.Count, ResponseFixture.DailyDataPoints);
        }
    }
}