using Newtonsoft.Json;

namespace FurryFeast.Helper
{
	public static class SessionHelper
	{

		public static void SetProductCartSession(this ISession session, string key, object value)
		{
            //JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //};
            session.SetString(key, JsonConvert.SerializeObject(value));
		}

		public static T GetProductCartSession<T>(this ISession session, string key)
		{
            //JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //};
            var value=session.GetString(key);
			return value==null? (T)default :JsonConvert.DeserializeObject<T>(value);
		}

		public static void RemoveProductCartSession(this ISession session, string key)
		{
			session.Remove(key);
		}

		
	}
}
