using myNOC.WeatherLink.JsonConverters;
using myNOC.WeatherLink.Models;
using myNOC.WeatherLink.Models.Sensors;
using myNOC.WeatherLink.Responses;
using myNOC.WeatherLink.Sensors.Data;
using NSubstitute;
using System.Text;
using System.Text.Json;

namespace myNOC.Tests.WeatherLink.JsonConverters
{
	[TestClass]
	public class SensorJsonConverterTests
	{
		private SensorJsonConverter _sensorJsonConverter = default!;
		private ISensorFactory _sensorFactory = Substitute.For<ISensorFactory>();

		[TestInitialize]
		public void TestInit()
		{
			_sensorJsonConverter = new SensorJsonConverter(_sensorFactory);
		}

		[TestMethod]
		public void CanConvert_Current_ReturnsTrue()
		{
			//	Assemble
			var typeToConvert = typeof(Sensor);

			//	Act
			var result = _sensorJsonConverter.CanConvert(typeToConvert);

			//	Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void CanConvert_Station_ReturnsFalse()
		{
			//	Assemble
			var typeToConvert = typeof(CurrentResponse);

			//	Act
			var result = _sensorJsonConverter.CanConvert(typeToConvert);

			//	Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void Deserialize_CurrentJson_ReturnCurrent()
		{
			//	Assemble
			var currentWeatherJson = Encoding.UTF8.GetString(Properties.Resources.CurrentWeather);

			JsonSerializerOptions options = new();
			options.Converters.Add(new SensorJsonConverter(_sensorFactory));

			_sensorFactory.GetSensorType(Arg.Is(46)).Returns(typeof(DavisVantagePro2Plus));
			_sensorFactory.GetSensorType(Arg.Is(323)).Returns(typeof(AirLink));

			//	Act
			var result = JsonSerializer.Deserialize<CurrentResponse>(currentWeatherJson, options);

			//	Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1671769064, result.UnixGeneratedAt);
			Assert.AreEqual(88769, result.StationId);
			Assert.AreEqual(6, result.Sensors.Count());

			var airlink = result?.Sensors.FirstOrDefault(x => x?.Type == 323) as Sensor<AirLink>;
			var davis = result?.Sensors.FirstOrDefault(x => x?.Type == 46) as Sensor<DavisVantagePro2Plus>;

			Assert.AreEqual(75, airlink?.Data?.FirstOrDefault()?.Humidity);
			Assert.AreEqual(12.1f, davis?.Data?.FirstOrDefault()?.Temperature);
		}
	}
}
