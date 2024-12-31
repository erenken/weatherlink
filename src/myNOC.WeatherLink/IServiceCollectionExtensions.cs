using Microsoft.Extensions.DependencyInjection;
using myNOC.WeatherLink.API;
using myNOC.WeatherLink.JsonConverters;
using myNOC.WeatherLink.Models.Sensors;
using myNOC.WeatherLink.Processors;
using myNOC.WeatherLink.Resolvers;
using myNOC.WeatherLink.Sensors;
using myNOC.WeatherLink.Sensors.Data;
using myNOC.WeatherLink.Utilities;
using System.Reflection;

namespace myNOC.WeatherLink
{
	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddWeatherLink(this IServiceCollection services, bool registerIAuthenticationSingleton = true)
		{
			services.AddHttpClient();

			if (registerIAuthenticationSingleton)
				services.AddSingleton<IAPIContext, APIContext>();
			else
				services.AddScoped<IAPIContext, APIContext>();

			services.AddSingleton<ITimeStamp, TimeStamp>();
			services.AddSingleton<IAPIQueryStringResolver, APIQueryStringResolver>();
			services.AddSingleton<IObjectTypeInfoResolver, ObjectTypeInfoResolver>();
			services.AddSingleton<IHighLowProcessor, HighLowProcessor>();

			services.AddScoped<ISensorFactory, SensorFactory>();
			services.AddScoped(x => new SensorJsonConverterFactory(x.GetRequiredService<ISensorFactory>()));

			services.AddSingleton<IAPIHttpClient, APIHttpClient>();
			services.AddScoped<IAPIRepository, APIRepository>();
			services.AddScoped<IClient, Client>();
			services.AddAllScoped(typeof(ISensorData));

			return services;
		}

		public static IServiceCollection AddAllScoped(this IServiceCollection services, Type serviceType)
		{
			var types = Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(x => !x.IsAbstract && !x.IsInterface)
				.Where(x => serviceType.IsAssignableFrom(x));

			foreach(var type in types)
				services.AddScoped(serviceType, type);

			return services;
		}
	}
}
