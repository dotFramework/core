using Newtonsoft.Json.Linq;

namespace DotFramework.Core.Serialization
{
    public static class DynamicHelper
    {
        public static T GetTypedDefinition<T>(dynamic obj) where T : class
        {
            if (obj != null)
            {
                string json = null;

                if (obj.GetType() == typeof(JObject))
                {
                    json = obj.ToString();
                }
                else
                {
                    json = JsonSerializerHelper.SimpleSerialize(obj);
                }

                return JsonSerializerHelper.Deserialize<T>(json);
            }
            else
            {
                return null;
            }
        }
    }
}
