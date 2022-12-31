using myNOC.WeatherLink.Sensors;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.JsonConverters
{
	public class SensorJsonConverter : JsonConverter<Sensor>
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
