namespace myNOC.WeatherLink.Sensors
{
	public interface ISensorFactory
	{
		ISensor? GetSensor(int sensorType);
	}
}
