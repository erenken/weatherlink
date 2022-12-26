using myNOC.WeatherLink.Utilities;

namespace myNOC.WeatherLink.Resolvers
{
	public interface IAPIQueryStringResolver
	{
		SortedDictionary<string, string> Build(IEnumerable<KeyValuePair<string, string>>? parameters = null);
	}
}
