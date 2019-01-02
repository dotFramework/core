using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace DotFramework.Core.Serialization
{
    public static class ObjectSerializationExtensions
    {
        public static byte[] ToSerializedByteArray(this Object obj)
        {
            if (obj == null)
            {
                return null;
            }
            else
            {
                return JsonSerializerHelper.Serialize(obj).ToByteArray();
            }
        }

        public static string ByteArrayToString(this byte[] byteArray)
        {
            if (byteArray == null)
            {
                return null;
            }
            else
            {
                return Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            }
        }

        public static Object FromSerializedByteArrayToObject(this byte[] arrBytes)
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

        public static T FromSerializedByteArrayToObject<T>(this byte[] arrBytes) where T : class
        {
            return arrBytes.FromSerializedByteArrayToObject() as T;
        }
    }
}
