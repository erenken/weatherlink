using myNOC.WeatherLink.Responses;

namespace myNOC.WeatherLink
{
	public interface IClient
	{
		Task<StationsResponse?> GetStations();
		Task<WeatherDataResponse?> GetCurrent(int stationId);
		Task<WeatherDataResponse?> GetHistoric(int stationId, DateTime startDateTime, DateTime endDateTime);
		Task<WeatherDataResponse?> GetHistoric(int stationId, DateOnly date);
		Task<WeatherDataResponse?> GetHighsAndLows(int stationId, DateOnly date);
	}
}
