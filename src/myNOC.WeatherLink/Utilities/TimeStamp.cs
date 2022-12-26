namespace myNOC.WeatherLink.Utilities
{
	public class TimeStamp : ITimeStamp
	{
		public string UnixTimeInSeconds => DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

		public override string ToString()
		{
			return UnixTimeInSeconds;
		}
	}
}
