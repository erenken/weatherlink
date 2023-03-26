using myNOC.WeatherLink.Responses;

namespace myNOC.WeatherLink.Processors
{
	public interface IHighLowProcessor
	{
		WeatherDataResponse CalculateHighLow(WeatherDataResponse response);
	}
}
