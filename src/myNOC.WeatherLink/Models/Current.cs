using myNOC.WeatherLink.Sensors;
using System.Text.Json.Serialization;

namespace myNOC.WeatherLink.Models
{
	public class Current : IResponse
	{
		[JsonPropertyName("stationId")]
		public int StationId { get; set; }
		[JsonPropertyName("sensors")]
		public IEnumerable<Sensor>? Sensors { get; set;}
		[JsonPropertyName("generated_at")]
		public int UnixGeneratedAt { get; set; }
		public DateTimeOffset GeneratedAt { get => DateTimeOffset.FromUnixTimeSeconds(UnixGeneratedAt); }
	}
}
