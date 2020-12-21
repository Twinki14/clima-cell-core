using System;
using System.Net.Http;
using System.Collections.Generic;
using ClimaCellCore.Services;
using System.Threading.Tasks;

namespace ClimaCellCore.Models
{
    /// <summary>
    ///     A subclass to assist with defining and implmenting classes for climacells 'Forcast' type responses.
    /// </summary>
    /// <typeparam name="T">The parent classes weather datapoint model to use when deserializing and storing in a collection.</typeparam>
    public abstract class ForecastResponse<T>
    {
        /// <summary>
        ///     Useful httpResponse and climacell metadata from a climacell response. See <see cref="ClimaCellResponse"/>.
        /// </summary>
        public ClimaCellResponse Response { get; set; }

        /// <summary>
        ///     Public collection of the observed weather datapoints returned by a climacell forecast request.
        /// </summary>
        public IList<T> DataPoints { get { return _dataPoints; } }
        private protected List<T> _dataPoints { get; set; }

        /// <summary>
        ///     Attempts to deserialize and initialize the parent class with the <see cref="HttpResponseMessage"/> using the 
        ///         <see cref="IJsonSerializerService"/> defined in the calling <see cref="ClimaCellService"/> instance.
        /// </summary>
        public static Task Deserialize(HttpResponseMessage responseMessage, IJsonSerializerService jsonSerializerService)
            => throw new NotImplementedException();
    }
}