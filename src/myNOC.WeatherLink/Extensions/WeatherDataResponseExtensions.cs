using myNOC.WeatherLink.Models.Sensors;
using myNOC.WeatherLink.Responses;
using System.Reflection;

namespace myNOC.WeatherLink.Extensions
{
	public static class WeatherDataResponseExtensions
	{
		public static WeatherDataResponse CalculateHighLow(this WeatherDataResponse response)
		{

			foreach (dynamic? sensor in response.Sensors)
			{
				if (sensor == null) continue;
				if (sensor.IsAssignableTo(typeof(Sensors.Data.SensorData)))
				{
					foreach (var data in sensor.Data)
					{
						Console.WriteLine(data.ToString());
					}
				}
			}

			return response;
		}
	}
}
