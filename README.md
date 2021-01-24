# Archived
With the release of [climacell v4](https://www.climacell.co/blog/introducing-v4-of-the-climacell-api-with-new-developer-centric-features/) there is no reason to keep working towards a 1.0 release, i'll be taking what I've learned and working towards publishing clima-cell-core for v4 which you can find [here](https://github.com/Twinki14/clima-cell-core) 

# clima-cell-core

An unoffical C# standard library [climacell v3 api](https://developer.climacell.co/) wrapper focused on the free tier data layer and endpoints, heavily inspired and based on [dark-sky-core](https://github.com/amweiss/dark-sky-core) and [ClimaCellCore](https://github.com/algedabra/ClimaCellCore)

### Supported endpoints

Present/Forecast

- :white_check_mark: Realtime
- :white_check_mark: Nowcast
- :white_check_mark: Hourly
- :white_check_mark: Daily

Historical

- :x: ClimaCell
- :x: Station
- :x: Fire Index

Map

- :x: Tiles

### Supported data layers

| Core                                           | Air Quality                                | Pollen                          |
| ---------------------------------------------- | ------------------------------------------ | ------------------------------- |
| :white_check_mark: temp                        | :white_check_mark: pm25                    | :white_check_mark: pollen_tree  |
| :white_check_mark: feels_like                  | :white_check_mark: pm10                    | :white_check_mark: pollen_weed  |
| :white_check_mark: dewpoint                    | :white_check_mark: o3                      | :white_check_mark: pollen_grass |
| :white_check_mark: humidity                    | :white_check_mark: no2                     | :x: Pollen species              |
| :white_check_mark: wind_speed                  | :white_check_mark: co                      |
| :white_check_mark: wind_direction              | :white_check_mark: so2                     |
| :white_check_mark: wind_gust                   | :white_check_mark: epa_aqi                 |
| :white_check_mark: baro_pressure               | :white_check_mark: epa_primary_pollutant   |
| :white_check_mark: precipitation               | :white_check_mark: epa_health_concern      |
| :white_check_mark: precipitation_type          | :white_check_mark: china_aqi               |
| :white_check_mark: precipitation_probability   | :white_check_mark: china_primary_pollutant |
| :white_check_mark: precipitation_accumulation  | :white_check_mark: china_health_concern    |
| :white_check_mark: sunrise                     |                                            |
| :white_check_mark: sunset                      |                                            |
| :white_check_mark: visibility                  |                                            |
| :white_check_mark: cloud_cover                 |                                            |
| :white_check_mark: cloud_base                  |                                            |
| :white_check_mark: cloud_ceiling               |                                            |
| :white_check_mark: surface_shortwave_radiation |                                            |
| :white_check_mark: moon_phase                  |                                            |
| :white_check_mark: weather_code                |                                            |
| :x: cloud_satellite                            |                                            |
| :x: weather_groups                             |                                            |

### Remarks/Known issues

<!-- 
:exclamation:
:pushpin:
 -->
