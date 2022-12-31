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
			Assert.AreEqual("068ad40427fe1cd98bc1f8ec5404a06db132ec279d7eb6a801887ef320d32a5d", apiSignature);
			Assert.AreEqual(3, sortedParmeters.Count);
		}
	}
}
