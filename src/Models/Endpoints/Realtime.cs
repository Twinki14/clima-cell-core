namespace ClimaCellCore.Models
{
    using Newtonsoft.Json;

    public class Realtime : ClimaCellResponse
    {
        [JsonProperty("temp")]
        public Temp temp { get; set; }

        [JsonProperty("feels_like")]
        public FeelsLike feelsLike { get; set; }

        [JsonProperty("dewpoint")]
        public Dewpoint dewpoint { get; set; }

        [JsonProperty("humidity")]
        public Humidity humidity { get; set; }

        [JsonProperty("wind_speed")]
        public WindSpeed windSpeed { get; set; }

        [JsonProperty("wind_direction")]
        public WindSpeed windDirection { get; set; }

        [JsonProperty("wind_gust")]
        public WindGust windGust{ get; set; }

        [JsonProperty("baro_pressure")]
        public BaroPressure baroPressure { get; set; }

        [JsonProperty("precipitation")]
        public Precipitation precipitation { get; set; }

        [JsonProperty("precipitation_type")]
        public PrecipitationType precipitationType { get; set; }

        [JsonProperty("precipitation_probability")]
        public PrecipitationProbability precipitationProbability { get; set; }

        [JsonProperty("precipitation_accumulation")]
        public PrecipitationAccumulation precipitationAccumulation { get; set; }

        [JsonProperty("sunrise")]
        public Sunrise sunrise { get; set; }

        [JsonProperty("sunset")]
        public Sunset sunset { get; set; }

        [JsonProperty("visibility")]
        public Visibility visibility { get; set; }

        [JsonProperty("cloud_cover")]
        public CloudCover cloudCover { get; set; }

        [JsonProperty("cloud_base")]
        public CloudBase cloudBase { get; set; }

        [JsonProperty("cloud_ceiling")]
        public CloudCeiling cloudCeiling { get; set; }

        [JsonProperty("surface_shortwave_radiation")]
        public SurfaceShortwaveRadiation surfaceShortwaveRadiation { get; set; }

        [JsonProperty("moon_phase")]
        public MoonPhase moonPhase { get; set; }

        [JsonProperty("weather_code")]
        public WeatherCode weatherCode { get; set; }

        [JsonProperty("observation_time")]
        public ObservationTime observationTime { get; set; }
    }
}
