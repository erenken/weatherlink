using System.Text.Json.Nodes;

namespace myNOC.WeatherLink.API
{
	public interface IAPIRepository
	{
		Task<T?> GetData<T>(string endPoint, IEnumerable<KeyValuePair<string, string>>? parameters = null, IEnumerable<string>? calculateOnly = null) where T : IResponse;
		Task<JsonNode?> GetData(string endPoint, IEnumerable<KeyValuePair<string, string>>? parameters = null, IEnumerable<string> ? calculateOnly = null);
	}
}
