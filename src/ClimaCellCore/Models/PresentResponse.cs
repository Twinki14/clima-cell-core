using System;
using System.Net.Http;
using ClimaCellCore.Services;
using System.Threading.Tasks;

namespace ClimaCellCore.Models
{
    /// <summary>
    ///     A subclass to assist with defining and implmenting classes for climacells 'Present' responses.
    /// </summary>
    public abstract class PresentResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public ClimaCellResponse Response { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <param name="jsonSerializerService"></param>
        /// <returns></returns>
        public static Task Deserialize(HttpResponseMessage responseMessage, IJsonSerializerService jsonSerializerService)
            => throw new NotImplementedException();
    }
}