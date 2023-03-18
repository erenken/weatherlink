using myNOC.WeatherLink.Models.Sensors.Data;
using myNOC.WeatherLink.Sensors.Data;
using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.Models.Sensors
{
	public class Sensor : ISensor
	{
		[JsonPropertyName("lsid")]
		public int Id { get; set; }
		[JsonPropertyName("sensor_type")]
		public SensorType Type { get; set; }
		[JsonPropertyName("data_structure_type")]
		public DataStructureType DataStructure { get; set; }
		
	}

	public class Sensor<T> : Sensor, ISensor<T> where T : ISensorData
	{
		[JsonPropertyName("data")]
		public IEnumerable<T>? Data { get; set; }
	}
}
