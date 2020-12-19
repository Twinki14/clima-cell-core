using Newtonsoft.Json;

namespace ClimaCellCore.Models
{
    public class PollenTree
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class PollenWeed
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    public class PollenGrass
    {
        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }
}
