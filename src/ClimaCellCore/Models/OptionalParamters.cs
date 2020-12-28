using System;

namespace ClimaCellCore
{
    /// <summary>
    ///     Optional paramaters for a climacell request made with a <see cref="ClimaCellService"/> instance.
    /// </summary>
    public class OptionalParamters
    {
        /// <summary>
        ///     Start time for a Forecast request.
        /// </summary>
        public DateTime? ForecastStartTime { get; set; }

        /// <summary>
        ///     End time for a Forecast request.
        /// </summary>
        public DateTime? ForecastEndTime { get; set; }

        /// <summary>
        ///     Time step between each data point for a <see cref="Nowcast"/> response, in minutes (1-60).
        /// </summary>
        /// <remarks>Only usable for a <see cref="Nowcast"/>request.</remarks>
        public int? NowcastTimeStep { get; set; }

        /// <summary>
        ///     Returns request field data in the requested unit system.
        ///     By default climacell uses 'si' if a unit system isn't specified.
        /// <list type="bullet">
        ///     <item>
        ///         <term>si</term>
        ///         <description>Metric system.</description>
        ///     </item>        
        ///     <item>
        ///         <term>us</term>
        ///         <description>US Imperial system</description>
        ///     </item>
        /// </list>
        /// </summary>
        public string UnitSystem { get; set; }
    }
}