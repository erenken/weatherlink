// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using myNOC.WeatherLink;
using myNOC.WeatherLink.API;
using System.Text.Json;

IServiceCollection services = new ServiceCollection();
IServiceProvider serviceProvider = default!;

services.AddWeatherLink();
serviceProvider = services.BuildServiceProvider();

var apiHttpClient = serviceProvider.GetService<IAPIHttpClient>()!;
apiHttpClient.BaseUri = "https://api.weatherlink.com/v2";

var apiContext = serviceProvider.GetService<IAPIContext>()!;
apiContext.APIKey = "{yourApiKey}";
apiContext.APISecret = "{yourApiSecret}";

var apiClient = serviceProvider.GetService<IClient>()!;
var stations = await apiClient.GetStations();
Console.WriteLine(JsonSerializer.Serialize(stations));

Console.WriteLine();

var stationId = stations!.Stations!.ToList()[0].Id;
var current = await apiClient.GetCurrent(stationId);
Console.WriteLine(JsonSerializer.Serialize(current));


Console.ReadLine();


