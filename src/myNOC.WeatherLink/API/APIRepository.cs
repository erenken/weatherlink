using myNOC.WeatherLink.Resolvers;
using System.Net.Http.Json;
using System.Text;
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

		public async Task<T?> GetData<T>(string endPoint, IEnumerable<KeyValuePair<string, string>>? parameters = null) where T : IResponse
		{
			var sortedParameters = _apiQueryStringResolver.Build(parameters);

			var httpClient = _apiHttpClient.GetHttpClient();
			var baseUri = new Uri(_apiHttpClient.BaseUri ?? string.Empty);

			StringBuilder queryBuilder = new ();
			foreach(var param in sortedParameters)
				queryBuilder.Append($"{param.Key}={HttpUtility.UrlEncode(param.Value)}&");

			var uri = new Uri(baseUri, $"{baseUri.AbsolutePath}/{endPoint}?{queryBuilder}");
			var response = await httpClient.GetFromJsonAsync<T>(uri);
			return response;
		}
	}
}
