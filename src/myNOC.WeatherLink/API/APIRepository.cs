using myNOC.WeatherLink.Resolvers;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Web;

namespace myNOC.WeatherLink.API
{
	public class APIRepository : IAPIRepository
	{
		private readonly IAPIHttpClient _apiHttpClient;
		private readonly IAPIQueryStringResolver _apiQueryStringResolver;

		public APIRepository(IAPIHttpClient apiHttpClient, IAPIQueryStringResolver apiQueryStringResolver)
		{
			_apiHttpClient = apiHttpClient;
			_apiQueryStringResolver = apiQueryStringResolver;
		}

		public async Task<T?> GetData<T>(string endPoint, IEnumerable<KeyValuePair<string, string>>? parameters = null, IEnumerable<string>? calculateOnly = null) where T : IResponse
		{
			var response = await CallWeatherLink(endPoint, parameters, calculateOnly);
			return JsonSerializer.Deserialize<T>(response);
		}

		public async Task<JsonNode?> GetData(string endPoint, IEnumerable<KeyValuePair<string, string>>? parameters = null, IEnumerable<string>? calculateOnly = null)
		{
			var response = await CallWeatherLink(endPoint, parameters, calculateOnly);
			return JsonNode.Parse(response);
		}

		private async Task<string> CallWeatherLink(string endPoint, IEnumerable<KeyValuePair<string, string>>? parameters = null, IEnumerable<string>? calculateOnly = null)
		{
			var sortedParameters = _apiQueryStringResolver.Build(parameters);

			var httpClient = _apiHttpClient.GetHttpClient();
			StringBuilder queryBuilder = new();

			if (sortedParameters != null)
			{
				foreach (var param in sortedParameters.Where(x => !calculateOnly?.Contains(x.Key) ?? true))
					queryBuilder.Append($"{param.Key}={HttpUtility.UrlEncode(param.Value)}&");
			}

			var baseUri = new Uri(_apiHttpClient.BaseUri);
			string path = baseUri.AbsolutePath.TrimEnd('/');

			var uri = new Uri(baseUri, $"{path}/{endPoint}?{queryBuilder}");
			var response = await httpClient.GetAsync(uri);
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			return content;
		}
	}
}
