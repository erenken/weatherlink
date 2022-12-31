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
	}
}
