using System;
using System.Net.Http;
using System.Collections.Generic;
using ClimaCellCore.Services;
using System.Threading.Tasks;

namespace ClimaCellCore.Models
{
    /// <summary>
    ///     A subclass to assist with defining and implmenting classes for climacells 'Forcast' responses.
    /// </summary>
    /// <typeparam name="T">The parent classes response model to use when deserializing.</typeparam>
    public abstract class ForecastResponse<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public ClimaCellResponse Response { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public IList<T> DataPoints { get { return _dataPoints; } }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        private protected List<T> _dataPoints { get; set; }

        /// <summary>
        ///     Attempts to deserializes and initializes the parent class with the response content using the 
        ///         <see cref="IJsonSerializerService"/> defined in the calling <see cref="ClimaCellService"/> instance.
        /// </summary>
        public static Task Deserialize(HttpResponseMessage responseMessage, IJsonSerializerService jsonSerializerService)
            => throw new NotImplementedException();
    }
}