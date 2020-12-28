using Xunit;
using ClimaCellCore.Tests.Fixtures;

namespace ClimaCellCore.Tests.UnitTests
{
    public class ResponseHeadersTest : IClassFixture<ResponseFixture>
    {
        private readonly ResponseFixture _fixture;

        public ResponseHeadersTest(ResponseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Realtime_HeadersMatch()
        {
            var response = _fixture.NormalRealtimeResponse;

            Assert.NotNull(response.Response);
            Assert.Equal(response.Response.RateLimitLimitDay, ResponseFixture.RateLimitLimitDay);
            Assert.Equal(response.Response.RateLimitLimitHour, ResponseFixture.RateLimitLimitHour);
            Assert.Equal(response.Response.RateLimitRemainingDay, ResponseFixture.RateLimitRemainingDay);
            Assert.Equal(response.Response.RateLimitRemainingHour, ResponseFixture.RateLimitRemainingHour);
        }

        [Fact]
        public void Nowcast_HeadersMatch()
        {
            var response = _fixture.NormalNowcastResponse;

            Assert.NotNull(response.Response);
            Assert.Equal(response.Response.RateLimitLimitDay, ResponseFixture.RateLimitLimitDay);
            Assert.Equal(response.Response.RateLimitLimitHour, ResponseFixture.RateLimitLimitHour);
            Assert.Equal(response.Response.RateLimitRemainingDay, ResponseFixture.RateLimitRemainingDay);
            Assert.Equal(response.Response.RateLimitRemainingHour, ResponseFixture.RateLimitRemainingHour);
        }

        [Fact]
        public void Hourly_HeadersMatch()
        {
            var response = _fixture.NormalHourlyResponse;

            Assert.NotNull(response.Response);
            Assert.Equal(response.Response.RateLimitLimitDay, ResponseFixture.RateLimitLimitDay);
            Assert.Equal(response.Response.RateLimitLimitHour, ResponseFixture.RateLimitLimitHour);
            Assert.Equal(response.Response.RateLimitRemainingDay, ResponseFixture.RateLimitRemainingDay);
            Assert.Equal(response.Response.RateLimitRemainingHour, ResponseFixture.RateLimitRemainingHour);
        }

        [Fact]
        public void Daily_HeadersMatch()
        {
            var response = _fixture.NormalDailyResponse;

            Assert.NotNull(response.Response);
            Assert.Equal(response.Response.RateLimitLimitDay, ResponseFixture.RateLimitLimitDay);
            Assert.Equal(response.Response.RateLimitLimitHour, ResponseFixture.RateLimitLimitHour);
            Assert.Equal(response.Response.RateLimitRemainingDay, ResponseFixture.RateLimitRemainingDay);
            Assert.Equal(response.Response.RateLimitRemainingHour, ResponseFixture.RateLimitRemainingHour);
        }

        [Fact]
        public void BadRequest_NullHeaders()
        {
            var response = _fixture.BadRequestRealtimeResponse;

            Assert.NotNull(response.Response);
            Assert.Null(response.Response.RateLimitLimitDay);
            Assert.Null(response.Response.RateLimitLimitHour);
            Assert.Null(response.Response.RateLimitRemainingDay);
            Assert.Null(response.Response.RateLimitRemainingHour);
        }

        [Fact]
        public void Unauthorized_NullHeaders()
        {
            var response = _fixture.UnauthorizedRealtimeResponse;

            Assert.NotNull(response.Response);
            Assert.Null(response.Response.RateLimitLimitDay);
            Assert.Null(response.Response.RateLimitLimitHour);
            Assert.Null(response.Response.RateLimitRemainingDay);
            Assert.Null(response.Response.RateLimitRemainingHour);
        }
    }
}