using myNOC.WeatherLink;

namespace myNOC.WeatherLink
{
	public interface IClient
	{
		Task<Stations?> GetStations();
	}
}
