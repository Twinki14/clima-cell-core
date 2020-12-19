using System;
using System.Net.Http;
using ClimaCellCore.Services;
using System.Threading.Tasks;

namespace ClimaCellCore.Models
{
    public abstract class RealtimeResponse
    {
        public ClimaCellResponse Response { get; set; }
        public static Task Deserialize(HttpResponseMessage responseMessage, IJsonSerializerService jsonSerializerService)
            => throw new NotImplementedException();
    }
}