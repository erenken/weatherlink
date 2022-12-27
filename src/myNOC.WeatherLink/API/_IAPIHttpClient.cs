namespace myNOC.WeatherLink.API
{
	public interface IAPIHttpClient
	{
		public string BaseUri { get; set; }
		public HttpClient GetHttpClient();
	}
}
