using Xunit;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using ClimaCellCore.Models;
using ClimaCellCore.Services;
using ClimaCellCore.Tests.Fixtures;

namespace ClimaCellCore.Tests.UnitTests
{
    public class ResponseUnitTests : IClassFixture<ResponseFixture>
    {
        private readonly ResponseFixture _fixture;

        public ResponseUnitTests(ResponseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CliamCellResponse_DefaultConstructorShouldHaveData()
        {
            var response = new ClimaCellResponse();

            Assert.False(string.IsNullOrWhiteSpace(response.DataSource));
            Assert.False(string.IsNullOrWhiteSpace(response.AttributionLine));
        }

        [Fact]
        public void CliamCellResponse_EmptyResponseMessageShouldHaveData()
        {
            var response = new ClimaCellResponse(new HttpResponseMessage());

            Assert.False(string.IsNullOrWhiteSpace(response.DataSource));
            Assert.False(string.IsNullOrWhiteSpace(response.AttributionLine));
        }

        [Fact]
        public async void Realtime_AllFieldsParsed()
        {
            var mockClient = new Mock<IHttpClient>();

            mockClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(ResponseFixture.MockRealtimeResponse));
            using (var service = new ClimaCellService("FakeKey", httpClient: mockClient.Object, jsonSerializerService: new JsonMissingMemberFixture()))
            {
                var resp = await service.GetRealtime(ResponseFixture.Latitude, ResponseFixture.Longitude, new List<string> { "mockField" });
                Assert.True(resp.Response.IsSuccessStatus);
            }
        }

        [Fact]
        public void Realtime_HeadersMatch()
        {
            var resp = _fixture.NormalRealtimeResponse;

            Assert.NotNull(resp.Response);
            Assert.Equal(resp.Response.RateLimitLimitDay, ResponseFixture.RateLimitLimitDay);
            Assert.Equal(resp.Response.RateLimitLimitHour, ResponseFixture.RateLimitLimitHour);
            Assert.Equal(resp.Response.RateLimitRemainingDay, ResponseFixture.RateLimitRemainingDay);
            Assert.Equal(resp.Response.RateLimitRemainingHour, ResponseFixture.RateLimitRemainingHour);
        }

        [Fact]
        public async void Nowcast_AllFieldsParsed()
        {
            var mockClient = new Mock<IHttpClient>();

            mockClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(ResponseFixture.MockNowcastResponse));
            using (var service = new ClimaCellService("FakeKey", httpClient: mockClient.Object, jsonSerializerService: new JsonMissingMemberFixture()))
            {
                var resp = await service.GetNowcast(ResponseFixture.Latitude, ResponseFixture.Longitude, new List<string> { "mockField" });
                Assert.True(resp.Response.IsSuccessStatus);
            }
        }

        [Fact]
        public void Nowcast_ExpectedNumberOfDataPoints()
        {
            var resp = _fixture.NormalNowcastResponse;

            Assert.NotNull(resp.DataPoints);
            Assert.NotEmpty(resp.DataPoints);
            Assert.Equal(resp.DataPoints.Count, ResponseFixture.NowcastDataPoints);
        }

        [Fact]
        public void Nowcast_HeadersMatch()
        {
            var resp = _fixture.NormalNowcastResponse;

            Assert.NotNull(resp.Response);
            Assert.Equal(resp.Response.RateLimitLimitDay, ResponseFixture.RateLimitLimitDay);
            Assert.Equal(resp.Response.RateLimitLimitHour, ResponseFixture.RateLimitLimitHour);
            Assert.Equal(resp.Response.RateLimitRemainingDay, ResponseFixture.RateLimitRemainingDay);
            Assert.Equal(resp.Response.RateLimitRemainingHour, ResponseFixture.RateLimitRemainingHour);
        }

        [Fact]
        public async void Hourly_AllFieldsParsed()
        {
            var mockClient = new Mock<IHttpClient>();

            mockClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(ResponseFixture.MockHourlyResponse));
            using (var service = new ClimaCellService("FakeKey", httpClient: mockClient.Object, jsonSerializerService: new JsonMissingMemberFixture()))
            {
                var resp = await service.GetHourly(ResponseFixture.Latitude, ResponseFixture.Longitude, new List<string> { "mockField" });
                Assert.True(resp.Response.IsSuccessStatus);
            }
        }

        [Fact]
        public void Hourly_ExpectedNumberOfDataPoints()
        {
            var resp = _fixture.NormalHourlyResponse;

            Assert.NotNull(resp.DataPoints);
            Assert.NotEmpty(resp.DataPoints);
            Assert.Equal(resp.DataPoints.Count, ResponseFixture.HourlyDataPoints);
        }

        [Fact]
        public void Hourly_HeadersMatch()
        {
            var resp = _fixture.NormalHourlyResponse;

            Assert.NotNull(resp.Response);
            Assert.Equal(resp.Response.RateLimitLimitDay, ResponseFixture.RateLimitLimitDay);
            Assert.Equal(resp.Response.RateLimitLimitHour, ResponseFixture.RateLimitLimitHour);
            Assert.Equal(resp.Response.RateLimitRemainingDay, ResponseFixture.RateLimitRemainingDay);
            Assert.Equal(resp.Response.RateLimitRemainingHour, ResponseFixture.RateLimitRemainingHour);
        }

        [Fact]
        public async void Daily_AllFieldsParsed()
        {
            var mockClient = new Mock<IHttpClient>();

            mockClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns(Task.FromResult(ResponseFixture.MockDailyResponse));
            using (var service = new ClimaCellService("FakeKey", httpClient: mockClient.Object, jsonSerializerService: new JsonMissingMemberFixture()))
            {
                var resp = await service.GetDaily(ResponseFixture.Latitude, ResponseFixture.Longitude, new List<string> { "mockField" });
                Assert.True(resp.Response.IsSuccessStatus);
            }
        }

        [Fact]
        public void Daily_ExpectedNumberOfDataPoints()
        {
            var resp = _fixture.NormalDailyResponse;

            Assert.NotNull(resp.DataPoints);
            Assert.NotEmpty(resp.DataPoints);
            Assert.Equal(resp.DataPoints.Count, ResponseFixture.DailyDataPoints);
        }

        [Fact]
        public void Daily_HeadersMatch()
        {
            var resp = _fixture.NormalDailyResponse;

            Assert.NotNull(resp.Response);
            Assert.Equal(resp.Response.RateLimitLimitDay, ResponseFixture.RateLimitLimitDay);
            Assert.Equal(resp.Response.RateLimitLimitHour, ResponseFixture.RateLimitLimitHour);
            Assert.Equal(resp.Response.RateLimitRemainingDay, ResponseFixture.RateLimitRemainingDay);
            Assert.Equal(resp.Response.RateLimitRemainingHour, ResponseFixture.RateLimitRemainingHour);
        }
    }
}