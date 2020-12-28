using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using ClimaCellCore.Models;
using ClimaCellCore.Services;

namespace ClimaCellCore
{
    /// <summary>
    ///     'Daily' climacell forecast response model.
    /// </summary>
    public class Daily : ForecastResponse<Daily.Model>
    {
        public class ForecastValueUnit
        {
            public DateTime ObservationTime { get; set; }
            public double? Value { get; set; }
            public string Units { get; set; }
        }

        public class ForecastMax
        {
            public ForecastValueUnit Max { get; set; }
        }

        public class ForecastMinMax
        {
            public ForecastValueUnit Min { get; set; }
            public ForecastValueUnit Max { get; set; }
        }

        /// <summary>
        ///     Observed weather data model for a Daily climacell response.
        /// </summary>
        public class Model
        {
            /// <summary>Latitude.</summary>
            public double Latitude { get; set; }

            /// <summary>Longitude.</summary>
            public double Longitude { get; set; }

            /// <summary>Temperature.</summary>
            public ForecastMinMax Temp { get; set; }

            /// <summary>Wind chill and heat window based on season.</summary>
            public ForecastMinMax FeelsLike { get; set; }

            /// <summary>Temperature of the dew point.</summary>
            public ForecastMinMax Dewpoint { get; set; }

            /// <summary>Percent relative humidity from 0 - 100%.</summary>
            public ForecastMinMax Humidity { get; set; }

            /// <summary>Wind speed.</summary>
            public ForecastMinMax WindSpeed { get; set; }

            /// <summary>Wind direction in polar degrees 0-360 where 0 is North.</summary>
            public ForecastMinMax WindDirection { get; set; }

            /// <summary>Barometric pressure (MSL mean sea level).</summary>
            public ForecastMinMax BaroPressure { get; set; }

            /// <summary>Precipitation intensity.</summary>
            public ForecastMax Precipitation { get; set; }

            /// <summary>The chance that precipitation will occur at the forecast time within the hour or day.</summary>
            public PrecipitationProbability PrecipitationProbability { get; set; }

            /// <summary>The accumulated amount of precipitation in the selected timestep.</summary>
            public PrecipitationAccumulation PrecipitationAccumulation { get; set; }

            /// <summary>The times sunrise based on location.</summary>
            public Sunrise Sunrise { get; set; }

            /// <summary>The times sunset based on location.</summary>
            public Sunset Sunset { get; set; }

            /// <summary>Visibility distance.</summary>
            public ForecastMinMax Visibility { get; set; }

            /// <summary>A textual field that conveys the weather conditions.</summary>
            public WeatherCode WeatherCode { get; set; }

            /// <summary>The times the data was observed.</summary>
            public ObservationTime ObservationTime { get; set; }
        }

        /// <summary>
        ///     Attempts to initalize a new <see cref="Daily"/> from the responding <see cref="HttpResponseMessage"/>, and deserializes the response content
        ///     using the <see cref="IJsonSerializerService"/> defined in the calling <see cref="ClimaCellService"/> instance and appends all model objects into the <see cref="ForecastResponse{T}.DataPoints"/> collection.
        ///
        ///     After deserializing, it'll then convert the private <see cref="_model"/> objects into the more readable public Daily <see cref="Model"/> using <see cref="_model.ToDaily(_model)"/>.
        /// </summary>
        public static new async Task<Daily> Deserialize(HttpResponseMessage responseMessage, IJsonSerializerService jsonSerializerService)
        {
            Daily d = new Daily() { Response = new ClimaCellResponse(responseMessage) };
            try
            {
                var models = await jsonSerializerService.DeserializeJsonAsync<List<_model>>(responseMessage.Content?.ReadAsStringAsync()).ConfigureAwait(false);
                d._dataPoints = models.ConvertAll(d => _model.ToDaily(d));
            }
            catch (FormatException e)
            {
                d.Response.IsSuccessStatus = false;
                d.Response.ReasonPhrase = $"Error parsing results: {e?.InnerException?.Message ?? e.Message}";
            }
            return d;
        }

        /// <summary>
        ///     Internal json model for a climacell daily response, due to the way climacell structures their daily forecast response objects
        ///         the model is split up between the client-facing model, and this internal model.
        ///     When deserializing the internal model objects are converted to the client-facing model. <see cref="ToDaily(_model)"/>.
        /// </summary>
        private protected class _model
        {
            public class DailyTemp
            {
                [JsonProperty("observation_time")]
                public DateTime ObservationTime { get; set; }

                [JsonProperty("min")]
                public Temp Min { get; set; }

                [JsonProperty("max")]
                public Temp Max { get; set; }
            }

            public class DailyFeelsLike
            {
                [JsonProperty("observation_time")]
                public DateTime ObservationTime { get; set; }

                [JsonProperty("min")]
                public FeelsLike Min { get; set; }

                [JsonProperty("max")]
                public FeelsLike Max { get; set; }
            }

            public class DailyDewpoint
            {
                [JsonProperty("observation_time")]
                public DateTime ObservationTime { get; set; }

                [JsonProperty("min")]
                public Dewpoint Min { get; set; }

                [JsonProperty("max")]
                public Dewpoint Max { get; set; }
            }

            public class DailyHumidity
            {
                [JsonProperty("observation_time")]
                public DateTime ObservationTime { get; set; }

                [JsonProperty("min")]
                public Humidity Min { get; set; }

                [JsonProperty("max")]
                public Humidity Max { get; set; }
            }

            public class DailyWindSpeed
            {
                [JsonProperty("observation_time")]
                public DateTime ObservationTime { get; set; }

                [JsonProperty("min")]
                public WindSpeed Min { get; set; }

                [JsonProperty("max")]
                public WindSpeed Max { get; set; }
            }

            public class DailyWindDirection
            {
                [JsonProperty("observation_time")]
                public DateTime ObservationTime { get; set; }

                [JsonProperty("min")]
                public WindDirection Min { get; set; }

                [JsonProperty("max")]
                public WindDirection Max { get; set; }
            }

            public class DailyBaroPressure
            {
                [JsonProperty("observation_time")]
                public DateTime ObservationTime { get; set; }

                [JsonProperty("min")]
                public BaroPressure Min { get; set; }

                [JsonProperty("max")]
                public BaroPressure Max { get; set; }
            }

            public class DailyPrecipitation
            {
                [JsonProperty("observation_time")]
                public DateTime ObservationTime { get; set; }

                [JsonProperty("max")]
                public Precipitation Max { get; set; }
            }

            public class DailyVisibility
            {
                [JsonProperty("observation_time")]
                public DateTime ObservationTime { get; set; }

                [JsonProperty("min")]
                public Visibility Min { get; set; }

                [JsonProperty("max")]
                public Visibility Max { get; set; }
            }

            [JsonProperty("temp")]
            public List<DailyTemp> Temp { get; set; }

            [JsonProperty("feels_like")]
            public List<DailyFeelsLike> FeelsLike { get; set; }

            [JsonProperty("dewpoint")]
            public List<DailyDewpoint> Dewpoint { get; set; }

            [JsonProperty("humidity")]
            public List<DailyHumidity> Humidity { get; set; }

            [JsonProperty("wind_speed")]
            public List<DailyWindSpeed> WindSpeed { get; set; }

            [JsonProperty("wind_direction")]
            public List<DailyWindSpeed> WindDirection { get; set; }

            [JsonProperty("baro_pressure")]
            public List<DailyBaroPressure> BaroPressure { get; set; }

            [JsonProperty("precipitation")]
            public List<DailyPrecipitation> Precipitation { get; set; }

            [JsonProperty("precipitation_probability")]
            public PrecipitationProbability PrecipitationProbability { get; set; }

            [JsonProperty("precipitation_accumulation")]
            public PrecipitationAccumulation PrecipitationAccumulation { get; set; }

            [JsonProperty("sunrise")]
            public Sunrise Sunrise { get; set; }

            [JsonProperty("sunset")]
            public Sunset Sunset { get; set; }

            [JsonProperty("visibility")]
            public List<DailyVisibility> Visibility { get; set; }

            [JsonProperty("weather_code")]
            public WeatherCode WeatherCode { get; set; }

            [JsonProperty("observation_time")]
            public ObservationTime ObservationTime { get; set; }

            /// <summary>Latitude.</summary>
            [JsonProperty("lat")]
            public double Latitude { get; set; }

            /// <summary>Longitude.</summary>
            [JsonProperty("lon")]
            public double Longitude { get; set; }

            /// <summary>
            ///     Intializes and returns a new <see cref="Model"/> instance and populates it from the passed <see cref="_model"/>.
            /// </summary>
            public static Model ToDaily(_model m)
            {
                Model d = new Model();

                if (m.Temp?.Count == 2)
                {
                    d.Temp = new ForecastMinMax()
                    {
                        Min = new ForecastValueUnit()
                        {
                            ObservationTime = m.Temp[0].ObservationTime,
                            Value = m.Temp[0].Min.Value,
                            Units = m.Temp[0].Min.Units,
                        },
                        Max = new ForecastValueUnit()
                        {
                            ObservationTime = m.Temp[1].ObservationTime,
                            Value = m.Temp[1].Max.Value,
                            Units = m.Temp[1].Max.Units,
                        }
                    };
                }

                if (m.FeelsLike?.Count == 2)
                {
                    d.FeelsLike = new ForecastMinMax()
                    {
                        Min = new ForecastValueUnit()
                        {
                            ObservationTime = m.FeelsLike[0].ObservationTime,
                            Value = m.FeelsLike[0].Min.Value,
                            Units = m.FeelsLike[0].Min.Units,
                        },
                        Max = new ForecastValueUnit()
                        {
                            ObservationTime = m.FeelsLike[1].ObservationTime,
                            Value = m.FeelsLike[1].Max.Value,
                            Units = m.FeelsLike[1].Max.Units,
                        }
                    };
                }

                if (m.Dewpoint?.Count == 2)
                {
                    d.Dewpoint = new ForecastMinMax()
                    {
                        Min = new ForecastValueUnit()
                        {
                            ObservationTime = m.Dewpoint[0].ObservationTime,
                            Value = m.Dewpoint[0].Min.Value,
                            Units = m.Dewpoint[0].Min.Units,
                        },
                        Max = new ForecastValueUnit()
                        {
                            ObservationTime = m.Dewpoint[1].ObservationTime,
                            Value = m.Dewpoint[1].Max.Value,
                            Units = m.Dewpoint[1].Max.Units,
                        }
                    };
                }

                if (m.Humidity?.Count == 2)
                {
                    d.Humidity = new ForecastMinMax()
                    {
                        Min = new ForecastValueUnit()
                        {
                            ObservationTime = m.Humidity[0].ObservationTime,
                            Value = m.Humidity[0].Min.Value,
                            Units = m.Humidity[0].Min.Units,
                        },
                        Max = new ForecastValueUnit()
                        {
                            ObservationTime = m.Humidity[1].ObservationTime,
                            Value = m.Humidity[1].Max.Value,
                            Units = m.Humidity[1].Max.Units,
                        }
                    };
                }

                if (m.WindSpeed?.Count == 2)
                {
                    d.WindSpeed = new ForecastMinMax()
                    {
                        Min = new ForecastValueUnit()
                        {
                            ObservationTime = m.WindSpeed[0].ObservationTime,
                            Value = m.WindSpeed[0].Min.Value,
                            Units = m.WindSpeed[0].Min.Units,
                        },
                        Max = new ForecastValueUnit()
                        {
                            ObservationTime = m.WindSpeed[1].ObservationTime,
                            Value = m.WindSpeed[1].Max.Value,
                            Units = m.WindSpeed[1].Max.Units,
                        }
                    };
                }

                if (m.WindDirection?.Count == 2)
                {
                    d.WindDirection = new ForecastMinMax()
                    {
                        Min = new ForecastValueUnit()
                        {
                            ObservationTime = m.WindDirection[0].ObservationTime,
                            Value = m.WindDirection[0].Min.Value,
                            Units = m.WindDirection[0].Min.Units,
                        },
                        Max = new ForecastValueUnit()
                        {
                            ObservationTime = m.WindDirection[1].ObservationTime,
                            Value = m.WindDirection[1].Max.Value,
                            Units = m.WindDirection[1].Max.Units,
                        }
                    };
                }

                if (m.BaroPressure?.Count == 2)
                {
                    d.BaroPressure = new ForecastMinMax()
                    {
                        Min = new ForecastValueUnit()
                        {
                            ObservationTime = m.BaroPressure[0].ObservationTime,
                            Value = m.BaroPressure[0].Min.Value,
                            Units = m.BaroPressure[0].Min.Units,
                        },
                        Max = new ForecastValueUnit()
                        {
                            ObservationTime = m.BaroPressure[1].ObservationTime,
                            Value = m.BaroPressure[1].Max.Value,
                            Units = m.BaroPressure[1].Max.Units,
                        }
                    };
                }

                if (m.Precipitation?.Count == 2)
                {
                    d.Precipitation = new ForecastMax()
                    {
                        Max = new ForecastValueUnit()
                        {
                            ObservationTime = m.Precipitation[0].ObservationTime,
                            Value = m.Precipitation[0].Max.Value,
                            Units = m.Precipitation[0].Max.Units,
                        }
                    };
                }

                d.PrecipitationProbability = m.PrecipitationProbability ?? null;
                d.PrecipitationAccumulation = m.PrecipitationAccumulation ?? null;
                d.Sunrise = m.Sunrise ?? null;
                d.Sunset = m.Sunset ?? null;

                if (m.Visibility?.Count == 2)
                {
                    d.Visibility = new ForecastMinMax()
                    {
                        Min = new ForecastValueUnit()
                        {
                            ObservationTime = m.Visibility[0].ObservationTime,
                            Value = m.Visibility[0].Min.Value,
                            Units = m.Visibility[0].Min.Units,
                        },
                        Max = new ForecastValueUnit()
                        {
                            ObservationTime = m.Visibility[1].ObservationTime,
                            Value = m.Visibility[1].Max.Value,
                            Units = m.Visibility[1].Max.Units,
                        }
                    };
                }

                d.WeatherCode = m.WeatherCode ?? null;
                d.ObservationTime = m.ObservationTime ?? null;
                d.Latitude = m.Latitude;
                d.Longitude = m.Longitude;

                return d;
            }
        }
    }
}