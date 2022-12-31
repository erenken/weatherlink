using myNOC.WeatherLink.Sensors.Data;
using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.Sensors
{
	public class Sensor : ISensor
	{
		[JsonPropertyName("lsid")]
		public int Id { get; set; }
		[JsonPropertyName("sensor_type")]
		public int Type { get; set; }
		[JsonPropertyName("data_structure_type")]
		public int DataStructure { get; set; }
	}

	public class Sensor<T> : Sensor, ISensor<T> where T : ISensorData
	{
		[JsonPropertyName("data")]
		public IEnumerable<T>? Data { get; set; }
	}
}
