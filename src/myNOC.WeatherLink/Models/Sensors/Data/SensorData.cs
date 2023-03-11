using myNOC.WeatherLink.Models.Sensors.Data;

namespace myNOC.WeatherLink.Sensors.Data
{
	public abstract class SensorData : ISensorData
	{
		public SensorData(SensorType sensorType, string description)
		{
			Type = sensorType;
			Description = description;
		}

		public SensorType Type { get; private set; }

		public string Description { get; private set; }
	}
}
