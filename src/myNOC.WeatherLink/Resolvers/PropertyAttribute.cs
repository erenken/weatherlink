namespace myNOC.WeatherLink.Resolvers
{
	public class PropertyAttribute<T> where T : Attribute
	{
		public string Name { get; set; } = default!;
		public T? Attribute { get; set; } = default;
	}
}
