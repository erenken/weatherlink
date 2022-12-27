namespace myNOC.WeatherLink.Sensors
{
	public class SensorFactory : ISensorFactory
	{
		private readonly IEnumerable<ISensor> _sensors;

		public SensorFactory(IEnumerable<ISensor> sensors)
		{
			_sensors = sensors;
		}

		public ISensor? GetSensor(int sensorType)
		{
			return _sensors.FirstOrDefault(x => x.Id == sensorType);
		}
	}
}
