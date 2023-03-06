using Microsoft.Extensions.Logging;
using myNOC.WeatherLink.JsonConverters;
using myNOC.WeatherLink.Resolvers;
using myNOC.WeatherLink.Responses;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace myNOC.WeatherLink.API
{
	public class APIRepository : IAPIRepository
	{
		private readonly IAPIHttpClient _apiHttpClient;
		private readonly IAPIQueryStringResolver _apiQueryStringResolver;
		private readonly ILogger<APIRepository> _logger;
		private readonly SensorJsonConverterFactory _sensorJsonConverterFactory;

		public APIRepository(
			IAPIHttpClient apiHttpClient,
			IAPIQueryStringResolver apiQueryStringResolver,
			SensorJsonConverterFactory sensorJsonConverterFactory,
			ILogger<APIRepository> logger
			)
		{
			_apiHttpClient = apiHttpClient;
			_apiQueryStringResolver = apiQueryStringResolver;
			_sensorJsonConverterFactory = sensorJsonConverterFactory;
			_logger = logger;
		}

		public async Task<T?> GetData<T>(string endPoint, IEnumerable<KeyValuePair<string, string>>? parameters = null, IEnumerable<string>? excludeFromUrl = null) where T : IResponse
		{
			var response = await CallWeatherLink(endPoint, parameters, excludeFromUrl);
			_logger.LogDebug($"Response: {response}");

			JsonSerializerOptions options = new();
			options.Converters.Add(_sensorJsonConverterFactory!);

			return JsonSerializer.Deserialize<T>(response, options);
		}

		private async Task<string> CallWeatherLink(string endPoint, IEnumerable<KeyValuePair<string, string>>? parameters = null, IEnumerable<string>? excludeFromUrl = null)
		{
			var sortedParameters = _apiQueryStringResolver.Build(parameters);

			var httpClient = _apiHttpClient.GetHttpClient();
			StringBuilder queryBuilder = new();

			if (sortedParameters != null)
			{
				foreach (var param in sortedParameters.Where(x => !excludeFromUrl?.Contains(x.Key) ?? true))
					queryBuilder.Append($"{param.Key}={HttpUtility.UrlEncode(param.Value)}&");
			}

			var baseUri = new Uri(_apiHttpClient.BaseUri);
			string path = baseUri.AbsolutePath.TrimEnd('/');

			var uri = new Uri(baseUri, $"{path}/{endPoint}?{queryBuilder}");
			_logger.LogInformation($"Calling WeatherLink API Uri: {uri}");

			var response = await httpClient.GetAsync(uri);
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			return content;
		}
	}
}
