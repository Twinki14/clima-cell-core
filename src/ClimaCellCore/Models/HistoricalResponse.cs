using System;
using System.Net.Http;
using System.Collections.Generic;
using ClimaCellCore.Services;
using System.Threading.Tasks;

namespace ClimaCellCore.Models
{
    public abstract class HistoricalResponse<T>
    {
        public ClimaCellResponse Response { get; set; }
        public IList<T> Objects { get { return _objects; } }
        private protected List<T> _objects { get; set; }

        public static Task Deserialize(HttpResponseMessage responseMessage, IJsonSerializerService jsonSerializerService)
            => throw new NotImplementedException();
    }
}