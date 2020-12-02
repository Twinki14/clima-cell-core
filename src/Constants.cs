namespace ClimaCellCore.Constants
{
    /// <summary>
    ///     climacell 'Core' data layer
    /// </summary>
    public static class Core
    {
        public static readonly string Temp                      = "temp";                         /// <summary>Temperature.</summary>
        public static readonly string FeelsLike                 = "feels_like";                   /// <summary>Wind chill and heat window based on season.</summary>
        public static readonly string Dewpoint                  = "dewpoint";                     /// <summary>Temperature of the dew point.</summary>
        public static readonly string Humidity                  = "humidity";                     /// <summary>Percent relative humidity from 0 - 100%.</summary>
        public static readonly string WindSpeed                 = "wind_speed";                   /// <summary>Wind speed.</summary>
        public static readonly string WindDirection             = "wind_direction";               /// <summary>Wind direction in polar degrees 0-360 where 0 is North.</summary>
        public static readonly string WindGust                  = "wind_gust";                    /// <summary>Wind gust speed.</summary>
        public static readonly string BaroPressure              = "baro_pressure";                /// <summary>Barometric pressure (MSL mean sea level).</summary>
        public static readonly string Precipitation             = "precipitation";                /// <summary>Precipitation intensity.</summary>
        public static readonly string PrecipitationType         = "precipitation_type";           /// <summary>The type of precipitation.</summary>
        public static readonly string PrecipitationProbability  = "precipitation_probability";    /// <summary>The chance that precipitation will occur at the forecast time within the hour or day.</summary>
        public static readonly string PrecipitationAccumulation = "precipitation_accumulation";   /// <summary>The accumulated amount of precipitation in the selected timestep.</summary>
        public static readonly string Sunrise                   = "sunrise";                      /// <summary>The times sunrise based on location.</summary>
        public static readonly string Sunset                    = "sunset";                       /// <summary>The times sunset based on location.</summary>
        public static readonly string Visibility                = "visibility";                   /// <summary>Visibility distance.</summary>
        public static readonly string CloudCover                = "cloud_cover";                  /// <summary>Fraction of the sky obscured by clouds.</summary>
        public static readonly string CloudBase                 = "cloud_base";                   /// <summary>The lowest level at which the air contains a perceptible quantity of cloud particles.</summary>
        public static readonly string CloudCeiling              = "cloud_ceiling";                /// <summary>The height of the lowest layer of clouds which covers more than half of the sky.</summary>
        public static readonly string SurfaceShortwaveRadiation = "surface_shortwave_radiation";  /// <summary>Solar radiation reaching the surface.</summary>
        public static readonly string MoonPhase                 = "moon_phase";                   /// <summary>The shape of the directly sunlit portion of the Moon.</summary>
        public static readonly string WeatherCode               = "weather_code";                 /// <summary>A textual field that conveys the weather conditions.</summary>

        // Unsupported - These are only used unsupported climacell endpoints
        // public static readonly string CloudSatellite   = "cloud_satellite"; /// <summary>Fraction of the sky obscured by clouds, as observed by satellites.</summary>
        // public static readonly string WeatherGroups    = "weather_groups";  /// <summary>All weather elements that convey the weather conditions.</summary>
    }

    /// <summary>
    ///     climacell 'AirQuality' data layer
    /// </summary>
    public static class AirQuality
    {
        public static readonly string Pm25                  = "pm25";                     /// <summary>Particulate Matter < 2.5 μm.</summary>
        public static readonly string Pm10                  = "pm10";                     /// <summary>Particulate Matter < 10 μm.</summary>
        public static readonly string O3                    = "o3";                       /// <summary>Ozone.</summary>
        public static readonly string NO2                   = "no2";                      /// <summary>Nitrogen Dioxide.</summary>
        public static readonly string CO                    = "co";                       /// <summary>Carbon Monoxide.</summary>
        public static readonly string SO2                   = "so2";                      /// <summary>Sulfur Dioxide.</summary>
        public static readonly string EpaAQI                = "epa_aqi";                  /// <summary>Air quality index per US EPA standard.</summary>
        public static readonly string EpaPrimaryPollutant   = "epa_primary_pollutant";    /// <summary>Primary pollutant per US EPA standard.</summary>
        public static readonly string EpaHealthConcern      = "epa_health_concern";       /// <summary>Health concern level based on EPA standard.</summary>
        public static readonly string ChinaAQI              = "china_aqi";                /// <summary>Air quality index per China MEP standard.</summary>
        public static readonly string ChinaPrimaryPollutant = "china_primary_pollutant";  /// <summary>Primary pollutant per China MEP standard.</summary>
        public static readonly string ChinaHealthConcern    = "china_health_concern";     /// <summary>Health concern level based on China MEP standard.</summary>
    }

    /// <summary>
    ///     climacell 'Pollen' data layer
    /// </summary>
    public static class Pollen
    {
        public static readonly string Tree  = "pollen_tree";   /// <summary>ClimaCell pollen index for trees.</summary>
        public static readonly string Weed  = "pollen_weed";   /// <summary>ClimaCell pollen index for weeds</summary>
        public static readonly string Grass = "pollen_grass";  /// <summary>ClimaCell pollen index for grass</summary>
    }

    /// <summary>
    ///     climacell data endpoints
    /// </summary>
    public static class Endpoint
    {
        public static readonly string Realtime = "realtime";
        public static readonly string NowCast  = "nowcast";
        public static readonly string Hourly   = "forecast/hourly";
        public static readonly string Daily    = "forecast/daily";
    }

    /// <summary>
    ///     climacell unit systems
    /// </summary>
    public static class UnitSystem
    {
        public static readonly string Si = "si";  /// <summary> International System of Units </summary>
        public static readonly string Us = "us";  /// <summary> US customary units </summary>
    }
}
