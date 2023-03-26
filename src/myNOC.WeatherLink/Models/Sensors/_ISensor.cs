using myNOC.WeatherLink.Models.Sensors.Data;
using myNOC.WeatherLink.Sensors.Data;
using System.Collections;

namespace myNOC.WeatherLink.Models.Sensors
{
	public interface ISensor
	{
		int Id { get; set; }
		SensorType Type { get; set; }
		DataStructureType DataStructure { get; set; }
	}

	public interface ISensor<T> : ISensor where T : ISensorData
	{
		List<T>? Data { get; set; }
	}
}
