using System.ComponentModel;
using System.Reflection;

namespace FurryFeast.Extensions
{
	public static class EnumToStringExtension
	{
		public static string GetDescription(this Enum value)
		{
			var fieldInfo = value.GetType().GetField(value.ToString());
			var descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
			return descriptionAttribute != null ? descriptionAttribute.Description : value.ToString();
		}
	}
}
