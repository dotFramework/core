using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DotFramework.Core.Serialization
{
    public static class JsonSerializerHelper
    {
        public static string Serialize(object obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects, TypeNameHandling = TypeNameHandling.Objects });
            return json;
        }

        public static string SimpleSerialize(object obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return json;
        }

        public static string SimpleSerialize2(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public static T Deserialize<T>(string str)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(str, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects, TypeNameHandling = TypeNameHandling.Objects });
            }
            catch (Exception ex)
            {
                throw new DeserializationException(str, ex);
            }
        }

        public static object Deserialize(Type type, string str)
        {
            try
            {
                return JsonConvert.DeserializeObject(str, type, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects, TypeNameHandling = TypeNameHandling.Objects });
            }
            catch (Exception ex)
            {
                throw new DeserializationException(str, ex);
            }
        }

        public static object SimpleDeserialize(string str)
        {
            try
            {
                return JsonConvert.DeserializeObject(str);
            }
            catch (Exception ex)
            {
                throw new DeserializationException(str, ex);
            }
        }

        public static object SimpleDeserialize(Type type, string str)
        {
            try
            {
                return JsonConvert.DeserializeObject(str, type);
            }
            catch (Exception ex)
            {
                throw new DeserializationException(str, ex);
            }
        }

        public static string SimpleSerializeNotNullObj(object obj)
        {
            string json = string.Empty;
            try
            {
                if (obj != null)
                    json = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                return json;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void PopulateObject(string str, object obj)
        {
            JsonConvert.PopulateObject(str, obj, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects, TypeNameHandling = TypeNameHandling.Objects });
        }

        public static string ParseJsonToXml(string json)
        {
            try
            {
                System.Text.StringBuilder result = new System.Text.StringBuilder();
                if (json.StartsWith("["))
                {
                    List<Dictionary<string, object>> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
                    foreach (Dictionary<string, object> dictionary in list)
                    {
                        result.Append(ParseJsonToXml(Newtonsoft.Json.JsonConvert.SerializeObject(dictionary)));
                    }
                }
                else
                {
                    Dictionary<string, object> dictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    if (dictionary == null)
                        return "";
                    foreach (string key in dictionary.Keys)
                    {
                        result.AppendFormat("<{0}>{1}</{0}>", key, ParseJsonToXml((dictionary[key] ?? "").ToString()));
                    }
                }
                return result.ToString();
            }
            catch
            {
                return json;
            }
        }
        public static XDocument ParseJsonToXDocument(string json, string rootElementName = "RootElement")
        {
            return XDocument.Parse(string.Format("<{1}>{0}</{1}>", ParseJsonToXml(json), rootElementName));
        }
    }
}
