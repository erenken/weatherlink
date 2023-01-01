using myNOC.WeatherLink;
using myNOC.WeatherLink.API;
using myNOC.WeatherLink.Models;
using myNOC.WeatherLink.Models.Sensors;
using NSubstitute;

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
			var stations = new Stations
			{
				Station = new List<Station> { new Station { Active = true, City = "Niles", Region = "Michigan", Id = id } }
			};

			_apiRepository.GetData<Stations>("stations").Returns(stations);

			//	Act
			var result = await _client.GetStations();

			//	Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Station?.Count());

			var station = result.Station?.ToList()[0];
			Assert.IsNotNull(station);
			Assert.AreEqual(id, station.Id);
		}

		[TestMethod]
		public async Task Client_GetCurrent_ReturnsCurrent()
		{
			//	Assemble
			var stationId = 49120;
			var current = new Current
			{
				StationId = stationId,
				Sensors = new List<Sensor?> {
					new Sensor { Id = 1 },
					new Sensor { Id = 2 },
					null,
					null
				}
			};

			_apiRepository.GetData<Current>($"current/{stationId}", Arg.Any<IEnumerable<KeyValuePair<string, string>>?>(), Arg.Any<IEnumerable<string>?>()).Returns(current);

			//	Act
			var result = await _client.GetCurrent(stationId);

			//	Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, current.Sensors.Count());

			await _apiRepository.Received().GetData<Current>($"current/{stationId}",
				Arg.Is<IEnumerable<KeyValuePair<string, string>>?>(p => p!.FirstOrDefault(kv => kv.Key == "station-id").Value == stationId.ToString()),
				Arg.Is<IEnumerable<string>?>(p => p!.FirstOrDefault(kv => kv == "station-id") != null));
		}
	}
}
