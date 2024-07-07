using System.Linq;
using System.Reflection;

namespace NotkaAPI.Helpers
{
	public static class PropertyUtil
	{
		/// <summary>
		/// Property "copier"
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <param name="targetObject"></param>
		/// <param name="sourceObject"></param>
		/// <returns></returns>
		public static T CopyProperties<T, T2>(this T targetObject, T2 sourceObject)
		{
			foreach (var property in typeof(T).GetProperties().Where(p => p.CanWrite))
			{
				Func<PropertyInfo, bool> CheckIfPropertyExistsInSource =
					prop => string.Equals(property.Name, prop.Name, StringComparison.InvariantCultureIgnoreCase)
					&& prop.PropertyType.Equals(property.PropertyType);

				if (sourceObject.GetType().GetProperties().Any(CheckIfPropertyExistsInSource))
				{
					property.SetValue(targetObject, sourceObject.GetPropertyValue(property.Name), null);
				}
			}
			return targetObject;
		}
		private static object GetPropertyValue<T>(this T source, string propertyName)
		{
			return source?.GetType().GetProperty(propertyName).GetValue(source, null);
		}
	}
}
