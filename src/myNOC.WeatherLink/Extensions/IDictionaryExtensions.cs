using System;

namespace myNOC.WeatherLink.Extensions
{
	public static class IDictionaryExtensions
	{
		public static string AddStationId(this IDictionary<string, string> dictionary, int stationId)
		{
			var key = "station-id";
			dictionary.TryAdd(key, stationId.ToString());

			return key;
		}

		public static string AddTimeStamp(this IDictionary<string, string> dictionary, string key, DateTime timestamp)
		{
			var dateTimeOffset = new DateTimeOffset(timestamp);
			dictionary.TryAdd(key, dateTimeOffset.ToUnixTimeSeconds().ToString());

			return key;
		}
	}
}
