using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.Sensors.Data
{
	public abstract class SensorData : ISensorData
	{
		public SensorData(int sensorType, string description)
		{
			Type = sensorType;
			Description = description;
		}

		[JsonIgnore]
		public int Type { get; private set; }

		public string Description { get; private set; }
	}
}
