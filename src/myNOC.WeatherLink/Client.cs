using myNOC.WeatherLink.API;

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
	}
}
