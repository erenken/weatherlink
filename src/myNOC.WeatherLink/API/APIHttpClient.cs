namespace myNOC.WeatherLink.API
{
	public class APIHttpClient : IAPIHttpClient
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public APIHttpClient(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public string? BaseUri { get; set; }
		public string? HttpClientFactoryName { get; set; }
		public HttpClient GetHttpClient()
		{
			HttpClient httpClient;
			if (string.IsNullOrEmpty(HttpClientFactoryName))
				httpClient = new HttpClient();
			else
				httpClient = _httpClientFactory.CreateClient(HttpClientFactoryName);

			if (!string.IsNullOrEmpty(BaseUri))
				httpClient.BaseAddress = new Uri(BaseUri);

			return httpClient;
		}
	}
}
