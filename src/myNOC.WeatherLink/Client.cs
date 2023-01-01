using myNOC.WeatherLink.API;
using myNOC.WeatherLink.Extensions;
using myNOC.WeatherLink.Responses;

namespace myNOC.WeatherLink
{
	public class Client : IClient
	{
		private readonly IAPIRepository _apiRepository;

		public Client(IAPIRepository apiRepository)
		{
			_apiRepository = apiRepository;
		}

		public async Task<StationsResponse?> GetStations()
		{
			return await _apiRepository.GetData<StationsResponse>("stations");
		}

		public async Task<CurrentResponse?> GetCurrent(int stationId)
		{
			var parameters = new Dictionary<string, string>();
			var stationIdKey = parameters.AddStationId(stationId);

			var excludeFromUrl = new string[] { stationIdKey };

			var current = await _apiRepository.GetData<CurrentResponse>($"current/{stationId}", parameters, excludeFromUrl);

			if (current != null)
				current.Sensors = current.Sensors.IdentifiedSensors();

			return current;
		}
	}
}
