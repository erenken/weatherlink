using myNOC.WeatherLink.Models;

namespace myNOC.WeatherLink
{
	public interface IClient
	{
		Task<Stations?> GetStations();
		Task<Current?> GetCurrent(int stationId);
	}
}
