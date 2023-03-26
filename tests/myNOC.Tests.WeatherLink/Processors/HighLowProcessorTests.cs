using Microsoft.Extensions.DependencyInjection;
using myNOC.WeatherLink;
using myNOC.WeatherLink.JsonConverters;
using myNOC.WeatherLink.Processors;
using myNOC.WeatherLink.Responses;
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

			//	Assert
			Assert.IsNotNull(result);
		}
	}
}
