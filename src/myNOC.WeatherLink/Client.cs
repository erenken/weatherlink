using myNOC.WeatherLink.API;
using myNOC.WeatherLink.Extensions;
using myNOC.WeatherLink.Processors;
using myNOC.WeatherLink.Responses;

namespace myNOC.WeatherLink
{
	public class Client : IClient
	{
		private readonly IAPIRepository _apiRepository;
		private readonly IHighLowProcessor _highLowProcessor;

		public Client(IAPIRepository apiRepository, IHighLowProcessor highLowProcessor)
		{
			_apiRepository = apiRepository;
			_highLowProcessor = highLowProcessor;
		}

		public async Task<StationsResponse?> GetStations()
		{
			return await _apiRepository.GetData<StationsResponse>("stations");
		}

		public async Task<WeatherDataResponse?> GetCurrent(int stationId)
		{
			var parameters = new Dictionary<string, string>();
			var stationIdKey = parameters.AddStationId(stationId);

			var excludeFromUrl = new string[] { stationIdKey };

			var current = await _apiRepository.GetData<WeatherDataResponse>($"current/{stationId}", parameters, excludeFromUrl);

			if (current != null)
				current.Sensors = current.Sensors.IdentifiedSensors();

			return current;
		}

		public async Task<WeatherDataResponse?> GetHistoric(int stationId, DateTime startDateTime, DateTime endDateTime)
		{
			var timeSpan = endDateTime - startDateTime;
			if (endDateTime < startDateTime) throw new ArgumentException($"{nameof(endDateTime)} must be greater than {nameof(startDateTime)}.");
			if (timeSpan.TotalHours > 24) throw new ArgumentException($"{nameof(endDateTime)} can not be more than 24 hours after the {nameof(startDateTime)}.");

			var parameters = new Dictionary<string, string>();
			var stationIdKey = parameters.AddStationId(stationId);

			parameters.AddTimeStamp("start-timestamp", startDateTime);
			parameters.AddTimeStamp("end-timestamp", endDateTime);

			var excludeFromUrl = new string[] { stationIdKey };

			var archive = await _apiRepository.GetData<WeatherDataResponse>($"historic/{stationId}", parameters, excludeFromUrl);

			if (archive != null)
				archive.Sensors = archive.Sensors.IdentifiedSensors();

			return archive;
		}

		public async Task<WeatherDataResponse?> GetHistoric(int stationId, DateOnly date)
		{
			var startDateTime = date.ToDateTime(new TimeOnly(0, 0, 0));
			var endDateTime = startDateTime.AddDays(1);

			return await GetHistoric(stationId, startDateTime, endDateTime);
		}

		public async Task<WeatherDataResponse?> GetHighsAndLows(int stationId, DateOnly date)
		{
			var historic = await GetHistoric(stationId, date);
			if (historic == null)
				return null;

			return _highLowProcessor.CalculateHighLow(historic);
		}
	}
}
