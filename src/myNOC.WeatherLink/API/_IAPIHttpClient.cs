namespace myNOC.WeatherLink.API
{
	public interface IAPIHttpClient
	{
		public string? BaseUri { get; set; }
		public string? HttpClientFactoryName { get; set; }
		public HttpClient GetHttpClient();
	}
}
