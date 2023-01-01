using System.Text.Json.Nodes;
using myNOC.WeatherLink.Responses;

namespace myNOC.WeatherLink.API
{
	public interface IAPIRepository
	{
		Task<T?> GetData<T>(string endPoint, IEnumerable<KeyValuePair<string, string>>? parameters = null, IEnumerable<string>? excludeFromUrl = null) where T : IResponse;
	}
}
