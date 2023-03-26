using myNOC.WeatherLink.Attributes;
using myNOC.WeatherLink.Models.Sensors;
using myNOC.WeatherLink.Resolvers;
using myNOC.WeatherLink.Responses;
using myNOC.WeatherLink.Sensors.Data;
using System.Collections;
using System.Collections.Concurrent;

namespace myNOC.WeatherLink.Processors
{
	public class HighLowProcessor : IHighLowProcessor
	{
		private readonly ConcurrentDictionary<Type, IList> _typeIList = new();
		private readonly IObjectTypeInfoResolver _objectTypeInfoResolver;

		public HighLowProcessor(IObjectTypeInfoResolver objectTypeInfoResolver)
		{
			_objectTypeInfoResolver = objectTypeInfoResolver;
		}

		public WeatherDataResponse CalculateHighLow(WeatherDataResponse response)
		{
			foreach (var sensor in response.Sensors)
			{
				if (sensor == null) continue;
				Type sensorType = sensor.GetType();
				if (sensorType.IsGenericType
					&& sensorType.GetGenericTypeDefinition() == typeof(Sensor<>))
				{
					var genericSensor = Convert.ChangeType(sensor, sensorType);
					var sensorData = _objectTypeInfoResolver.GetPropertyValue(genericSensor, "Data") as IEnumerable<ISensorData>;
					var baseData = sensorData?.FirstOrDefault();

					if (sensorData != null && baseData != null)
					{
						foreach (var data in sensorData!)
							ProcessHighLowAttributes(baseData, data);

						var sensorListInstance = GetIListForSensorType(sensorType);
						sensorListInstance.Add(baseData);

						_objectTypeInfoResolver.SetPropertyValue(genericSensor, "Data", sensorListInstance);
					}
				}
			}

			return response;
		}

		private IList GetIListForSensorType(Type sensorType)
		{
			if (!_typeIList.TryGetValue(sensorType, out var sensorListInstance))
			{
				var sensorListGenericType = typeof(List<>).MakeGenericType(sensorType.GetGenericArguments()[0]);
				sensorListInstance = (IList)Activator.CreateInstance(sensorListGenericType)!;
			}

			return sensorListInstance;
		}

		private void ProcessHighLowAttributes(dynamic baseData, dynamic data)
		{
			List<PropertyAttribute<HighLowAttribute>> propertyAttributes = _objectTypeInfoResolver.PropertyAttributes<HighLowAttribute>(data);

			foreach (var propertyAttribute in propertyAttributes)
			{
				var attribute = propertyAttribute.Attribute;
				var propertyName = propertyAttribute.Name;

				if (attribute != null)
				{
					var dataValue = _objectTypeInfoResolver.GetPropertyValue(data, propertyName);
					var baseValue = _objectTypeInfoResolver.GetPropertyValue(baseData, propertyName);

					if (attribute.HighOrLow == HighOrLow.High)
					{
						if (dataValue > baseValue)
							SetPropertyValues(attribute, propertyName, baseData, data, dataValue);
					}
					else
					{
						if (dataValue < baseValue)
							SetPropertyValues(attribute, propertyName, baseData, data, dataValue);
					}
				}
			}
		}

		private void SetPropertyValues(HighLowAttribute? attribute, string propertyName, dynamic baseData, dynamic data, dynamic dataValue)
		{
			_objectTypeInfoResolver.SetPropertyValue(baseData, propertyName, dataValue);
			if (attribute?.RelatedProperties != null)
			{
				foreach (var relatedProperty in attribute.RelatedProperties)
				{
					var propertyValue = _objectTypeInfoResolver.GetPropertyValue(data, relatedProperty);
					_objectTypeInfoResolver.SetPropertyValue(baseData, relatedProperty, propertyValue);
				}
			}
		}
	}
}
