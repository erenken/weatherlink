using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.Sensors.Data
{
	public abstract class SensorData : ISensorData
	{
		public SensorData(int sensorType)
		{
			Type = sensorType;
		}

		[JsonIgnore]
		public int Type { get; private set; }
	}
}
