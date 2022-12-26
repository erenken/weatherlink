using Microsoft.Extensions.Logging;
using myNOC.WeatherLink.API;
using myNOC.WeatherLink.Resolvers;
using myNOC.WeatherLink.Utilities;
using NSubstitute;

namespace myNOC.Tests.WeatherLink.API
{
	[TestClass]
	public class APISignatureResolverTests
	{
		private readonly IAPIContext _apiContext = Substitute.For<IAPIContext>();
		private readonly ILogger<APIQueryStringResolver> _logger = Substitute.For<ILogger<APIQueryStringResolver>>();
		private readonly ITimeStamp _timeStamp = Substitute.For<ITimeStamp>();
		private IAPIQueryStringResolver _apiSignatureResolver = default!;


		[TestInitialize]
		public void TestInit()
		{
			_apiContext.APIKey.Returns("testKey");
			_apiContext.APISecret.Returns("testSecret");
			_apiContext.StationId.Returns("testStationId");

			_timeStamp.UnixTimeInSeconds.Returns("1671769055");

			_apiSignatureResolver = new APIQueryStringResolver(_apiContext, _timeStamp, _logger);
		}

		[TestMethod]
		public void CalculateSignature_NullParmeters_Returns_Signature()
		{
			//	Assemble

			//	Act
			var sortedParmeters = _apiSignatureResolver.Build();
			var apiSignature = sortedParmeters["api-signature"];

			//	Assert
			Assert.IsTrue(apiSignature.Length > 20);
			Assert.AreEqual("7d62c14f49db39abeaac31ffc03572d3bb4ceaf919a823f45408d5fb05e3b836", apiSignature);
			Assert.AreEqual(4, sortedParmeters.Count);
		}
	}
}
