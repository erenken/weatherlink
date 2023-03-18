using Microsoft.Extensions.DependencyInjection;
using myNOC.WeatherLink;
using myNOC.WeatherLink.Extensions;
using myNOC.WeatherLink.JsonConverters;
using myNOC.WeatherLink.Responses;
using System.Text;
using System.Text.Json;

namespace myNOC.Tests.WeatherLink.Extensions
{
	[TestClass]
	public class WeatherDataResponseExtensionsTests
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
		public void WeatherDataResponseExtensionsTest()
		{
			//	Assemble
			var historicWeatherJson = Encoding.UTF8.GetString(Properties.Resources.HistoricWeather);

			JsonSerializerOptions options = new();
			options.Converters.Add(_serviceProvider.GetRequiredService<SensorJsonConverterFactory>());

			var response = JsonSerializer.Deserialize<WeatherDataResponse>(historicWeatherJson, options);

			//	Act
			var result = response!.CalculateHighLow();

			//	Assert
			Assert.IsNotNull(result);
		}
	}
}
