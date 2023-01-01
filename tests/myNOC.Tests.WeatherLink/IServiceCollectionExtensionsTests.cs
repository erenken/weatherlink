using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using myNOC.WeatherLink;
using myNOC.WeatherLink.API;
using myNOC.WeatherLink.Models.Sensors;
using myNOC.WeatherLink.Resolvers;
using myNOC.WeatherLink.Sensors.Data;
using myNOC.WeatherLink.Utilities;

namespace myNOC.Tests.WeatherLink
{
	[TestClass]
	public class IServiceCollectionExtensionsTests
	{
		private IServiceCollection _services = default!;
		private IServiceProvider _serviceProvider = default!;

		[TestInitialize]
		public void TestInitialize()
		{
			_services = new ServiceCollection();
			_services.AddLogging(config => config.AddConsole());
		}

		[TestMethod]
		public void AddWeatherLink_registerIAUthenticationSingletonFalse_Scoped()
		{
			//	Assemble
			_services.AddWeatherLink(false);

			//	Act
			var service = _services.FirstOrDefault(x => x.ServiceType == typeof(IAPIContext));

			//	Assert
			Assert.IsNotNull(service);
			Assert.AreEqual(typeof(APIContext), service!.ImplementationType);
			Assert.AreEqual(ServiceLifetime.Scoped, service.Lifetime);
		}

		[TestMethod]
		public void AddWeatherLink_registerIAUthenticationSingletonTrue_Singleton()
		{
			//	Assemble
			_services.AddWeatherLink();

			//	Act
			var service = _services.FirstOrDefault(x => x.ServiceType == typeof(IAPIContext));

			//	Assert
			Assert.IsNotNull(service);
			Assert.AreEqual(typeof(APIContext), service!.ImplementationType);
			Assert.AreEqual(ServiceLifetime.Singleton, service.Lifetime);
		}

		[TestMethod]
		public void AddWeatherLink_Resolve_Interfaces()
		{
			//	Assemble
			_services.AddWeatherLink();
			_serviceProvider = _services.BuildServiceProvider();

			//	Act
			var apiContext = _serviceProvider.GetService<IAPIContext>();
			var timestamp = _serviceProvider.GetService<ITimeStamp>();
			var apiQueryStringResolver = _serviceProvider.GetService<IAPIQueryStringResolver>();
			var apiHttpClient = _serviceProvider.GetService<IAPIHttpClient>();
			var sensorFactory = _serviceProvider.GetService<ISensorFactory>();

			var apiRepository = _serviceProvider.GetService<IAPIRepository>();
			var client = _serviceProvider.GetService<IClient>();
			var sensors = _serviceProvider.GetServices<ISensorData>()?.ToList();

			//	Assert
			Assert.IsNotNull(apiContext);
			Assert.IsNotNull(timestamp);
			Assert.IsNotNull(apiQueryStringResolver);
			Assert.IsNotNull(apiHttpClient);
			Assert.IsNotNull(sensorFactory);

			Assert.IsNotNull(apiRepository);
			Assert.IsNotNull(client);
			Assert.IsNotNull(sensors);
			Assert.IsTrue(sensors.Any());
		}
	}
}
