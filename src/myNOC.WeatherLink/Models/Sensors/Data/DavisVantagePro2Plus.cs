using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.Sensors.Data
{
	public class DavisVantagePro2Plus : SensorData
	{
		public DavisVantagePro2Plus() : base(46, "Vantage Pro2 Plus /w 24-hr-Fan-Aspirated Radiation shield, UV & Solar Radiation") { }
		[JsonPropertyName("rx_state")]
		public int? RxState { get; set; }
		[JsonPropertyName("wind_speed_hi_last_2_min")]
		public int? WindSpeedHiLast_2Min { get; set; }
		[JsonPropertyName("hum")]
		public float? Humidity { get; set; }
		[JsonPropertyName("wind_dir_at_hi_speed_last_10_min")]
		public int? WindDirectionAtHiSpeedLast_10Min { get; set; }
		[JsonPropertyName("wind_chill")]
		public float? WindChill { get; set; }
		[JsonPropertyName("rain_rate_hi_last_15_min_clicks")]
		public int? RainRateHiLast_15MinClicks { get; set; }
		[JsonPropertyName("thw_index")]
		public float? THWIndex { get; set; }
		[JsonPropertyName("wind_dir_scalar_avg_last_10_min")]
		public float? WindDirectionScalarAvgLast_10Min { get; set; }
		[JsonPropertyName("rain_size")]
		public float? RainCupSize { get; set; }
		[JsonPropertyName("uv_index")]
		public float? UVIndex { get; set; }
		[JsonPropertyName("wind_speed_last")]
		public int? WindSpeed { get; set; }
		[JsonPropertyName("rainfall_last_60_min_clicks")]
		public int? RainfallLast_60MinClicks { get; set; }
		[JsonPropertyName("wet_bulb")]
		public float? WetBulb { get; set; }
		[JsonPropertyName("rainfall_monthly_clicks")]
		public int? RainfallMonthlyClicks { get; set; }
		[JsonPropertyName("wind_speed_avg_last_10_min")]
		public float? WindSpeedAvgLast_10Min { get; set; }
		[JsonPropertyName("wind_dir_at_hi_speed_last_2_min")]
		public int? WindDirectionAtHiSpeedLast_2Min { get; set; }
		[JsonPropertyName("rainfall_daily_in")]
		public float? RainfallDaily_in { get; set; }
		[JsonPropertyName("wind_dir_last")]
		public int? WindDir { get; set; }
		[JsonPropertyName("rainfall_daily_mm")]
		public float? RainfallDaily_mm { get; set; }
		[JsonPropertyName("rain_storm_last_clicks")]
		public int? RainStormLastClicks { get; set; }
		[JsonPropertyName("tx_id")]
		public int? TXId { get; set; }
		[JsonPropertyName("rain_storm_last_start_at")]
		public int? UnixRainStormLastStartAt { get; set; }
		public DateTimeOffset RainStormLastStartAt => DateTimeOffset.FromUnixTimeSeconds(UnixRainStormLastStartAt ?? 0);
		[JsonPropertyName("rain_rate_hi_clicks")]
		public int? RainRateHiClicks { get; set; }
		[JsonPropertyName("rainfall_last_15_min_in")]
		public float? RainfallLast_15Min_in { get; set; }
		[JsonPropertyName("rainfall_daily_clicks")]
		public int? RainfallDailyClicks { get; set; }
		[JsonPropertyName("dew_point")]
		public float? DewPoint { get; set; }
		[JsonPropertyName("rainfall_last_15_min_mm")]
		public float? RainfallLast_15Min_mm { get; set; }
		[JsonPropertyName("rain_rate_hi_in")]
		public float? RainRateHi_in { get; set; }
		[JsonPropertyName("rain_storm_clicks")]
		public int? RainStormClicks { get; set; }
		[JsonPropertyName("rain_rate_hi_mm")]
		public float? RainRateHi_mm { get; set; }
		[JsonPropertyName("rainfall_year_clicks")]
		public int? RainfallYearClicks { get; set; }
		[JsonPropertyName("rain_storm_in")]
		public float? RainStorm_in { get; set; }
		[JsonPropertyName("rain_storm_last_end_at")]
		public int? UnixRainStormLastEndAt { get; set; }
		public DateTimeOffset RainStormLastEndAt => DateTimeOffset.FromUnixTimeSeconds(UnixRainStormLastEndAt ?? 0);
		[JsonPropertyName("rain_storm_mm")]
		public float? RainStorm_mm { get; set; }
		[JsonPropertyName("wind_dir_scalar_avg_last_2_min")]
		public float? WindDirectionScalarAvgLast_2Min { get; set; }
		[JsonPropertyName("heat_index")]
		public float? HeatIndex { get; set; }
		[JsonPropertyName("rainfall_last_24_hr_in")]
		public float? RainfallLast_24Hr_in { get; set; }
		[JsonPropertyName("rainfall_last_60_min_mm")]
		public float? RainfallLast_60Min_mm { get; set; }
		[JsonPropertyName("trans_battery_flag")]
		public int? TransmitterBatteryFlag { get; set; }
		[JsonPropertyName("rainfall_last_60_min_in")]
		public float? RainfallLast_60Min_in { get; set; }
		[JsonPropertyName("rain_storm_start_time")]
		public int? UnixRainStormStartTime { get; set; }
		public DateTimeOffset RainStormStartTime => DateTimeOffset.FromUnixTimeSeconds(UnixRainStormStartTime ?? 0);
		[JsonPropertyName("rainfall_last_24_hr_mm")]
		public float? RainfallLast_24Hr_bm { get; set; }
		[JsonPropertyName("rainfall_year_in")]
		public float? RainfallYear_in { get; set; }
		[JsonPropertyName("wind_speed_hi_last_10_min")]
		public int? WindSpeedHiLast_10Min { get; set; }
		[JsonPropertyName("rainfall_last_15_min_clicks")]
		public int? RainfallLast_15MinClicks { get; set; }
		[JsonPropertyName("rainfall_year_mm")]
		public float? RainfallYear_mm { get; set; }
		[JsonPropertyName("wind_dir_scalar_avg_last_1_min")]
		public float? WindDirScalarAvgLast_1Min { get; set; }
		[JsonPropertyName("temp")]
		public float? Temperature { get; set; }
		[JsonPropertyName("wind_speed_avg_last_2_min")]
		public float? WindSpeedAvgLast_2Min { get; set; }
		[JsonPropertyName("solar_rad")]
		public int? SolarRadiation { get; set; }
		[JsonPropertyName("rainfall_monthly_mm")]
		public float? RainfallMonthly_mm { get; set; }
		[JsonPropertyName("rain_storm_last_mm")]
		public float? RainStormLast_mm { get; set; }
		[JsonPropertyName("wind_speed_avg_last_1_min")]
		public float? WindSpeedAvgLast_1Min { get; set; }
		[JsonPropertyName("thsw_index")]
		public float? THSWIndex { get; set; }
		[JsonPropertyName("rainfall_monthly_in")]
		public float? RainfallMonthly_in { get; set; }
		[JsonPropertyName("rain_rate_last_mm")]
		public float? RainRateLast_mm { get; set; }
		[JsonPropertyName("rain_rate_last_clicks")]
		public int? RainRateLastClicks { get; set; }
		[JsonPropertyName("rainfall_last_24_hr_clicks")]
		public int? RainfallLast_24HrClicks { get; set; }
		[JsonPropertyName("rain_storm_last_in")]
		public float? RainStormLast_in { get; set; }
		[JsonPropertyName("rain_rate_last_in")]
		public float? RainRateLast_in { get; set; }
		[JsonPropertyName("rain_rate_hi_last_15_min_mm")]
		public float? RainRateHiLast_15Min_mm { get; set; }
		[JsonPropertyName("rain_rate_hi_last_15_min_in")]
		public float? RainRateHiLast_15Min_in { get; set; }
		[JsonPropertyName("ts")]
		public int? UnixTimeStamp { get; set; }
		public DateTimeOffset TimeStamp => DateTimeOffset.FromUnixTimeSeconds(UnixTimeStamp ?? 0);
	}
}
