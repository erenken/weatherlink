using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.Responses
{
	public class StationsResponse : IResponse
	{
		[JsonPropertyName("stations")]
		public IEnumerable<Station>? Stations { get; set; }

		[JsonPropertyName("generated_at")]
		public int UnixGeneratedAt { get; set; }
		public DateTimeOffset GeneratedAt => DateTimeOffset.FromUnixTimeSeconds(UnixGeneratedAt);
	}
}

