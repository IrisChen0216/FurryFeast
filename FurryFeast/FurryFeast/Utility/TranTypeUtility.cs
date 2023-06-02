using System.Reflection;

namespace FurryFeast.Utility
{
	public class TranTypeUtility
	{
		public static T DictionaryToObject<T>(IDictionary<string, string> dict) where T : new()
		{
			var t = new T();
			PropertyInfo[] properties = t.GetType().GetProperties();

			foreach (PropertyInfo property in properties)
			{
				try
				{
					if (dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
					{
						KeyValuePair<string, string> item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

						Type tPropertyType = t.GetType().GetProperty(property.Name).PropertyType;

						Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;

						object newA = Convert.ChangeType(item.Value, newT);
						t.GetType().GetProperty(property.Name).SetValue(t, newA, null);
					}
				}
				catch { }

			}
			return t;
		}
		public static List<KeyValuePair<string, string>> ModelToKeyValuePairList<T>(T model)
		{
			List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

			try
			{
				Type t = model.GetType();
				foreach (var p in t.GetProperties())
				{
					string name = p.Name;
					object value = p.GetValue(model, null);
					if (value != null)
					{
						result.Add(new KeyValuePair<string, string>(name, value.ToString()));
					}
				}
			}
			catch (Exception ex)
			{
			}

			return result;
		}
	}
}
