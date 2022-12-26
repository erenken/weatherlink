using Microsoft.Extensions.DependencyInjection;
using myNOC.WeatherLink.API;
using myNOC.WeatherLink.Resolvers;
using myNOC.WeatherLink.Utilities;

namespace myNOC.WeatherLink
{
	public static class ServiceCollectionExtensions
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

			services.AddSingleton<IAPIHttpClient, APIHttpClient>();
			services.AddScoped<IAPIRepository, APIRepository>();
			services.AddScoped<IClient, Client>();

			return services;
		}
	}
}
