using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using ClimaCellCore.Models;
using ClimaCellCore.Services;

namespace ClimaCellCore
{
    public class Nowcast : ForecastResponse<Nowcast.Model>
    {
        public class Model
        {
            [JsonProperty("temp")]
            public Temp Temp { get; set; }

            [JsonProperty("feels_like")]
            public FeelsLike FeelsLike { get; set; }

            [JsonProperty("dewpoint")]
            public Dewpoint Dewpoint { get; set; }

            [JsonProperty("humidity")]
            public Humidity Humidity { get; set; }

            [JsonProperty("wind_speed")]
            public WindSpeed WindSpeed { get; set; }

            [JsonProperty("wind_direction")]
            public WindSpeed WindDirection { get; set; }

            [JsonProperty("wind_gust")]
            public WindGust WindGust { get; set; }

            [JsonProperty("baro_pressure")]
            public BaroPressure BaroPressure { get; set; }

            [JsonProperty("precipitation")]
            public Precipitation Precipitation { get; set; }

            [JsonProperty("precipitation_type")]
            public PrecipitationType PrecipitationType { get; set; }

            [JsonProperty("sunrise")]
            public Sunrise Sunrise { get; set; }

            [JsonProperty("sunset")]
            public Sunset Sunset { get; set; }

            [JsonProperty("visibility")]
            public Visibility Visibility { get; set; }

            [JsonProperty("cloud_cover")]
            public CloudCover CloudCover { get; set; }

            [JsonProperty("cloud_base")]
            public CloudBase CloudBase { get; set; }

            [JsonProperty("cloud_ceiling")]
            public CloudCeiling CloudCeiling { get; set; }

            [JsonProperty("surface_shortwave_radiation")]
            public SurfaceShortwaveRadiation SurfaceShortwaveRadiation { get; set; }

            [JsonProperty("weather_code")]
            public WeatherCode WeatherCode { get; set; }

            [JsonProperty("pm25")]
            public Pm25 Pm25 { get; set; }

            [JsonProperty("pm10")]
            public Pm10 Pm10 { get; set; }

            [JsonProperty("o3")]
            public O3 O3 { get; set; }

            [JsonProperty("no2")]
            public NO2 NO2 { get; set; }

            [JsonProperty("co")]
            public CO CO { get; set; }

            [JsonProperty("so2")]
            public SO2 SO2 { get; set; }

            [JsonProperty("epa_primary_pollutant")]
            public EpaAQI EpaAQI { get; set; }

            [JsonProperty("epa_aqi")]
            public EpaPrimaryPollutant EpaPrimaryPollutant { get; set; }

            [JsonProperty("epa_health_concern")]
            public EpaHealthConcern EpaHealthConcern { get; set; }

            [JsonProperty("china_aqi")]
            public ChinaAQI ChinaAQI { get; set; }

            [JsonProperty("china_primary_pollutant")]
            public ChinaPrimaryPollutant ChinaPrimaryPollutant { get; set; }

            [JsonProperty("china_health_concern")]
            public ChinaHealthConcern ChinaHealthConcern { get; set; }

            [JsonProperty("pollen_tree")]
            public PollenTree PollenTree { get; set; }

            [JsonProperty("pollen_weed")]
            public PollenWeed PollenWeed { get; set; }

            [JsonProperty("pollen_grass")]
            public PollenGrass PollenGrass { get; set; }

            [JsonProperty("observation_time")]
            public ObservationTime ObservationTime { get; set; }
        }

        /// <summary>
        ///     Attempts to deserialize and initialize the parent class with the response content using the 
        ///         <see cref="IJsonSerializerService"/> defined in the calling <see cref="ClimaCellService"/> instance.
        /// </summary>
        public static new async Task<Nowcast> Deserialize(HttpResponseMessage responseMessage, IJsonSerializerService jsonSerializerService)
        {
            Nowcast h = new Nowcast() { Response = new ClimaCellResponse(responseMessage) };
            try
            {
                h._dataPoints = await jsonSerializerService.DeserializeJsonAsync<List<Nowcast.Model>>(responseMessage.Content?.ReadAsStringAsync()).ConfigureAwait(false);
            }
            catch (FormatException e)
            {
                h.Response.IsSuccessStatus = false;
                h.Response.ReasonPhrase = $"Error parsing results: {e?.InnerException?.Message ?? e.Message}";
            }
            return h;
        }
    }
}