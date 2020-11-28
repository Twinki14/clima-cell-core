namespace ClimaCellCore.Models
{
    using System;
    using Newtonsoft.Json;
    using System.Runtime.Serialization;

    /// <summary>
    ///     ClimaCellCore api 'AirQuality' data layers
    /// </summary>
    [Flags]
    public enum AirQuality
    {
        [EnumMember(Value = "pm25")]                    Pm25                  = 1 << 0,    /// <summary> </summary>
        [EnumMember(Value = "pm10")]                    Pm10                  = 1 << 1,    /// <summary> </summary>
        [EnumMember(Value = "o3")]                      O3                    = 1 << 2,    /// <summary> </summary>
        [EnumMember(Value = "no2")]                     NO2                   = 1 << 3,    /// <summary> </summary>
        [EnumMember(Value = "co")]                      CO                    = 1 << 4,    /// <summary> </summary>
        [EnumMember(Value = "so2")]                     SO2                   = 1 << 5,    /// <summary> </summary>
        [EnumMember(Value = "epa_aqi")]                 EpaAQI                = 1 << 6,    /// <summary> </summary>
        [EnumMember(Value = "epa_primary_pollutant")]   EpaPrimaryPollutant   = 1 << 7,    /// <summary> </summary>
        [EnumMember(Value = "epa_health_concern")]      EpaHealthConcern      = 1 << 8,    /// <summary> </summary>
        [EnumMember(Value = "china_aqi")]               ChinaAQI              = 1 << 9,    /// <summary> </summary>
        [EnumMember(Value = "china_primary_pollutant")] ChinaPrimaryPollutant = 1 << 10,   /// <summary> </summary>
        [EnumMember(Value = "china_health_concern")]    ChinaHealthConcern    = 1 << 11,   /// <summary> </summary>
    }

    public class Pm25
    {
        [JsonProperty("value")]
        public float Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class Pm10
    {
        [JsonProperty("value")]
        public float Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }    
    
    public class O3
    {
        [JsonProperty("value")]
        public float Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class NO2
    {
        [JsonProperty("value")]
        public float Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class CO
    {
        [JsonProperty("value")]
        public float Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class SO2
    {
        [JsonProperty("value")]
        public float Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class EpaAQI
    {
        [JsonProperty("value")]
        public int Value { get; set; }
    }

    public class EpaPrimaryPollutant
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class EpaHealthConcern
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class ChinaAQI
    {
        [JsonProperty("value")]
        public float Value { get; set; }
    }

    public class ChinaPrimaryPollutant
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class ChinaHealthConcern
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
