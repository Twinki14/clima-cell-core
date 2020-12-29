using System;
using System.Net.Http;
using ClimaCellCore.Services;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClimaCellCore.Models
{
    /// <summary>
    ///     A subclass to assist with defining and implmenting classes for climacells 'Present' type responses.
    /// </summary>
    public abstract class PresentResponse
    {
        /// <summary>
        ///     Useful httpResponse and climacell metadata from a climacell response. See <see cref="ClimaCellResponse"/>.
        /// </summary>
        [JsonIgnore]
        public ClimaCellResponse Response { get; set; }

        /// <summary>
        ///     Attempts to deserialize and initialize the parent class with the <see cref="HttpResponseMessage"/> using the 
        ///         <see cref="IJsonSerializerService"/> defined in the calling <see cref="ClimaCellService"/> instance.
        /// </summary>
        public static Task Deserialize(HttpResponseMessage responseMessage, IJsonSerializerService jsonSerializerService)
            => throw new NotImplementedException();
    }
}