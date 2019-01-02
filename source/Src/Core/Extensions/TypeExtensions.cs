using System.Linq;
using System.Reflection;

namespace System
{
    public static class TypeExtensions
    {
        public static PropertyInfo GetProperty(this Type type, String propertyName)
        {
            return type.GetTypeInfo().GetDeclaredProperty(propertyName);
        }

        public static PropertyInfo[] GetProperties(this Type type)
        {
            return type.GetTypeInfo().DeclaredProperties.ToArrayOrDefault();
        }

        public static MethodInfo GetMethod(this Type type, String methodName)
        {
            return type.GetTypeInfo().GetDeclaredMethod(methodName);
        }

        public static Attribute[] GetCustomAttributes(this Type type, bool inherit)
        {
            return type.GetTypeInfo().GetCustomAttributes<Attribute>(inherit).ToArrayOrDefault();
        }

        public static Attribute[] GetCustomAttributes<T>(this Type type) where T : Attribute
        {
            return type.GetTypeInfo().GetCustomAttributes<T>(false).ToArrayOrDefault();
        }

        public static Attribute[] GetCustomAttributes<T>(this Type type, bool inherit) where T : Attribute
        {
            return type.GetTypeInfo().GetCustomAttributes<T>(inherit).ToArrayOrDefault();
        }

        public static bool IsSubclassOf(this Type type, Type parentType)
        {
            return type.GetTypeInfo().IsSubclassOf(parentType);
        }

        public static bool IsAssignableFrom(this Type type, Type parentType)
        {
            return type.GetTypeInfo().IsAssignableFrom(parentType.GetTypeInfo());
        }

        public static bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }

        public static bool IsPrimitive(this Type type)
        {
            return type.GetTypeInfo().IsPrimitive;
        }

        public static Type GetBaseType(this Type type)
        {
            return type.GetTypeInfo().BaseType;
        }

        public static bool IsGenericType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType;
        }

        public static bool IsValueType(this Type type)
        {
            return type.GetTypeInfo().IsValueType;
        }

        public static bool IsVisible(this Type type)
        {
            return type.GetTypeInfo().IsVisible;
        }

        public static Type[] GetGenericArguments(this Type type)
        {
            return type.GetTypeInfo().GenericTypeArguments;
        }

        public static object GetPropertyValue(this Object instance, string propertyValue)
        {
            return instance.GetType().GetTypeInfo().GetDeclaredProperty(propertyValue).GetValue(instance);
        }

        public static string GetAssemblyQualifiedNameWithoutVersion(this Type type)
        {
            var parts = type.AssemblyQualifiedName.Split(',');
            return String.Format("{0}, {1}", parts[0].Trim(), parts[1].Trim());
        }
    }
}
