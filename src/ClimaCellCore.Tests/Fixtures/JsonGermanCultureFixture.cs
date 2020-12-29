using System.Globalization;
using System.Threading.Tasks;
using ClimaCellCore.Services;
using Newtonsoft.Json;

namespace ClimaCellCore.Tests.Fixtures
{
    class JsonGermanCultureFixture : IJsonSerializerService
    {
        public async Task<T> DeserializeJsonAsync<T>(Task<string> json)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                Culture = new CultureInfo("de-DE")
            };

            return JsonConvert.DeserializeObject<T>(await json.ConfigureAwait(false), jsonSettings);
        }
    }
}