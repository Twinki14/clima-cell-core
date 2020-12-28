using System;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using Xunit;
using ClimaCellCore.Services;
using ClimaCellCore.Tests.Fixtures;

namespace ClimaCellCore.Tests
{
    public class ClimaCellServiceTests : IClassFixture<ResponseFixture>
    {
        private readonly ResponseFixture _fixture;

        public ClimaCellServiceTests(ResponseFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("	")]
        [InlineData("  ")]
        public void Constructor_MissingAPIKey(string key)
        {
            Assert.ThrowsAny<ArgumentException>(() => new ClimaCellService(key));
        }

        [Fact]
        public void Constructor_WithAPIKey()
        {
            using var service = new ClimaCellService("ABCD1234");
            Assert.NotNull(service);
        }

        [Fact]
        public async void Realtime_AtleaseOneFieldIsRequired()
        {
            using var service = new ClimaCellService("ABCD1234");
            await Assert.ThrowsAsync<ArgumentException>(async () => await service.GetRealtime(42, -71, new List<string>()));
        }

        [Fact]
        public async void Nowcast_AtleaseOneFieldIsRequired()
        {
            using var service = new ClimaCellService("ABCD1234");
            await Assert.ThrowsAsync<ArgumentException>(async () => await service.GetNowcast(42, -71, new List<string>()));
        }

        [Fact]
        public async void Hourly_AtleaseOneFieldIsRequired()
        {
            using var service = new ClimaCellService("ABCD1234");
            await Assert.ThrowsAsync<ArgumentException>(async () => await service.GetHourly(42, -71, new List<string>()));
        }

        [Fact]
        public async void Daily_AtleaseOneFieldIsRequired()
        {
            using var service = new ClimaCellService("ABCD1234");
            await Assert.ThrowsAsync<ArgumentException>(async () => await service.GetDaily(42, -71, new List<string>()));
        }

        [Fact]
        public void BuildRequestUri_ShouldMatch()
        {
            const string baseUri = @"https://api.climacell.co/v3/weather/";

            var apiKey = "ABCD1234";
            var fields = new List<string> { "temp", "feels_like", "dewpoint" };

            var fieldString = Uri.EscapeUriString(String.Join(",", fields));
            var expectedQueryString = new StringBuilder($"realtime?");

            expectedQueryString.Append($"lat={ResponseFixture.Latitude:N4}&lon={ResponseFixture.Longitude:N4}");
            expectedQueryString.Append($"&fields={fieldString}");
            expectedQueryString.Append($"&apikey={apiKey}");
            expectedQueryString.Append($"&start_time=2019-03-20T14:09:50");
            expectedQueryString.Append($"&timestep=10");
            expectedQueryString.Append($"&unit_system=us");

            var totalQuery = $"{baseUri}{expectedQueryString}";

            var queryCheckClient = new Mock<IHttpClient>();
            queryCheckClient.Setup(f => f.HttpRequestAsync(It.IsAny<string>())).Returns((string s) => Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = s
            }));

            using var serviceQueryCheck = new ClimaCellService(apiKey, new Uri(baseUri), queryCheckClient.Object);
            var parameters = new OptionalParamters
            {
                ForecastStartTime = DateTime.Parse("2019-03-20T14:09:50Z"),
                NowcastTimeStep = 10,
                UnitSystem = "us"
            };

            var result = serviceQueryCheck.GetRealtime(ResponseFixture.Latitude, ResponseFixture.Longitude, fields, parameters).Result;

            Assert.Equal(totalQuery, result.Response.ReasonPhrase);
            Assert.True(result.Response.IsSuccessStatus);
            Assert.False(string.IsNullOrWhiteSpace(result.Response.DataSource));
            Assert.False(string.IsNullOrWhiteSpace(result.Response.AttributionLine));
        }
    }
}
