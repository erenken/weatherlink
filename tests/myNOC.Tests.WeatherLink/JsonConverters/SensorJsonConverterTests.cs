using myNOC.WeatherLink.JsonConverters;
using myNOC.WeatherLink.Models.Sensors;
using myNOC.WeatherLink.Models.Sensors.Data;
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
		private SensorJsonConverterFactory _sensorJsonConverterFactory = default!;
		private ISensorFactory _sensorFactory = Substitute.For<ISensorFactory>();

		[TestInitialize]
		public void TestInit()
		{
			_sensorJsonConverterFactory = new SensorJsonConverterFactory(_sensorFactory);
		}

		[TestMethod]
		public void CanConvert_Current_ReturnsTrue()
		{
			//	Assemble
			var typeToConvert = typeof(Sensor);

			//	Act
			var result = _sensorJsonConverterFactory.CanConvert(typeToConvert);

			//	Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void CanConvert_Station_ReturnsFalse()
		{
			//	Assemble
			var typeToConvert = typeof(WeatherDataResponse);

			//	Act
			var result = _sensorJsonConverterFactory.CanConvert(typeToConvert);

			//	Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void Deserialize_CurrentJson_ReturnCurrent()
		{
			//	Assemble
			var currentWeatherJson = Encoding.UTF8.GetString(Properties.Resources.CurrentWeather);

			JsonSerializerOptions options = new();
			options.Converters.Add(_sensorJsonConverterFactory);

			_sensorFactory.GetSensorType(Arg.Is(SensorType.VantagePro2Plus), Arg.Is(DataStructureType.ISSCurrentConditionsRecord)).Returns(typeof(VantagePro2Plus));
			_sensorFactory.GetSensorType(Arg.Is(SensorType.AirLink), Arg.Is(DataStructureType.AirLinkCurrentConditionsRecord)).Returns(typeof(AirLink));

			//	Act
			var result = JsonSerializer.Deserialize<WeatherDataResponse>(currentWeatherJson, options);

			//	Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1677903687, result.UnixGeneratedAt);
			Assert.AreEqual(152788, result.StationId);
			Assert.AreEqual(6, result.Sensors.Count());

			var airlink = result?.Sensors.FirstOrDefault(x => x?.Type == SensorType.AirLink && x?.DataStructure == DataStructureType.AirLinkCurrentConditionsRecord) as Sensor<AirLink>;
			var davis = result?.Sensors.FirstOrDefault(x => x?.Type == SensorType.VantagePro2Plus && x?.DataStructure == DataStructureType.ISSCurrentConditionsRecord) as Sensor<VantagePro2Plus>;

			Assert.AreEqual(90.5f, airlink?.Data?.FirstOrDefault()?.Humidity);
			Assert.AreEqual(32.2f, davis?.Data?.FirstOrDefault()?.Temperature);
		}
	}
}
