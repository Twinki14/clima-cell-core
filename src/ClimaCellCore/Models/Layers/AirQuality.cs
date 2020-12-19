namespace ClimaCellCore.Models
{
    using Newtonsoft.Json;

    public class Pm25
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class Pm10
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }    
    
    public class O3
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class NO2
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class CO
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class SO2
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class EpaAQI
    {
        [JsonProperty("value")]
        public double Value { get; set; }
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
        public double Value { get; set; }
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
