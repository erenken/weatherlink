namespace myNOC.WeatherLink.API
{
	public class APIHttpClient : IAPIHttpClient
	{
		public string BaseUri { get; set; } = default!;

		public HttpClient GetHttpClient()
		{
			HttpClient httpClient = new HttpClient();

			if (!string.IsNullOrEmpty(BaseUri))
				httpClient.BaseAddress = new Uri(BaseUri);

			return httpClient;
		}
	}
}
