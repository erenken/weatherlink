namespace myNOC.WeatherLink.Sensors
{
	public interface ISensor
	{
		int Id { get; set; }
		int Type { get; }
		int DataStructure { get; set; }
	}
}
