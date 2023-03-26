using myNOC.WeatherLink.Attributes;
using myNOC.WeatherLink.Models.Sensors.Data;
using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.Sensors.Data
{
	public class VantagePro2PlusArchive : SensorArchiveData
	{
		public VantagePro2PlusArchive() : base(SensorType.VantagePro2Plus, DataStructureType.ISSArchiveRecord, "Vantage Pro2 Plus /w 24-hr-Fan-Aspirated Radiation shield, UV & Solar Radiation") { }

		[HighLow, JsonPropertyName("temp_avg")]
		public float? TemperatureAvg { get; set; }
		[HighLow(relatedProperties: nameof(UnixTemperatureHighAt)), JsonPropertyName("temp_hi")]
		public float? TemperatureHigh { get; set; }
		[JsonPropertyName("temp_hi_at")]
		public int? UnixTemperatureHighAt { get; set; }
		public DateTimeOffset TemperatureHighAt => DateTimeOffset.FromUnixTimeSeconds(UnixTemperatureHighAt ?? 0);
		[HighLow(HighOrLow.Low, nameof(UnixTemperatureLowAt)), JsonPropertyName("temp_lo")]
		public float? TemperatureLow { get; set; }
		[JsonPropertyName("temp_lo_at")]
		public int? UnixTemperatureLowAt { get; set; }
		public DateTimeOffset TemperatureLowAt => DateTimeOffset.FromUnixTimeSeconds(UnixTemperatureLowAt ?? 0);
		[HighLow(relatedProperties: nameof(UnixHumidityHighAt)), JsonPropertyName("hum_hi")]
		public float? HumidityHigh { get; set; }
		[JsonPropertyName("hum_hi_at")]
		public int? UnixHumidityHighAt { get; set; }
		public DateTimeOffset HumidityHighAt => DateTimeOffset.FromUnixTimeSeconds(UnixHumidityHighAt ?? 0);
		[HighLow(HighOrLow.Low, nameof(UnixHumidityLowAt)), JsonPropertyName("hum_lo")]
		public float? HumidityLow { get; set; }
		[JsonPropertyName("hum_lo_at")]
		public int? UnixHumidityLowAt { get; set; }
		public DateTimeOffset HumidityLowAt => DateTimeOffset.FromUnixTimeSeconds(UnixHumidityLowAt ?? 0);
		[HighLow(relatedProperties: nameof(UnixDewPointHighAt)), JsonPropertyName("dew_point_hi")]
		public float? DewPointHigh { get; set; }
		[JsonPropertyName("dew_point_hi_at")]
		public int? UnixDewPointHighAt { get; set; }
		public DateTimeOffset DewPointHighAt => DateTimeOffset.FromUnixTimeSeconds(UnixDewPointHighAt ?? 0);
		[HighLow(HighOrLow.Low, nameof(UnixDewPointLowAt)), JsonPropertyName("dew_point_lo")]
		public float? DewPointLow { get; set; }
		[JsonPropertyName("dew_point_lo_at")]
		public int? UnixDewPointLowAt { get; set; }
		public DateTimeOffset DewPointLowAt => DateTimeOffset.FromUnixTimeSeconds(UnixDewPointLowAt ?? 0);
		[HighLow(relatedProperties: nameof(UnixWetBulbHighAt)), JsonPropertyName("wet_bulb_hi")]
		public float? WetBulbHigh { get; set; }
		[JsonPropertyName("wet_bulb_hi_at")]
		public int? UnixWetBulbHighAt { get; set; }
		public DateTimeOffset WetBulbHighAt => DateTimeOffset.FromUnixTimeSeconds(UnixWetBulbHighAt ?? 0);
		[HighLow(HighOrLow.Low, relatedProperties: nameof(UnixWetBulbLowAt)), JsonPropertyName("wet_bulb_lo")]
		public float? WetBulbLow { get; set; }
		[JsonPropertyName("wet_bulb_lo_at")]
		public int? UnixWetBulbLowAt { get; set; }
		public DateTimeOffset WetBulbLowAt => DateTimeOffset.FromUnixTimeSeconds(UnixWetBulbLowAt ?? 0);
		[HighLow, JsonPropertyName("wind_speed_avg")]
		public float? WindSpeedAvg { get; set; }
		[HighLow(HighOrLow.High, nameof(UnixWindSpeedHighAt), nameof(WindSpeedHighDir)), JsonPropertyName("wind_speed_hi")]
		public float? WindSpeedHigh { get; set; }
		[JsonPropertyName("wind_speed_hi_dir")]
		public int? WindSpeedHighDir { get; set; }
		[JsonPropertyName("wind_speed_hi_at")]
		public int? UnixWindSpeedHighAt { get; set; }
		public DateTimeOffset WindSpeedHighAt => DateTimeOffset.FromUnixTimeSeconds(UnixWindSpeedHighAt ?? 0);
		[HighLow(HighOrLow.Low, nameof(UnixWindChillLowAt)), JsonPropertyName("wind_chill_lo")]
		public float? WindChillLow { get; set; }
		[JsonPropertyName("wind_chill_lo_at")]
		public int? UnixWindChillLowAt { get; set; }
		public DateTimeOffset WindChillLowAt => DateTimeOffset.FromUnixTimeSeconds(UnixWindChillLowAt ?? 0);
		[HighLow(relatedProperties: nameof(UnixHeatIndexHighAt)), JsonPropertyName("heat_index_hi")]
		public float? HeatIndexHigh { get; set; }
		[JsonPropertyName("heat_index_hi_at")]
		public int? UnixHeatIndexHighAt { get; set; }
		public DateTimeOffset HeatIndexHighAt => DateTimeOffset.FromUnixTimeSeconds(UnixHeatIndexHighAt ?? 0);
		[HighLow(relatedProperties: nameof(UnixTHWIndexHighAt)), JsonPropertyName("thw_index_hi")]
		public float? THWIndexHigh { get; set; }
		[JsonPropertyName("thw_index_hi_at")]
		public int? UnixTHWIndexHighAt { get; set; }
		public DateTimeOffset THWIndexHighAt => DateTimeOffset.FromUnixTimeSeconds(UnixTHWIndexHighAt ?? 0);
		[HighLow(HighOrLow.Low, nameof(UnixTHWIndexLowAt)), JsonPropertyName("thw_index_lo")]
		public float? THWIndexLow { get; set; }
		[JsonPropertyName("thw_index_lo_at")]
		public int? UnixTHWIndexLowAt { get; set; }
		public DateTimeOffset THWIndexLowAt => DateTimeOffset.FromUnixTimeSeconds(UnixTHWIndexLowAt ?? 0);
		[HighLow(relatedProperties: nameof(UnixTHSWIndexHighAt)), JsonPropertyName("thsw_index_hi")]
		public float? THSWIndexHigh { get; set; }
		[JsonPropertyName("thsw_index_hi_at")]
		public int? UnixTHSWIndexHighAt { get; set; }
		public DateTimeOffset THSWIndexHighAt => DateTimeOffset.FromUnixTimeSeconds(UnixTHSWIndexHighAt ?? 0);
		[HighLow(HighOrLow.Low, nameof(UnixTHSWIndexLowAt)), JsonPropertyName("thsw_index_lo")]
		public float? THSWIndexLow { get; set; }
		[JsonPropertyName("thsw_index_lo_at")]
		public int? UnixTHSWIndexLowAt { get; set; }
		public DateTimeOffset THSWIndexLowAt => DateTimeOffset.FromUnixTimeSeconds(UnixTHSWIndexLowAt ?? 0);
		[HighLow, JsonPropertyName("rainfall_clicks")]
		public int? RainfallClicks { get; set; }
		[HighLow, JsonPropertyName("rainfall_in")]
		public float? Rainfall_in { get; set; }
		[HighLow, JsonPropertyName("rainfall_mm")]
		public float? Rainfall_mm { get; set; }
		[HighLow(HighOrLow.High, nameof(RainRateHigh_mm), nameof(UnixRainRateHighAt)), JsonPropertyName("rain_rate_hi_in")]
		public float? RainRateHigh_in { get; set; }
		[JsonPropertyName("rain_rate_hi_mm")]
		public float? RainRateHigh_mm { get; set; }
		[JsonPropertyName("rain_rate_hi_at")]
		public int? UnixRainRateHighAt { get; set; }
		public DateTimeOffset RainRateHighAt => DateTimeOffset.FromUnixTimeSeconds(UnixRainRateHighAt ?? 0);
		[HighLow, JsonPropertyName("solar_rad_avg")]
		public int? SolarRadAvg { get; set; }
		[HighLow(relatedProperties: nameof(UnixSolarRadHighAt)), JsonPropertyName("solar_rad_hi")]
		public int? SolarRadHigh { get; set; }
		[JsonPropertyName("solar_rad_hi_at")]
		public int? UnixSolarRadHighAt { get; set; }
		public DateTimeOffset SolarRadHighAt => DateTimeOffset.FromUnixTimeSeconds(UnixSolarRadHighAt ?? 0);
		[HighLow, JsonPropertyName("uv_index_avg")]
		public float? UVIndexAvg { get; set; }
		[HighLow(relatedProperties: nameof(UnixUVIndexHighAt)), JsonPropertyName("uv_index_hi")]
		public float? UvIndexHigh { get; set; }
		[JsonPropertyName("uv_index_hi_at")]
		public int? UnixUVIndexHighAt { get; set; }
		public DateTimeOffset UVIndexHighAt => DateTimeOffset.FromUnixTimeSeconds(UnixUVIndexHighAt ?? 0);
		[HighLow, JsonPropertyName("wind_run")]
		public float? WindRun { get; set; }
		[HighLow, JsonPropertyName("solar_energy")]
		public float? SolarEnergy { get; set; }
		[HighLow, JsonPropertyName("uv_dose")]
		public float? UVDose { get; set; }
		[HighLow, JsonPropertyName("cooling_degree_days")]
		public float? CoolingDegreeDays { get; set; }
		[HighLow, JsonPropertyName("heating_degree_days")]
		public float? HeatingDegreeDays { get; set; }
	}
}
