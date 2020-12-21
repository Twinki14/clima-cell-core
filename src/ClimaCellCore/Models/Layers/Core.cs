using System;
using Newtonsoft.Json;

namespace ClimaCellCore.Models
{
    /// <summary>Temperature.</summary>
    public class Temp
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Wind chill and heat window based on season.</summary>
    public class FeelsLike
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Temperature of the dew point.</summary>
    public class Dewpoint
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Percent relative humidity from 0 - 100%.</summary>
    public class Humidity
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Wind speed.</summary>
    public class WindSpeed
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Wind direction in polar degrees 0-360 where 0 is North.</summary>
    public class WindDirection
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Wind gust speed.</summary>
    public class WindGust
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Barometric pressure (MSL mean sea level).</summary>
    public class BaroPressure
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Precipitation intensity.</summary>
    public class Precipitation
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>The type of precipitation.</summary>
    public class PrecipitationType
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    /// <summary>The chance that precipitation will occur at the forecast time within the hour or day.</summary>
    public class PrecipitationProbability
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>The accumulated amount of precipitation in the selected timestep.</summary>
    public class PrecipitationAccumulation
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>The times sunrise based on location.</summary>
    public class Sunrise
    {
        [JsonProperty("value")]
        public DateTime Value { get; set; }
    }

    /// <summary>The times sunset based on location.</summary>
    public class Sunset
    {
        [JsonProperty("value")]
        public DateTime Value { get; set; }
    }

    /// <summary>Visibility distance.</summary>
    public class Visibility
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Fraction of the sky obscured by clouds.</summary>
    public class CloudCover
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>The lowest level at which the air contains a perceptible quantity of cloud particles.</summary>
    public class CloudBase
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>The height of the lowest layer of clouds which covers more than half of the sky.</summary>
    public class CloudCeiling
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Solar radiation reaching the surface.</summary>
    public class SurfaceShortwaveRadiation
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>The shape of the directly sunlit portion of the Moon.</summary>
    public class MoonPhase
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    /// <summary>A textual field that conveys the weather conditions.</summary>
    public class WeatherCode
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    /// <summary>The times the data was observed.</summary>
    public class ObservationTime
    {
        [JsonProperty("value")]
        public DateTime Value { get; set; }
    }
}
