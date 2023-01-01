using myNOC.WeatherLink.Models.Sensors;

namespace myNOC.WeatherLink.Extensions
{
	public static class IEnermerableExtensions
	{
		public static IEnumerable<Sensor> IdentifiedSensors(this IEnumerable<Sensor?> sensors)
		{
			foreach (var sensor in sensors)
			{
				if (sensor != null)
					yield return sensor;
			}
		}
	}
}
