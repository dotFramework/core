namespace System.Reflection
{
    public static class PropertyInfoExtensions
    {
        public static MethodInfo GetGetMethod(this PropertyInfo property)
        {
            return property.GetMethod;
        }

        public static MethodInfo GetSetMethod(this PropertyInfo property)
        {
            return property.SetMethod;
        }
    }
}
