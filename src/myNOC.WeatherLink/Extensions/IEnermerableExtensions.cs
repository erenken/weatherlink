using myNOC.WeatherLink.Sensors;

namespace myNOC.WeatherLink.Extensions
{
	public static class IEnermerableExtensions
	{
		public static IEnumerable<Sensor> RemoveNullSensors(this IEnumerable<Sensor> sensors)
		{
			foreach (var sensor in sensors)
			{
				if (sensor != null)
					yield return sensor;
			}
		}
	}
}
