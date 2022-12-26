namespace myNOC.WeatherLink.API
{
	public class APIContext : IAPIContext
	{
		public string APIKey { get; set; } = default!;
		public string APISecret { get; set; } = default!;
		public string? StationId { get; set; }
	}
}
