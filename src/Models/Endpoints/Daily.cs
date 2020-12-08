namespace ClimaCellCore.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Daily
    {
        public class Model
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
        }

        public class ObservedValueUnit
        {
            public DateTime ObservationTime { get; set; }
            public double Value { get; set; }
            public string Units { get; set; }
        }

        public class ObservedValueUnitMax
        {
            public ObservedValueUnit Max { get; set; }
        }

        public class ObservedValueUnitMinMax
        {
            public ObservedValueUnit Min { get; set; }
            public ObservedValueUnit Max { get; set; }
        }

        public ObservedValueUnitMinMax Temp { get; set; }
        public ObservedValueUnitMinMax FeelsLike { get; set; }
        public ObservedValueUnitMinMax Dewpoint { get; set; }
        public ObservedValueUnitMinMax Humidity { get; set; }
        public ObservedValueUnitMinMax WindSpeed { get; set; }
        public ObservedValueUnitMinMax WindDirection { get; set; }
        public ObservedValueUnitMinMax BaroPressure { get; set; }
        public ObservedValueUnitMax Precipitation { get; set; }
        public PrecipitationProbability PrecipitationProbability { get; set; }
        public PrecipitationAccumulation PrecipitationAccumulation { get; set; }
        public Sunrise Sunrise { get; set; }
        public Sunset Sunset { get; set; }
        public ObservedValueUnitMinMax Visibility { get; set; }
        public WeatherCode WeatherCode { get; set; }
        public ObservationTime ObservationTime { get; set; }

        public static implicit operator Daily(Model m)
        {
            Daily d = new Daily();

            if (m.Temp?.Count == 2)
            {
                d.Temp = new ObservedValueUnitMinMax()
                {
                    Min = new ObservedValueUnit()
                    {
                        ObservationTime = m.Temp[0].ObservationTime,
                        Value = m.Temp[0].Min.Value,
                        Units = m.Temp[0].Min.Units,
                    },
                    Max = new ObservedValueUnit()
                    {
                        ObservationTime = m.Temp[1].ObservationTime,
                        Value = m.Temp[1].Max.Value,
                        Units = m.Temp[1].Max.Units,
                    }
                };
            }

            if (m.FeelsLike?.Count == 2)
            {
                d.FeelsLike = new ObservedValueUnitMinMax()
                {
                    Min = new ObservedValueUnit()
                    {
                        ObservationTime = m.FeelsLike[0].ObservationTime,
                        Value = m.FeelsLike[0].Min.Value,
                        Units = m.FeelsLike[0].Min.Units,
                    },
                    Max = new ObservedValueUnit()
                    {
                        ObservationTime = m.FeelsLike[1].ObservationTime,
                        Value = m.FeelsLike[1].Max.Value,
                        Units = m.FeelsLike[1].Max.Units,
                    }
                };
            }

            if (m.Dewpoint?.Count == 2)
            {
                d.Dewpoint = new ObservedValueUnitMinMax()
                {
                    Min = new ObservedValueUnit()
                    {
                        ObservationTime = m.Dewpoint[0].ObservationTime,
                        Value = m.Dewpoint[0].Min.Value,
                        Units = m.Dewpoint[0].Min.Units,
                    },
                    Max = new ObservedValueUnit()
                    {
                        ObservationTime = m.Dewpoint[1].ObservationTime,
                        Value = m.Dewpoint[1].Max.Value,
                        Units = m.Dewpoint[1].Max.Units,
                    }
                };
            }

            if (m.Humidity?.Count == 2)
            {
                d.Humidity = new ObservedValueUnitMinMax()
                {
                    Min = new ObservedValueUnit()
                    {
                        ObservationTime = m.Humidity[0].ObservationTime,
                        Value = m.Humidity[0].Min.Value,
                        Units = m.Humidity[0].Min.Units,
                    },
                    Max = new ObservedValueUnit()
                    {
                        ObservationTime = m.Humidity[1].ObservationTime,
                        Value = m.Humidity[1].Max.Value,
                        Units = m.Humidity[1].Max.Units,
                    }
                };
            }

            if (m.WindSpeed?.Count == 2)
            {
                d.WindSpeed = new ObservedValueUnitMinMax()
                {
                    Min = new ObservedValueUnit()
                    {
                        ObservationTime = m.WindSpeed[0].ObservationTime,
                        Value = m.WindSpeed[0].Min.Value,
                        Units = m.WindSpeed[0].Min.Units,
                    },
                    Max = new ObservedValueUnit()
                    {
                        ObservationTime = m.WindSpeed[1].ObservationTime,
                        Value = m.WindSpeed[1].Max.Value,
                        Units = m.WindSpeed[1].Max.Units,
                    }
                };
            }

            if (m.WindDirection?.Count == 2)
            {
                d.WindDirection = new ObservedValueUnitMinMax()
                {
                    Min = new ObservedValueUnit()
                    {
                        ObservationTime = m.WindDirection[0].ObservationTime,
                        Value = m.WindDirection[0].Min.Value,
                        Units = m.WindDirection[0].Min.Units,
                    },
                    Max = new ObservedValueUnit()
                    {
                        ObservationTime = m.WindDirection[1].ObservationTime,
                        Value = m.WindDirection[1].Max.Value,
                        Units = m.WindDirection[1].Max.Units,
                    }
                };
            }

            if (m.BaroPressure?.Count == 2)
            {
                d.BaroPressure = new ObservedValueUnitMinMax()
                {
                    Min = new ObservedValueUnit()
                    {
                        ObservationTime = m.BaroPressure[0].ObservationTime,
                        Value = m.BaroPressure[0].Min.Value,
                        Units = m.BaroPressure[0].Min.Units,
                    },
                    Max = new ObservedValueUnit()
                    {
                        ObservationTime = m.BaroPressure[1].ObservationTime,
                        Value = m.BaroPressure[1].Max.Value,
                        Units = m.BaroPressure[1].Max.Units,
                    }
                };
            }

            if (m.Precipitation?.Count == 2)
            {
                d.Precipitation = new ObservedValueUnitMax()
                {
                    Max = new ObservedValueUnit()
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
                d.Visibility = new ObservedValueUnitMinMax()
                {
                    Min = new ObservedValueUnit()
                    {
                        ObservationTime = m.Visibility[0].ObservationTime,
                        Value = m.Visibility[0].Min.Value,
                        Units = m.Visibility[0].Min.Units,
                    },
                    Max = new ObservedValueUnit()
                    {
                        ObservationTime = m.Visibility[1].ObservationTime,
                        Value = m.Visibility[1].Max.Value,
                        Units = m.Visibility[1].Max.Units,
                    }
                };
            }

            d.WeatherCode = m.WeatherCode ?? null;
            d.ObservationTime = m.ObservationTime ?? null;

            return d;
        }
    }
}
