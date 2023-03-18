// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using myNOC.WeatherLink;
using myNOC.WeatherLink.API;
using myNOC.WeatherLink.JsonConverters;
using System.Text.Json;

IServiceCollection services = new ServiceCollection();
IServiceProvider serviceProvider = default!;

IConfigurationBuilder configBuilder = new ConfigurationBuilder()
	.AddEnvironmentVariables();

IConfiguration config = configBuilder.Build();

services.AddLogging(options => options.AddConsole());
services.AddWeatherLink();
serviceProvider = services.BuildServiceProvider();

var apiHttpClient = serviceProvider.GetService<IAPIHttpClient>()!;
apiHttpClient.BaseUri = "https://api.weatherlink.com/v2";

var apiContext = serviceProvider.GetService<IAPIContext>()!;
apiContext.APIKey = config.GetValue<string>("WL_APIKEY")!;
apiContext.APISecret = config.GetValue<string>("WL_APISECRET")!;

var apiClient = serviceProvider.GetService<IClient>()!;
var stations = await apiClient.GetStations();
Console.WriteLine(JsonSerializer.Serialize(stations));

Console.WriteLine();

JsonSerializerOptions options = new();
var converterFactory = serviceProvider.GetService<SensorJsonConverterFactory>();
options.Converters.Add(converterFactory!);

var stationId = stations!.Stations!.ToList()[1].Id;
var current = await apiClient.GetHistoric(stationId, new DateOnly(2023,3,17));
var output = JsonSerializer.Serialize(current, options);
Console.WriteLine(output);

Console.ReadLine();
