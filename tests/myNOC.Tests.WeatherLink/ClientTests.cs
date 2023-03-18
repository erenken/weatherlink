using myNOC.WeatherLink;
using myNOC.WeatherLink.API;
using myNOC.WeatherLink.Models;
using myNOC.WeatherLink.Models.Sensors;
using myNOC.WeatherLink.Responses;
using NSubstitute;
using System.Collections.Generic;

namespace myNOC.Tests.WeatherLink
{
	[TestClass]
	public class ClientTests
	{
		private IAPIRepository _apiRepository = Substitute.For<IAPIRepository>();
		private IClient _client = default!;

		[TestInitialize]
		public void TestInit()
		{
			_client = new Client(_apiRepository);
		}

		[TestMethod]
		public async Task Client_GetStations_ReturnsStations()
		{
			//	Assemble
			var id = 49120;
			var stationResponse = new StationsResponse
			{
				Stations = new List<Station> { new Station { Active = true, City = "Niles", Region = "Michigan", Id = id } }
			};

			_apiRepository.GetData<StationsResponse>("stations").Returns(stationResponse);

			//	Act
			var result = await _client.GetStations();

			//	Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Stations?.Count());

			var station = result.Stations?.ToList()[0];
			Assert.IsNotNull(station);
			Assert.AreEqual(id, station.Id);
		}

		[TestMethod]
		public async Task Client_GetCurrent_ReturnsCurrent()
		{
			//	Assemble
			var stationId = 49120;
			var currentResponse = new WeatherDataResponse
			{
				StationId = stationId,
				Sensors = new List<Sensor?> {
					new Sensor { Id = 1 },
					new Sensor { Id = 2 },
					null,
					null
				}
			};

			_apiRepository.GetData<WeatherDataResponse>($"current/{stationId}", Arg.Any<IEnumerable<KeyValuePair<string, string>>?>(), Arg.Any<IEnumerable<string>?>()).Returns(currentResponse);

			//	Act
			var result = await _client.GetCurrent(stationId);

			//	Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, currentResponse.Sensors.Count());

			await _apiRepository.Received().GetData<WeatherDataResponse>($"current/{stationId}",
				Arg.Is<IEnumerable<KeyValuePair<string, string>>?>(p => p!.FirstOrDefault(kv => kv.Key == "station-id").Value == stationId.ToString()),
				Arg.Is<IEnumerable<string>?>(p => p!.FirstOrDefault(kv => kv == "station-id") != null));
		}

		[TestMethod]
		public async Task Client_GetArchive_ReturnsHistoric()
		{
			//	Assemble
			var stationId = 49120;
			var date = new DateOnly(2023, 3, 17);
			var historicResponse = new WeatherDataResponse
			{
				StationId = stationId,
				Sensors = new List<Sensor?> {
					new Sensor { Id = 1 },
					new Sensor { Id = 2 },
					null,
					null
				}
			};

			_apiRepository.GetData<WeatherDataResponse>($"historic/{stationId}", Arg.Any<IEnumerable<KeyValuePair<string, string>>?>(), Arg.Any<IEnumerable<string>?>()).Returns(historicResponse);

			//	Act
			var result = await _client.GetHistoric(stationId, date);

			//	Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, historicResponse.Sensors.Count());

			var dateTimeOffset = new DateTimeOffset(date.ToDateTime(new TimeOnly(0, 0, 0)));
			var startTimeStamp = dateTimeOffset.ToUnixTimeSeconds().ToString();
			var endTimeStamp = dateTimeOffset.AddDays(1).ToUnixTimeSeconds().ToString();

			await _apiRepository.Received().GetData<WeatherDataResponse>($"historic/{stationId}",
				Arg.Is<IEnumerable<KeyValuePair<string, string>>?>(p =>
					p!.FirstOrDefault(kv => kv.Key == "station-id").Value == stationId.ToString()
					&& p!.FirstOrDefault(kv => kv.Key == "start-timestamp").Value == startTimeStamp
					&& p!.FirstOrDefault(kv => kv.Key == "end-timestamp").Value == endTimeStamp
					),
				Arg.Is<IEnumerable<string>?>(p => p!.FirstOrDefault(kv => kv == "station-id") != null));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public async Task Client_GetArchive_StartGreaterThanEnd_ThrowsException()
		{
			//	Assemble
			var stationId = 49120;
			var startDate = new DateTime(2023, 3, 17);
			var endDate = new DateTime(2023, 3, 16);

			var historicResponse = new WeatherDataResponse
			{
				StationId = stationId,
				Sensors = new List<Sensor?> {
					new Sensor { Id = 1 },
					new Sensor { Id = 2 },
					null,
					null
				}
			};

			_apiRepository.GetData<WeatherDataResponse>($"historic/{stationId}", Arg.Any<IEnumerable<KeyValuePair<string, string>>?>(), Arg.Any<IEnumerable<string>?>()).Returns(historicResponse);

			//	Act
			var result = await _client.GetHistoric(stationId, startDate, endDate);

			//	Assert
			//	Throws Exception
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public async Task Client_GetArchive_StartEndGreaterThan24Hours_ThrowsException()
		{
			//	Assemble
			var stationId = 49120;
			var startDate = new DateTime(2023, 3, 17, 0, 0, 0);
			var endDate = new DateTime(2023, 3, 18, 0, 0, 1);

			var historicResponse = new WeatherDataResponse
			{
				StationId = stationId,
				Sensors = new List<Sensor?> {
					new Sensor { Id = 1 },
					new Sensor { Id = 2 },
					null,
					null
				}
			};

			_apiRepository.GetData<WeatherDataResponse>($"historic/{stationId}", Arg.Any<IEnumerable<KeyValuePair<string, string>>?>(), Arg.Any<IEnumerable<string>?>()).Returns(historicResponse);

			//	Act
			var result = await _client.GetHistoric(stationId, startDate, endDate);

			//	Assert
			//	Throws Exception
		}
	}
}
