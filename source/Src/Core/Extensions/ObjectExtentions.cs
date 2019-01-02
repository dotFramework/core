using DotFramework.Core;
using Newtonsoft.Json.Linq;

namespace System
{
    public static class ObjectExtentions
    {
        public static Object ToObject(this byte[] arrBytes)
        {
            if (arrBytes == null)
            {
                return null;
            }
            else
            {
                try
                {
                    string json = arrBytes.ByteArrayToString();
                    JObject obj = JObject.Parse(json);

                    string typeName = String.Empty;

                    if (obj["$type"] != null)
                    {
                        typeName = obj["$type"].Value<String>();
                    }
                    else if (obj["xType"] != null)
                    {
                        typeName = obj["xType"].Value<String>();
                    }

                    if (!String.IsNullOrEmpty(typeName))
                    {
                        Type type = Type.GetType(typeName);
                        return JsonSerializerHelper.SimpleDeserialize(type, json);
                    }
                    else
                    {
                        throw new Exception("Invalid Type");
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public static T ToObject<T>(this byte[] arrBytes) where T : class
        {
            return arrBytes.ToObject() as T;
        }
    }
}
