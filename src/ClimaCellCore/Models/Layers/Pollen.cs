using Newtonsoft.Json;

namespace ClimaCellCore.Models
{
    /// <summary>ClimaCell pollen index for trees.</summary>
    public class PollenTree
    {
        [JsonProperty("value")]
        public double? Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>ClimaCell pollen index for weeds.</summary>
    public class PollenWeed
    {
        [JsonProperty("value")]
        public double? Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }

    /// <summary>ClimaCell pollen index for grass.</summary>
    public class PollenGrass
    {
        [JsonProperty("value")]
        public double? Value { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }
    }
}
