﻿using System;
using Newtonsoft.Json;

namespace ClimaCellCore.Models
{
    public class Temp
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class FeelsLike
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class Dewpoint
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class Humidity
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class WindSpeed
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class WindDirection
    {
        [JsonProperty("value")]
        public double Value { get; set; }

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
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class Precipitation
    {
        [JsonProperty("value")]
        public double Value { get; set; }

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
        
        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class PrecipitationAccumulation
    {
        [JsonProperty("value")]
        public double Value { get; set; }

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

    public class Visibility
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class CloudCover
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class CloudBase
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class CloudCeiling
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class SurfaceShortwaveRadiation
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
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