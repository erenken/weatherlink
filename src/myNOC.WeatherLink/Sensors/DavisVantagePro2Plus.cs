namespace myNOC.WeatherLink.Sensors
{
	public class DavisVantagePro2Plus : Sensor
	{
		public DavisVantagePro2Plus() : base(sensorType: 46)
		{
		}

		public int rx_state { get; set; }
		public int wind_speed_hi_last_2_min { get; set; }
		public float hum { get; set; }
		public int wind_dir_at_hi_speed_last_10_min { get; set; }
		public float wind_chill { get; set; }
		public int rain_rate_hi_last_15_min_clicks { get; set; }
		public float thw_index { get; set; }
		public int wind_dir_scalar_avg_last_10_min { get; set; }
		public int rain_size { get; set; }
		public int uv_index { get; set; }
		public int wind_speed_last { get; set; }
		public int rainfall_last_60_min_clicks { get; set; }
		public float wet_bulb { get; set; }
		public int rainfall_monthly_clicks { get; set; }
		public int wind_speed_avg_last_10_min { get; set; }
		public int wind_dir_at_hi_speed_last_2_min { get; set; }
		public float rainfall_daily_in { get; set; }
		public int wind_dir_last { get; set; }
		public float rainfall_daily_mm { get; set; }
		public int rain_storm_last_clicks { get; set; }
		public int tx_id { get; set; }
		public int rain_storm_last_start_at { get; set; }
		public int rain_rate_hi_clicks { get; set; }
		public int rainfall_last_15_min_in { get; set; }
		public int rainfall_daily_clicks { get; set; }
		public float dew_point { get; set; }
		public int rainfall_last_15_min_mm { get; set; }
		public int rain_rate_hi_in { get; set; }
		public int rain_storm_clicks { get; set; }
		public int rain_rate_hi_mm { get; set; }
		public int rainfall_year_clicks { get; set; }
		public float rain_storm_in { get; set; }
		public int rain_storm_last_end_at { get; set; }
		public float rain_storm_mm { get; set; }
		public int wind_dir_scalar_avg_last_2_min { get; set; }
		public float heat_index { get; set; }
		public float rainfall_last_24_hr_in { get; set; }
		public int rainfall_last_60_min_mm { get; set; }
		public int trans_battery_flag { get; set; }
		public int rainfall_last_60_min_in { get; set; }
		public int rain_storm_start_time { get; set; }
		public float rainfall_last_24_hr_mm { get; set; }
		public float rainfall_year_in { get; set; }
		public int wind_speed_hi_last_10_min { get; set; }
		public int rainfall_last_15_min_clicks { get; set; }
		public float rainfall_year_mm { get; set; }
		public int wind_dir_scalar_avg_last_1_min { get; set; }
		public float temp { get; set; }
		public int wind_speed_avg_last_2_min { get; set; }
		public int solar_rad { get; set; }
		public float rainfall_monthly_mm { get; set; }
		public float rain_storm_last_mm { get; set; }
		public int wind_speed_avg_last_1_min { get; set; }
		public float thsw_index { get; set; }
		public float rainfall_monthly_in { get; set; }
		public int rain_rate_last_mm { get; set; }
		public int rain_rate_last_clicks { get; set; }
		public int rainfall_last_24_hr_clicks { get; set; }
		public float rain_storm_last_in { get; set; }
		public int rain_rate_last_in { get; set; }
		public int rain_rate_hi_last_15_min_mm { get; set; }
		public int rain_rate_hi_last_15_min_in { get; set; }
		public int ts { get; set; }
	}
}
