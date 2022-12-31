using myNOC.WeatherLink.API;
using myNOC.WeatherLink.Extensions;
using myNOC.WeatherLink.Models;

namespace myNOC.WeatherLink
{
	public class Client : IClient
	{
		private readonly IAPIRepository _apiRepository;

		public Client(IAPIRepository apiRepository)
		{
			_apiRepository = apiRepository;
		}

		public async Task<Stations?> GetStations()
		{
			return await _apiRepository.GetData<Stations>("stations");
		}

		public async Task<Current?> GetCurrent(int stationId)
		{
			var parameters = new Dictionary<string, string>();
			var stationIdKey = parameters.AddStationId(stationId);

			var excludeFromUrl = new string[] { stationIdKey };

			var current = await _apiRepository.GetData<Current>($"current/{stationId}", parameters, excludeFromUrl);
			return current;
		}
	}
}
