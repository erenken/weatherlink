using myNOC.WeatherLink.Models.Sensors;
using myNOC.WeatherLink.Sensors.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.JsonConverters
{
	public class SensorJsonConverterFactory : JsonConverterFactory
	{
		private readonly ISensorFactory _sensorFactory;

		public SensorJsonConverterFactory(ISensorFactory sensorFactory)
		{
			_sensorFactory = sensorFactory;
		}

		public override bool CanConvert(Type typeToConvert)
		{
			if (typeToConvert == typeof(Sensor))
				return true;

			if (!typeToConvert.IsGenericType)
				return false;

			if (typeToConvert.GetGenericTypeDefinition() != typeof(Sensor<>))
				return false;

			return true;
		}

		public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			if (!typeToConvert.IsGenericType)
				return new SensorJsonConverter(_sensorFactory);

			Type sensorDataType = typeToConvert.GetGenericArguments()[0];

			var converter = (JsonConverter)Activator.CreateInstance(typeof(SensorTConverter<>).MakeGenericType(sensorDataType))!;
			return converter;
		}

		private class SensorTConverter<T> : JsonConverter<Sensor<T>> where T : ISensorData
		{
			public SensorTConverter()
			{
			}

			public override Sensor<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				var sensor = JsonSerializer.Deserialize(ref reader, typeToConvert) as Sensor<T>;
				return sensor!;
			}

			public override void Write(Utf8JsonWriter writer, Sensor<T> value, JsonSerializerOptions options)
			{
				JsonSerializer.Serialize(writer, value, typeof(Sensor<T>));
			}
		}

		private class SensorJsonConverter : JsonConverter<Sensor>
		{
			private readonly ISensorFactory _sensorFactory;

			public SensorJsonConverter(ISensorFactory sensorFactory)
			{
				_sensorFactory = sensorFactory;
			}

			public override Sensor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				var readerClone = reader;
				var sensor = JsonSerializer.Deserialize<Sensor>(ref reader)!;

				var sensorType = _sensorFactory.GetSensorType(sensor.Type);
				if (sensorType == null)
					return null;

				var genericSensor = typeof(Sensor<>).MakeGenericType(sensorType);
				var deserialized = JsonSerializer.Deserialize(ref readerClone, genericSensor, options) as Sensor;

				return deserialized;
			}

			public override void Write(Utf8JsonWriter writer, Sensor value, JsonSerializerOptions options)
			{
				JsonSerializer.Serialize(writer, value, value.GetType(), options);
			}
		}
	}
}
