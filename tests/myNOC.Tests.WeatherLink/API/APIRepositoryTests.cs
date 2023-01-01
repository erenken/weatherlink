using myNOC.WeatherLink;
using myNOC.WeatherLink.API;
using myNOC.WeatherLink.Models.Sensors;
using myNOC.WeatherLink.Resolvers;
using NSubstitute;
using System.Net;
using System.Text.Json;

namespace myNOC.Tests.WeatherLink.API
{
	[TestClass]
	public class APIRepositoryTests
	{
		private IAPIHttpClient _apiHttpClient = Substitute.For<IAPIHttpClient>();
		private ISensorFactory _sensorFactory = Substitute.For<ISensorFactory>();
		private IAPIQueryStringResolver _apiQueryStringResolver = Substitute.For<IAPIQueryStringResolver>();
		private MockHttpMessageHandler _httpMessageHandler = Substitute.ForPartsOf<MockHttpMessageHandler>();

		private IAPIRepository _apiRepository = default!;

		[TestInitialize]
		public void TestInit()
		{
			_apiHttpClient.BaseUri.Returns("https://localhost/v2");
			_apiHttpClient.GetHttpClient().Returns(new HttpClient(_httpMessageHandler));

			_apiRepository = new APIRepository(_apiHttpClient, _apiQueryStringResolver, _sensorFactory);
		}

		[TestMethod]
		public async Task GetData_Generic_ForTypeHttpOK_ReturnsType()
		{
			//	Assemble
			WeatherStation getDataTypeResponse = new()
			{
				Name = "KMINILES3",
				Description = "Weather Station",
				Type = "Davis Vantage Pro 2"
			};

			var httpRequestMessage = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent(JsonSerializer.Serialize(getDataTypeResponse))
			};

			_httpMessageHandler.MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(httpRequestMessage);

			//	Act
			var result = await _apiRepository.GetData<WeatherStation>("stations");

			//	Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(getDataTypeResponse.Name, result.Name);
			Assert.AreEqual(getDataTypeResponse.Description, result.Description);
			Assert.AreEqual(getDataTypeResponse.Type, result.Type);

			_httpMessageHandler.Received().MockSend(Arg.Is<HttpRequestMessage>(r => r.RequestUri!.ToString().Equals("https://localhost/v2/stations?")), Arg.Any<CancellationToken>());
		}

		[TestMethod]
		public async Task GetData_Generic_ForTypeWithParametersHttpOK_ReturnsType()
		{
			//	Assemble
			Dictionary<string, string> parameters = new ()
			{
				{ "t", "12345" },
				{ "api-key", "testKey" }
			};

			WeatherStation getDataTypeResponse = new()
			{
				Name = "KMINILES3",
				Description = "Weather Station",
				Type = "Davis Vantage Pro 2"
			};

			var httpRequestMessage = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent(JsonSerializer.Serialize(getDataTypeResponse))
			};

			_httpMessageHandler.MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(httpRequestMessage);
			_apiQueryStringResolver.Build(Arg.Any<IEnumerable<KeyValuePair<string, string>>>()).Returns(new SortedDictionary<string, string>(parameters));

			//	Act
			var result = await _apiRepository.GetData<WeatherStation>("stations", parameters);

			//	Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(getDataTypeResponse.Name, result.Name);
			Assert.AreEqual(getDataTypeResponse.Description, result.Description);
			Assert.AreEqual(getDataTypeResponse.Type, result.Type);

			_httpMessageHandler.Received().MockSend(Arg.Is<HttpRequestMessage>(r => r.RequestUri!.ToString().Equals("https://localhost/v2/stations?api-key=testKey&t=12345&")), Arg.Any<CancellationToken>());
		}

		[TestMethod]
		public async Task GetData_Generic_ForTypeWithParametersOneCalculateOnlyHttpOK_ReturnsTypeMissingParameter()
		{
			//	Assemble
			Dictionary<string, string> parameters = new()
			{
				{ "t", "12345" },
				{ "api-key", "testKey" },
				{ "station-id", "88769" }
			};

			var calculageOnly = new string[] { "station-id" };

			WeatherStation getDataTypeResponse = new()
			{
				Name = "KMINILES3",
				Description = "Weather Station",
				Type = "Davis Vantage Pro 2"
			};

			var httpRequestMessage = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent(JsonSerializer.Serialize(getDataTypeResponse))
			};

			_httpMessageHandler.MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(httpRequestMessage);
			_apiQueryStringResolver.Build(Arg.Any<IEnumerable<KeyValuePair<string, string>>>()).Returns(new SortedDictionary<string, string>(parameters));

			//	Act
			var result = await _apiRepository.GetData<WeatherStation>($"current/{parameters["station-id"]}", parameters, calculageOnly);

			//	Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(getDataTypeResponse.Name, result.Name);
			Assert.AreEqual(getDataTypeResponse.Description, result.Description);
			Assert.AreEqual(getDataTypeResponse.Type, result.Type);

			_httpMessageHandler.Received().MockSend(Arg.Is<HttpRequestMessage>(r => r.RequestUri!.ToString().Equals("https://localhost/v2/current/88769?api-key=testKey&t=12345&")), Arg.Any<CancellationToken>());
		}


		[TestMethod]
		public async Task GetData_Generic_ForTypeHttpBadRequest_ReturnsType()
		{
			//	Assemble
			WeatherStation getDataTypeResponse = new()
			{
				Name = "KMINILES3",
				Description = "Weather Station",
				Type = "Davis Vantage Pro 2"
			};

			var httpRequestMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
			{
				Content = new StringContent(JsonSerializer.Serialize(getDataTypeResponse))
			};

			_httpMessageHandler.MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()).Returns(httpRequestMessage);

			//	Act
			HttpStatusCode? statusCode = null;
			try
			{
				_ = await _apiRepository.GetData<WeatherStation>("stations");
			}
			catch (HttpRequestException ex)
			{
				statusCode = ex.StatusCode;
			}

			//	Assert
			Assert.IsNotNull(statusCode);
			Assert.AreEqual(HttpStatusCode.BadRequest, statusCode);
		}


		internal class WeatherStation : IResponse
		{
			public string Name { get; set; } = default!;
			public string Description { get; set; } = default!;
			public string Type { get; set; } = default!;
		}

	}
}
