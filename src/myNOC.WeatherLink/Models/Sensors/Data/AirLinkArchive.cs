using myNOC.WeatherLink.Attributes;
using myNOC.WeatherLink.Models.Sensors.Data;
using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.Sensors.Data
{
	public class AirLinkArchive : SensorArchiveData
	{
		public AirLinkArchive() : base(SensorType.AirLink, DataStructureType.AirLinkArchiveRecord, "AirLink") { }

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
		[HighLow(HighOrLow.Low, nameof(UnixWetBulbLowAt)), JsonPropertyName("wet_bulb_lo")]
		public float? WetBulbLow { get; set; }
		[JsonPropertyName("wet_bulb_lo_at")]
		public int? UnixWetBulbLowAt { get; set; }
		public DateTimeOffset WetBulbLowAt => DateTimeOffset.FromUnixTimeSeconds(UnixWetBulbLowAt ?? 0);
		[HighLow(relatedProperties: nameof(UnixHeatIndexHighAt)), JsonPropertyName("heat_index_hi")]
		public float? HeatIndexHigh { get; set; }
		[JsonPropertyName("heat_index_hi_at")]
		public int? UnixHeatIndexHighAt { get; set; }
		public DateTimeOffset HeatIndexHighAt => DateTimeOffset.FromUnixTimeSeconds(UnixHeatIndexHighAt ?? 0);
		[HighLow, JsonPropertyName("pm_1_avg")]
		public float? PM1Avg { get; set; }
		[HighLow(relatedProperties: nameof(PM1HighAt)), JsonPropertyName("pm_1_hi")]
		public float? PM1High { get; set; }
		[JsonPropertyName("pm_1_hi_at")]
		public int? PM1HighAt { get; set; }
		[HighLow, JsonPropertyName("pm_2p5_avg")]
		public float? PM2p5Avg { get; set; }
		[HighLow(relatedProperties: nameof(UnixPM2p5HighAt)), JsonPropertyName("pm_2p5_hi")]
		public float? PM2p5High { get; set; }
		[JsonPropertyName("pm_2p5_hi_at")]
		public int? UnixPM2p5HighAt { get; set; }
		public DateTimeOffset PM2p5HighAt => DateTimeOffset.FromUnixTimeSeconds(UnixPM2p5HighAt ?? 0);
		[HighLow, JsonPropertyName("pm_10_avg")]
		public float? PM10Avg { get; set; }
		[HighLow(relatedProperties:	nameof(UnixPM10HighAt)), JsonPropertyName("pm_10_hi")]
		public float? PM10High { get; set; }
		[JsonPropertyName("pm_10_hi_at")]
		public int? UnixPM10HighAt { get; set; }
		public DateTimeOffset PM10HighAt => DateTimeOffset.FromUnixTimeSeconds(UnixPM10HighAt ?? 0);
		[HighLow, JsonPropertyName("pm_0p3_avg_num_part")]
		public float? PM0p3AvgNumPart { get; set; }
		[HighLow, JsonPropertyName("pm_0p3_hi_num_part")]
		public float? PM0p3HighNumPart { get; set; }
		[HighLow, JsonPropertyName("pm_0p5_avg_num_part")]
		public float? PM0p5AvgNumPart { get; set; }
		[HighLow, JsonPropertyName("pm_0p5_hi_num_part")]
		public float? PM0p5HighNumPart { get; set; }
		[HighLow, JsonPropertyName("pm_1_avg_num_part")]
		public float? PM1AvgNumPart { get; set; }
		[HighLow, JsonPropertyName("pm_1_hi_num_part")]
		public float? PM1HighNumPart { get; set; }
		[HighLow, JsonPropertyName("pm_2p5_avg_num_part")]
		public float? PM2p5AvgNumPart { get; set; }
		[HighLow, JsonPropertyName("pm_2p5_hi_num_part")]
		public float? PM2p5HighNumPart { get; set; }
		[HighLow, JsonPropertyName("pm_5_avg_num_part")]
		public float? PM5AvgNumPart { get; set; }
		[HighLow, JsonPropertyName("pm_5_hi_num_part")]
		public float? PM5HighNumPart { get; set; }
		[HighLow, JsonPropertyName("pm_10_avg_num_part")]
		public float? PM10AvgNumPart { get; set; }
		[HighLow, JsonPropertyName("pm_10_hi_num_part")]
		public float? PM10HighNumPart { get; set; }
		[JsonPropertyName("aqi_type")]
		public string? AQIType { get; set; }
		[HighLow(relatedProperties: nameof(AQIAvgDesc)), JsonPropertyName("aqi_avg_val")]
		public float? AQIAvgVal { get; set; }
		[JsonPropertyName("aqi_avg_desc")]
		public string? AQIAvgDesc { get; set; }
		[HighLow(relatedProperties: nameof(AQIHighDesc)), JsonPropertyName("aqi_hi_val")]
		public float? AQIHighVal { get; set; }
		[JsonPropertyName("aqi_hi_desc")]
		public string? AQIHighDesc { get; set; }
	}
}
