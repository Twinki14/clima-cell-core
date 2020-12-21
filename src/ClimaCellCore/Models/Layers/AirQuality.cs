using Newtonsoft.Json;

namespace ClimaCellCore.Models
{
    /// <summary>Particulate Matter < 2.5 μm.</summary>
    public class Pm25
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Particulate Matter < 10 μm.</summary>
    public class Pm10
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Ozone.</summary>
    public class O3
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Nitrogen Dioxide.</summary>
    public class NO2
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Carbon Monoxide.</summary>
    public class CO
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Sulfur Dioxide.</summary>
    public class SO2
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>Air quality index per US EPA standard.</summary>
    public class EpaAQI
    {
        [JsonProperty("value")]
        public double Value { get; set; }
    }

    /// <summary>Primary pollutant per US EPA standard.</summary>
    public class EpaPrimaryPollutant
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    /// <summary>Health concern level based on EPA standard.</summary>
    public class EpaHealthConcern
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    /// <summary>Air quality index per China MEP standard.</summary>
    public class ChinaAQI
    {
        [JsonProperty("value")]
        public double Value { get; set; }
    }

    /// <summary>Primary pollutant per China MEP standard.</summary>
    public class ChinaPrimaryPollutant
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    /// <summary>Health concern level based on China MEP standard.</summary>
    public class ChinaHealthConcern
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
