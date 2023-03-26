using myNOC.WeatherLink.Attributes;
using myNOC.WeatherLink.Models.Sensors;
using myNOC.WeatherLink.Resolvers;
using myNOC.WeatherLink.Responses;

namespace myNOC.WeatherLink.Processors
{
	public class HighLowProcessor : IHighLowProcessor
	{
		private readonly IObjectTypeInfoResolver _objectTypeInfoResolver;

		public HighLowProcessor(IObjectTypeInfoResolver objectTypeInfoResolver)
		{
			_objectTypeInfoResolver = objectTypeInfoResolver;
		}

		public WeatherDataResponse CalculateHighLow(WeatherDataResponse response)
		{

			foreach (dynamic? sensor in response.Sensors)
			{
				if (sensor == null) continue;
				Type sensorType = sensor.GetType();
				if (sensorType.IsGenericType && sensorType.GetGenericTypeDefinition() == typeof(Sensor<>))
				{
					foreach (var data in sensor.Data)
					{
						var attributes = _objectTypeInfoResolver.PropertyAttributes<HighLowAttribute>(data);

						foreach (var attribute in attributes)
						{
						}
					}
				}
			}

			return response;
		}
	}
}
