using System.Text.Json.Serialization;

namespace myNOC.WeatherLink
{
	public class Stations : IResponse
	{
		[JsonPropertyName("stations")]
		public IEnumerable<Station>? Station { get; set; }
	}
}
