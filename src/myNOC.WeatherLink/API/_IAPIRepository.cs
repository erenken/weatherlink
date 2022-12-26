namespace myNOC.WeatherLink.API
{
	public interface IAPIRepository
	{
		Task<T?> GetData<T>(string endPoint, IEnumerable<KeyValuePair<string, string>>? parameters = null) where T : IResponse;
	}
}
