namespace myNOC.WeatherLink.API
{
	public interface IAPIContext
	{
		string APIKey { get; set; }
		string APISecret { get; set; }
	}
}
