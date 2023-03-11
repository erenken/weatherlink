using myNOC.WeatherLink.Models.Sensors.Data;

namespace myNOC.WeatherLink.Models.Sensors
{
	public interface ISensorFactory
	{
		Type? GetSensorType(SensorType sensorType);
	}
}
