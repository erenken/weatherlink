namespace myNOC.WeatherLink.Resolvers
{
	public interface IObjectTypeInfoResolver
	{
		object? GetPropertyValue(object instance, string name);
		void SetPropertyValue(object instance, string name, object value);
		IEnumerable<PropertyAttribute<T>?>? PropertyAttributes<T>(object instance) where T : Attribute;
	}
}
