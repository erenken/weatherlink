using myNOC.WeatherLink.Models.Sensors;
using myNOC.WeatherLink.Sensors.Data;

namespace myNOC.WeatherLink.Sensors
{
	public class SensorFactory : ISensorFactory
	{
		private readonly IEnumerable<ISensorData> _sensors;

		public SensorFactory(IEnumerable<ISensorData> sensors)
		{
			_sensors = sensors;
		}

		public Type? GetSensorType(int sensorType)
		{
			return _sensors.FirstOrDefault(x => x.Type == sensorType)?.GetType();
		}
	}
}
