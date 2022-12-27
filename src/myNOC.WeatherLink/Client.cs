using myNOC.WeatherLink.API;
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
			var parameters = new Dictionary<string, string>
			{
				{ "station-id", stationId.ToString() }
			};

			var calculateOnly = new string[] { "station-id" };

			var jsonNode = await _apiRepository.GetData($"current/{stationId}", parameters, calculateOnly);

			return new Current();
		}
	}
}
