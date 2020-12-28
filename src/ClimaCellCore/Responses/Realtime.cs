using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ClimaCellCore.Models;
using ClimaCellCore.Services;

namespace ClimaCellCore
{
    /// <summary>
    ///     'Realtime' climacell present response model.
    /// </summary>
    public class Realtime : PresentResponse
    {
        /// <summary>Latitude.</summary>
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        /// <summary>Longitude.</summary>
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        /// <summary>Temperature.</summary>
        [JsonProperty("temp")]
        public Temp Temp { get; set; }

        /// <summary>Wind chill and heat window based on season.</summary>
        [JsonProperty("feels_like")]
        public FeelsLike FeelsLike { get; set; }

        /// <summary>Temperature of the dew point.</summary>
        [JsonProperty("dewpoint")]
        public Dewpoint Dewpoint { get; set; }

        /// <summary>Percent relative humidity from 0 - 100%.</summary>
        [JsonProperty("humidity")]
        public Humidity Humidity { get; set; }

        /// <summary>Wind speed.</summary>
        [JsonProperty("wind_speed")]
        public WindSpeed WindSpeed { get; set; }

        /// <summary>Wind direction in polar degrees 0-360 where 0 is North.</summary>
        [JsonProperty("wind_direction")]
        public WindSpeed WindDirection { get; set; }

        /// <summary>Wind gust speed.</summary>
        [JsonProperty("wind_gust")]
        public WindGust WindGust { get; set; }

        /// <summary>Barometric pressure (MSL mean sea level).</summary>
        [JsonProperty("baro_pressure")]
        public BaroPressure BaroPressure { get; set; }

        /// <summary>Precipitation intensity.</summary>
        [JsonProperty("precipitation")]
        public Precipitation Precipitation { get; set; }

        /// <summary>The type of precipitation.</summary>
        [JsonProperty("precipitation_type")]
        public PrecipitationType PrecipitationType { get; set; }

        /// <summary>The times sunrise based on location.</summary>
        [JsonProperty("sunrise")]
        public Sunrise Sunrise { get; set; }

        /// <summary>The times sunset based on location.</summary>
        [JsonProperty("sunset")]
        public Sunset Sunset { get; set; }

        /// <summary>Visibility distance.</summary>
        [JsonProperty("visibility")]
        public Visibility Visibility { get; set; }

        /// <summary>Fraction of the sky obscured by clouds.</summary>
        [JsonProperty("cloud_cover")]
        public CloudCover CloudCover { get; set; }

        /// <summary>The lowest level at which the air contains a perceptible quantity of cloud particles.</summary>
        [JsonProperty("cloud_base")]
        public CloudBase CloudBase { get; set; }

        /// <summary>The height of the lowest layer of clouds which covers more than half of the sky.</summary>
        [JsonProperty("cloud_ceiling")]
        public CloudCeiling CloudCeiling { get; set; }

        /// <summary>Solar radiation reaching the surface.</summary>
        [JsonProperty("surface_shortwave_radiation")]
        public SurfaceShortwaveRadiation SurfaceShortwaveRadiation { get; set; }

        /// <summary>The shape of the directly sunlit portion of the Moon.</summary>
        [JsonProperty("moon_phase")]
        public MoonPhase MoonPhase { get; set; }

        /// <summary>A textual field that conveys the weather conditions.</summary>
        [JsonProperty("weather_code")]
        public WeatherCode WeatherCode { get; set; }

        /// <summary>Particulate Matter < 2.5 μm.</summary>
        [JsonProperty("pm25")]
        public Pm25 Pm25 { get; set; }

        /// <summary>Particulate Matter < 10 μm.</summary>
        [JsonProperty("pm10")]
        public Pm10 Pm10 { get; set; }

        /// <summary>Ozone.</summary>
        [JsonProperty("o3")]
        public O3 O3 { get; set; }

        /// <summary>Nitrogen Dioxide.</summary>
        [JsonProperty("no2")]
        public NO2 NO2 { get; set; }

        /// <summary>Carbon Monoxide.</summary>
        [JsonProperty("co")]
        public CO CO { get; set; }

        /// <summary>Sulfur Dioxide.</summary>
        [JsonProperty("so2")]
        public SO2 SO2 { get; set; }

        /// <summary>Air quality index per US EPA standard.</summary>
        [JsonProperty("epa_aqi")]
        public EpaAQI EpaAQI { get; set; }

        /// <summary>Primary pollutant per US EPA standard.</summary>
        [JsonProperty("epa_primary_pollutant")]
        public EpaPrimaryPollutant EpaPrimaryPollutant { get; set; }

        /// <summary>Health concern level based on EPA standard.</summary>
        [JsonProperty("epa_health_concern")]
        public EpaHealthConcern EpaHealthConcern { get; set; }

        /// <summary>Air quality index per China MEP standard.</summary>
        [JsonProperty("china_aqi")]
        public ChinaAQI ChinaAQI { get; set; }

        /// <summary>Primary pollutant per China MEP standard.</summary>
        [JsonProperty("china_primary_pollutant")]
        public ChinaPrimaryPollutant ChinaPrimaryPollutant { get; set; }

        /// <summary>Health concern level based on China MEP standard.</summary>
        [JsonProperty("china_health_concern")]
        public ChinaHealthConcern ChinaHealthConcern { get; set; }

        /// <summary>ClimaCell pollen index for trees.</summary>
        [JsonProperty("pollen_tree")]
        public PollenTree PollenTree { get; set; }

        /// <summary>ClimaCell pollen index for weeds.</summary>
        [JsonProperty("pollen_weed")]
        public PollenWeed PollenWeed { get; set; }

        /// <summary>ClimaCell pollen index for grass.</summary>
        [JsonProperty("pollen_grass")]
        public PollenGrass PollenGrass { get; set; }

        /// <summary>The times the data was observed.</summary>
        [JsonProperty("observation_time")]
        public ObservationTime ObservationTime { get; set; }

        /// <summary>
        ///     Attempts to deserialize and initialize the parent class with the response content using the 
        ///         <see cref="IJsonSerializerService"/> defined in the calling <see cref="ClimaCellService"/> instance.
        /// </summary>
        public static new async Task<Realtime> Deserialize(HttpResponseMessage responseMessage, IJsonSerializerService jsonSerializerService)
        {
            Realtime r = new Realtime();
            try
            {
                r = await jsonSerializerService.DeserializeJsonAsync<Realtime>(responseMessage.Content?.ReadAsStringAsync()).ConfigureAwait(false);
                r.Response = new ClimaCellResponse(responseMessage);
            }
            catch (FormatException e)
            {
                r.Response.IsSuccessStatus = false;
                r.Response.ReasonPhrase = $"Error parsing results: {e?.InnerException?.Message ?? e.Message}";
            }
            return r;
        }
    }
}
