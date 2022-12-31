using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.Sensors.Data
{
	public class AirLink : SensorData
	{
		public AirLink() : base(sensorType: 323) { }

		[JsonPropertyName("hum")]
		public int Humidity { get; set; }

		[JsonPropertyName("pm_10_3_hour")]
		public float PM_10_3_Hours { get; set; }
		public float pm_10_24_hour { get; set; }
		public float pm_2p5_1_hour { get; set; }
		public float aqi_nowcast_val { get; set; }
		public string? aqi_type { get; set; }
		public float heat_index { get; set; }
		public float pm_2p5_nowcast { get; set; }
		public float pm_2p5_24_hour { get; set; }
		public float pm_1 { get; set; }
		public int pct_pm_data_nowcast { get; set; }
		public int pct_pm_data_24_hour { get; set; }
		public float wet_bulb { get; set; }
		public float aqi_val { get; set; }
		public string? aqi_desc { get; set; }
		public float temp { get; set; }
		public float pm_2p5_3_hour { get; set; }
		public int pct_pm_data_3_hour { get; set; }
		public int last_report_time { get; set; }
		public string? aqi_nowcast_desc { get; set; }
		public float aqi_1_hour_val { get; set; }
		public float pm_10_nowcast { get; set; }
		public string? aqi_1_hour_desc { get; set; }
		public float pm_10_1_hour { get; set; }
		public int dew_point { get; set; }
		public float pm_10 { get; set; }
		public int pct_pm_data_1_hour { get; set; }
		public int ts { get; set; }
		public float pm_2p5 { get; set; }
	}
}
