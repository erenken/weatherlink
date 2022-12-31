namespace myNOC.WeatherLink.Sensors
{
	public interface ISensorFactory
	{
		Type? GetSensorType(int sensorType);
	}
}
