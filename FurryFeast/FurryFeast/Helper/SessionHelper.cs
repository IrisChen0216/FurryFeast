using Newtonsoft.Json;

namespace FurryFeast.Helper
{
	public static class SessionHelper
	{

		public static void SetProductCartSession(this ISession session, string key, object value)
		{
			
            session.SetString(key, JsonConvert.SerializeObject(value));
		}

		public static T GetProductCartSession<T>(this ISession session, string key)
		{
            
            var value=session.GetString(key);
			return value==null? (T)default :JsonConvert.DeserializeObject<T>(value);
		}

		public static void RemoveProductCartSession(this ISession session, string key)
		{
			session.Remove(key);
		}

		
	}
}
