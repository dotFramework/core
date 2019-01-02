using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace DotFramework.Core
{
    public static class EnumHelper<T>
    {
        public static string GetDescription(T enumValue, string defaultDesc)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            if (null != fi)
            {
                object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);

                if (attrs != null && attrs.Count() > 0)
                {
                    return ((DescriptionAttribute)attrs.First()).Description;
                }
            }

            return defaultDesc;
        }

        public static string GetDescription(T enumValue)
        {
            return GetDescription(enumValue, string.Empty);
        }

        public static T FromDescription(string description)
        {
            Type t = typeof(T);

            foreach (FieldInfo fi in t.GetFields())
            {
                object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);

                if (attrs != null && attrs.Count() > 0)
                {
                    foreach (DescriptionAttribute attr in attrs)
                    {
                        if (attr.Description.Equals(description))
                        {
                            return (T)fi.GetValue(null);
                        }
                    }
                }
            }
            return default(T);
        }

        public static T[] GetValues()
        {
            Type typeFromHandle = typeof(T);

            if (!typeFromHandle.IsEnum)
            {
                throw new ArgumentException("Type '" + typeFromHandle.Name + "' is not an enum");
            }

            List<T> list = new List<T>();

            IEnumerable<FieldInfo> enumerable = from field in typeFromHandle.GetFields()
                                                where field.IsLiteral
                                                select field;

            foreach (FieldInfo current in enumerable)
            {
                object value = current.GetValue(typeFromHandle);
                list.Add((T)((object)value));
            }

            return list.ToArray();
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static TValue Parse<TValue>(string value)
        {
            return (TValue)Enum.Parse(typeof(T), value);
        }
    }
}
