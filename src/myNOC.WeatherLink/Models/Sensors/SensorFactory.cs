using myNOC.WeatherLink.Models.Sensors;
using myNOC.WeatherLink.Models.Sensors.Data;
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

		public Type? GetSensorType(SensorType sensorType, DataStructureType dataStructureType)
		{
			return _sensors.FirstOrDefault(x => x.Type == sensorType && x.DataStructure == dataStructureType)?.GetType();
		}
	}
}
