namespace myNOC.WeatherLink.Models.Sensors
{
	public interface ISensorFactory
	{
		Type? GetSensorType(int sensorType);
	}
}
