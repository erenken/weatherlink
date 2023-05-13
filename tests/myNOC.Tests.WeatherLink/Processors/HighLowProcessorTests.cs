using Microsoft.Extensions.DependencyInjection;
using myNOC.WeatherLink;
using myNOC.WeatherLink.JsonConverters;
using myNOC.WeatherLink.Models.Sensors;
using myNOC.WeatherLink.Processors;
using myNOC.WeatherLink.Responses;
using myNOC.WeatherLink.Sensors.Data;
using System.Text;
using System.Text.Json;

namespace myNOC.Tests.WeatherLink.Extensions
{
	[TestClass]
	public class HighLowProcessorTests
	{
		private IServiceCollection _services = default!;
		private IServiceProvider _serviceProvider = default!;
		
		[TestInitialize]
		public void TestInit()
		{
			_services = new ServiceCollection();
			_services.AddWeatherLink();

			_serviceProvider = _services.BuildServiceProvider();
		}

		[TestMethod]
		public void CalculateHighLowTest()
		{
			//	Assemble
			var historicWeatherJson = Encoding.UTF8.GetString(Properties.Resources.HistoricWeather);

			JsonSerializerOptions options = new();
			options.Converters.Add(_serviceProvider.GetRequiredService<SensorJsonConverterFactory>());

			var response = JsonSerializer.Deserialize<WeatherDataResponse>(historicWeatherJson, options);

			var highLowProcessor = _serviceProvider.GetRequiredService<IHighLowProcessor>();

			//	Act
			var result = highLowProcessor.CalculateHighLow(response!);
			var vantage = result.Sensors.FirstOrDefault(x => x?.Type == myNOC.WeatherLink.Models.Sensors.Data.SensorType.VantagePro2Plus
				&& x.DataStructure == myNOC.WeatherLink.Models.Sensors.Data.DataStructureType.ISSArchiveRecord) as Sensor<VantagePro2PlusArchive>;

			//	Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(vantage);
			Assert.AreEqual(0.24f, vantage.Data!.First().RainRateHigh_in);
			Assert.AreEqual(new DateTime(2023, 3, 17, 4, 17, 12, DateTimeKind.Utc), vantage.Data!.First().RainRateHighAt);
		}
	}
}
