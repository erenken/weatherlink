using myNOC.WeatherLink.Models.Sensors;
using myNOC.WeatherLink.Sensors.Data;

namespace myNOC.WeatherLink.Extensions
{
	public static class ListExtensions
	{
		public static IEnumerable<Sensor?> IdentifiedSensors(this IEnumerable<Sensor?> sensors)
		{
			foreach (var sensor in sensors)
			{
				if (sensor != null)
					yield return sensor;
			}
		}
	}
}
