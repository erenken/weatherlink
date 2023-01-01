using myNOC.WeatherLink.Responses;

namespace myNOC.WeatherLink
{
	public interface IClient
	{
		Task<StationsResponse?> GetStations();
		Task<CurrentResponse?> GetCurrent(int stationId);
	}
}
