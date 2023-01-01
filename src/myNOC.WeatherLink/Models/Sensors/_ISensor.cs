using myNOC.WeatherLink.Sensors.Data;

namespace myNOC.WeatherLink.Models.Sensors
{
	public interface ISensor
	{
		int Id { get; set; }
		int Type { get; set; }
		int DataStructure { get; set; }
	}

	public interface ISensor<T> : ISensor where T : ISensorData
	{
		public IEnumerable<T>? Data { get; set; }
	}
}
