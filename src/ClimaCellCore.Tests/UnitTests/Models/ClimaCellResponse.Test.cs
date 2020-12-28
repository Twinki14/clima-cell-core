using Xunit;
using ClimaCellCore.Models;
using System.Net.Http;

namespace ClimaCellCore.Tests.UnitTests
{
    public class ClimaCellResponseTests
    {
        [Fact]
        public void Constructor_Default_ShouldHaveData()
        {
            var response = new ClimaCellResponse();

            Assert.False(string.IsNullOrWhiteSpace(response.DataSource));
            Assert.False(string.IsNullOrWhiteSpace(response.AttributionLine));
        }

        [Fact]
        public void Constructor_Response_EmptyResponseMessageShouldHaveData()
        {
            var response = new ClimaCellResponse(new HttpResponseMessage());

            Assert.False(string.IsNullOrWhiteSpace(response.DataSource));
            Assert.False(string.IsNullOrWhiteSpace(response.AttributionLine));
        }
    }
}