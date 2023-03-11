using myNOC.WeatherLink.Models.Sensors.Data;
using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.Sensors.Data
{
	public class AirLink : SensorData
	{
		public AirLink() : base(SensorType.AirLink, "AirLink") { }

		[JsonPropertyName("hum")]
		public float? Humidity { get; set; }

		[JsonPropertyName("pm_10_3_hour")]
		public float? PM10_3Hour { get; set; }
		[JsonPropertyName("pm_10_24_hour")]
		public float? PM10_24Hour { get; set; }
		[JsonPropertyName("pm_2p5_1_hour")]
		public float? PM2p5_1Hour { get; set; }
		[JsonPropertyName("aqi_nowcast_val")]
		public float? AQINowCast { get; set; }
		[JsonPropertyName("aqi_type")]
		public string? AQIType { get; set; }
		[JsonPropertyName("heat_index")]
		public float? HeatIndex { get; set; }
		[JsonPropertyName("pm_2p5_nowcast")]
		public float? PM2p5_NowCast { get; set; }
		[JsonPropertyName("pm_2p5_24_hour")]
		public float? PM2p5_24Hour { get; set; }
		[JsonPropertyName("pm_1")]
		public float? PM1 { get; set; }
		[JsonPropertyName("pct_pm_data_nowcast")]
		public float? PercentPMDataNowCast { get; set; }
		[JsonPropertyName("pct_pm_data_24_hour")]
		public float? PctPMData_24Hour { get; set; }
		[JsonPropertyName("wet_bulb")]
		public float? WetBulb { get; set; }
		[JsonPropertyName("aqi_val")]
		public float? AQI { get; set; }
		[JsonPropertyName("aqi_desc")]
		public string? AQIDescription { get; set; }
		[JsonPropertyName("temp")]
		public float? Temperature { get; set; }
		[JsonPropertyName("pm_2p5_3_hour")]
		public float? PM2p5_3Hour { get; set; }
		[JsonPropertyName("pct_pm_data_3_hour")]
		public float? PctPMData_3Hour { get; set; }
		[JsonPropertyName("last_report_time")]
		public int? UnixLastReportTime { get; set; }
		public DateTimeOffset LastReportTime => DateTimeOffset.FromUnixTimeSeconds(UnixLastReportTime ?? 0);
		[JsonPropertyName("aqi_nowcast_desc")]
		public string? AQINowCastDescription { get; set; }
		[JsonPropertyName("aqi_1_hour_val")]
		public float? AQI_1Hour { get; set; }
		[JsonPropertyName("pm_10_nowcast")]
		public float? PM10NowCast { get; set; }
		[JsonPropertyName("aqi_1_hour_desc")]
		public string? AQIDescription_1Hour { get; set; }
		[JsonPropertyName("pm_10_1_hour")]
		public float? PM10_1Hour { get; set; }
		[JsonPropertyName("dew_point")]
		public float? DewPoint { get; set; }
		[JsonPropertyName("pm_10")]
		public float? PM10 { get; set; }
		[JsonPropertyName("pct_pm_data_1_hour")]
		public float? PctPMData_1Hhour { get; set; }
		[JsonPropertyName("ts")]
		public int? UnixTimeStamp { get; set; }
		public DateTimeOffset TimeStamp => DateTimeOffset.FromUnixTimeSeconds(UnixTimeStamp ?? 0);
		[JsonPropertyName("pm_2p5")]
		public float? PM2p5 { get; set; }
	}
}
