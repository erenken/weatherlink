using myNOC.WeatherLink.Models.Sensors;
using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.Models
{
	public class Current : IResponse
	{
		[JsonPropertyName("station_id")]
		public int StationId { get; set; }
		[JsonPropertyName("sensors")]
		public IEnumerable<Sensor?> Sensors { get; set; } = default!;
		[JsonPropertyName("generated_at")]
		public int UnixGeneratedAt { get; set; }
		public DateTimeOffset GeneratedAt => DateTimeOffset.FromUnixTimeSeconds(UnixGeneratedAt);
	}
}
