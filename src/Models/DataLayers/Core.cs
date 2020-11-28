using System;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ClimaCellCore.Models
{
    /// <summary>
    ///     ClimaCellCore api 'Core' data layers
    /// </summary>
    [Flags]
    public enum Core
    {
        [EnumMember(Value = "temp")]               Temp              = 1 << 0,
        [EnumMember(Value = "feels_like")]         FeelsLike         = 1 << 1,
        [EnumMember(Value = "dewpoint")]           Dewpoint          = 1 << 2,
        [EnumMember(Value = "wind_speed")]         WindSpeed         = 1 << 3,
        [EnumMember(Value = "wind_gust")]          WindGust          = 1 << 4,
        [EnumMember(Value = "baro_pressure")]      BaroPressure      = 1 << 5,
        [EnumMember(Value = "visibility")]         Visibility        = 1 << 6,
        [EnumMember(Value = "humidity")]           Humidity          = 1 << 7,
        [EnumMember(Value = "wind_direction")]     WindDirection     = 1 << 8,
        [EnumMember(Value = "precipitation")]      Precipitation     = 1 << 9,
        [EnumMember(Value = "precipitation_type")] PrecipitationType = 1 << 10,
        [EnumMember(Value = "cloud_cover")]        CloudCover        = 1 << 11,
        [EnumMember(Value = "sunrise")]            Sunrise           = 1 << 12,
        [EnumMember(Value = "sunset")]             Sunset            = 1 << 13,
        [EnumMember(Value = "moon_phase")]         MoonPhase         = 1 << 14,
        [EnumMember(Value = "weather_code")]       WeatherCode       = 1 << 15,
    }

    public class Temp
    {
        [JsonProperty("value")]
        public float Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class FeelsLike
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class Dewpoint
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class WindSpeed
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class WindGust
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class BaroPressure
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class Visibility
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class Humidity
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class WindDirection
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class Precipitation
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class PrecipitationType
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class PrecipitationProbability
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class CloudCover
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class Sunrise
    {
        [JsonProperty("value")]
        public DateTime Value { get; set; }
    }

    public class Sunset
    {
        [JsonProperty("value")]
        public DateTime Value { get; set; }
    }

    public class MoonPhase
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class WeatherCode
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class ObservationTime
    {
        [JsonProperty("value")]
        public DateTime Value { get; set; }
    }
}
