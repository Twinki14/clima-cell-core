using System;
using System.Threading.Tasks;
using ClimaCellCore.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ClimaCellCore.Tests.Fixtures
{
    class JsonMissingMemberFixture : IJsonSerializerService
    {
        // Force all json props to be required
        private class RequireObjectPropertiesContractResolver : DefaultContractResolver
        {
            protected override JsonObjectContract CreateObjectContract(Type objectType)
            {
                var contract = base.CreateObjectContract(objectType);
                contract.ItemRequired = Required.AllowNull;
                return contract;
            }
        }

        public async Task<T> DeserializeJsonAsync<T>(Task<string> json)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error,
                ContractResolver = new RequireObjectPropertiesContractResolver()
            };

            return JsonConvert.DeserializeObject<T>(await json.ConfigureAwait(false), jsonSettings);
        }
    }
}