namespace myNOC.WeatherLink.Sensors.Data
{
	public abstract class SensorData : ISensorData
	{
		public SensorData(int sensorType, string description)
		{
			Type = sensorType;
			Description = description;
		}

		public int Type { get; private set; }

		public string Description { get; private set; }
	}
}
