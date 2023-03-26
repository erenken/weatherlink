using myNOC.WeatherLink.Models.Sensors.Data;

namespace myNOC.WeatherLink.Sensors.Data
{
	public abstract class SensorData : ISensorData
	{
		public SensorData(SensorType sensorType, DataStructureType dataStructure, string description)
		{
			Type = sensorType;
			DataStructure = dataStructure;
			Description = description;
		}

		public SensorType Type { get; private set; }
		public DataStructureType DataStructure { get; }
		public string Description { get; private set; }
	}
}
