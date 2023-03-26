using System.Collections.Concurrent;
using System.Reflection;

namespace myNOC.WeatherLink.Resolvers
{
	public class ObjectTypeInfoResolver : IObjectTypeInfoResolver
	{
		private static ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> _typeProperties = new();
		private static ConcurrentDictionary<Type, IEnumerable<PropertyAttribute<Attribute>>?> _typeAttributes = new();

		private Dictionary<string, PropertyInfo> Initialize(Type resolve)
		{
			if (!_typeProperties.TryGetValue(resolve, out var typeProperties))
			{
				var properties = resolve.GetProperties();

				typeProperties = properties.ToDictionary(k => k.Name, v => v);
				_typeProperties.TryAdd(resolve, typeProperties);
			}

			return typeProperties;
		}

		public object? GetPropertyValue(object instance, string name)
		{
			throw new NotImplementedException();
		}

		public void SetPropertyValue(object instance, string name, object value)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<PropertyAttribute<T>>? PropertyAttributes<T>(object instance) where T : Attribute
		{
			if (!_typeAttributes.TryGetValue(instance.GetType(), out var attributes))
			{
				var resolved = Initialize(instance.GetType());
				var propertyNamesAndAttributes = resolved.Select(x => x).ToDictionary(k => k.Key, v => v.Value.GetCustomAttributes(false).ToList());

				attributes = propertyNamesAndAttributes.SelectMany(pn => pn.Value, (pn, at) => new PropertyAttribute<Attribute> { Attribute = at as Attribute, Name = pn.Key });

				_typeAttributes.TryAdd(instance.GetType(), attributes);
			}

			return attributes?.Where(x => x.Attribute is T).Select(x => new PropertyAttribute<T> { Attribute = x.Attribute as T, Name = x.Name });
		}
	}
}
