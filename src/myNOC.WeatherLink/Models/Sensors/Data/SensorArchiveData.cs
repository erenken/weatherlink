using myNOC.WeatherLink.Models.Sensors.Data;
using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.Sensors.Data
{
	public abstract class SensorArchiveData : SensorData
	{
		protected SensorArchiveData(SensorType sensorType, DataStructureType dataStructure, string description) : base(sensorType, dataStructure, description)
		{
		}

		[JsonPropertyName("arch_int")]
		public int? ArchiveInterval { get; set; }
	}
}
