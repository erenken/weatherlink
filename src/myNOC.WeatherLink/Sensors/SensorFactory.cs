namespace myNOC.WeatherLink.Sensors
{
	public class SensorFactory : ISensorFactory
	{
		private readonly IEnumerable<ISensor> _sensors;

		public SensorFactory(IEnumerable<ISensor> sensors)
		{
			_sensors = sensors;
		}

		public Type? GetSensorType(int sensorType)
		{
			return _sensors.FirstOrDefault(x => x.Type == sensorType)?.GetType();
		}
	}
}
