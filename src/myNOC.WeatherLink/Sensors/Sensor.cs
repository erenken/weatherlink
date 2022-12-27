using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.Sensors
{
	public abstract class Sensor : ISensor
	{
		private readonly int _sensorType;

		public Sensor(int sensorType)
		{
			_sensorType = sensorType;
		}

		[JsonPropertyName("lsid")]
		public int Id { get; set; }
		[JsonPropertyName("sensor_type")]
		public int Type => _sensorType;
		[JsonPropertyName("data_structure_type")]
		public int DataStructure { get; set; }
	}
}
