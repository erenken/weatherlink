using myNOC.WeatherLink.Models.Sensors.Data;

namespace myNOC.WeatherLink.Sensors.Data
{
	public interface ISensorData
	{
		public SensorType Type { get; }
		public DataStructureType DataStructure { get; }
		public string Description { get; }
	}
}
