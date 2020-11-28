namespace ClimaCellCore.Models
{
    using Newtonsoft.Json;

    public class Realtime : ClimaCellResponse
    {
        [JsonProperty("temp")]
        public Temp Temp { get; set; }

        [JsonProperty("feels_like")]
        public FeelsLike FeelsLike { get; set; }

        [JsonProperty("dewpoint")]
        public Dewpoint Dewpoint { get; set; }

        [JsonProperty("wind_speed")]
        public WindSpeed WindSpeed { get; set; }

        [JsonProperty("wind_gust")]
        public WindGust WindGust{ get; set; }

        [JsonProperty("baro_pressure")]
        public BaroPressure BaroPressure { get; set; }

        [JsonProperty("visibility")]
        public Visibility Visibility { get; set; }

        [JsonProperty("humidity")]
        public Humidity Humidity { get; set; }

        [JsonProperty("wind_directions")]
        public WindDirection WindDirection { get; set; }

        [JsonProperty("precipitation")]
        public Precipitation Precipitation { get; set; }

        [JsonProperty("precipitation_type")]
        public PrecipitationType PrecipitationType { get; set; }

        [JsonProperty("cloud_cover")]
        public CloudCover CloudCover { get; set; }

        [JsonProperty("sunrise")]
        public Sunrise Sunrise { get; set; }

        [JsonProperty("sunset")]
        public Sunset Sunset { get; set; }

        [JsonProperty("moon_phase")]
        public MoonPhase MoonPhase { get; set; }

        [JsonProperty("weather_code")]
        public WeatherCode WeatherCode { get; set; }

        [JsonProperty("observation_time")]
        public ObservationTime ObservationTime { get; set; }
    }
}
