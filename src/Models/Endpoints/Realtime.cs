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

        [JsonProperty("pm25")]
        public Pm25 pm25 { get; set; }

        [JsonProperty("pm10")]
        public Pm10 pm10 { get; set; }

        [JsonProperty("o3")]
        public O3 o3 { get; set; }        
        
        [JsonProperty("no2")]
        public NO2 no2 { get; set; }        
        
        [JsonProperty("co")]
        public CO co { get; set; }

        [JsonProperty("so2")]
        public SO2 so2 { get; set; }

        [JsonProperty("epa_primary_pollutant")]
        public EpaAQI epaAqi { get; set; }

        [JsonProperty("epa_aqi")]
        public EpaPrimaryPollutant epaPrimaryPollutant { get; set; }

        [JsonProperty("epa_health_concern")]
        public EpaHealthConcern epaHealthConcern { get; set; }

        [JsonProperty("china_aqi")]
        public ChinaAQI chinaAqi { get; set; }

        [JsonProperty("china_primary_pollutant")]
        public ChinaPrimaryPollutant chinaPrimaryPollutant { get; set; }

        [JsonProperty("china_health_concern")]
        public ChinaHealthConcern chinaHealthConcern { get; set; }

        [JsonProperty("pollen_tree")]
        public PollenTree pollenTree { get; set; }

        [JsonProperty("pollen_weed")]
        public PollenWeed pollenWeed { get; set; }

        [JsonProperty("pollen_grass")]
        public PollenGrass pollenGrass { get; set; }

        [JsonProperty("observation_time")]
        public ObservationTime observationTime { get; set; }
    }
}
