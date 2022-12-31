using Microsoft.Extensions.Logging;
using myNOC.WeatherLink.API;
using myNOC.WeatherLink.Utilities;
using System.Security.Cryptography;
using System.Text;

namespace myNOC.WeatherLink.Resolvers
{
	public class APIQueryStringResolver : IAPIQueryStringResolver
	{
		private readonly IAPIContext _apiContext;
		private readonly ILogger<APIQueryStringResolver> _logger;
		private readonly ITimeStamp _timeStamp;

		public APIQueryStringResolver(
			IAPIContext apiContext,
			ITimeStamp timeStamp,
			ILogger<APIQueryStringResolver> logger
			)
		{
			_apiContext = apiContext;
			_timeStamp = timeStamp;
			_logger = logger;
		}

		public SortedDictionary<string, string> Build(IEnumerable<KeyValuePair<string, string>>? parameters = null)
		{
			SortedDictionary<string, string> sortedParams = new()
			{
				{ "api-key", _apiContext.APIKey },
				{ "t", _timeStamp.UnixTimeInSeconds }
			};

			if (parameters != null)
			{
				foreach (var param in parameters)
					sortedParams.Add(param.Key, param.Value);
			}

			StringBuilder dataToHashStringBuilder = new();
			foreach (var param in sortedParams)
			{
				dataToHashStringBuilder.Append(param.Key);
				dataToHashStringBuilder.Append(param.Value);
			}

			var dataToHash = dataToHashStringBuilder.ToString();
			_logger.LogDebug($"Data to hash is: {dataToHash}");

			string? apiSignature;
			using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_apiContext.APISecret));
			var signature = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataToHash));
			apiSignature = BitConverter.ToString(signature).Replace("-", "").ToLower();

			_logger.LogDebug($"API Signature: {apiSignature}");
			sortedParams.Add("api-signature", apiSignature);

			return sortedParams;
		}

	}
}
