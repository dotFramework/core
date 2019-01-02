using System.Globalization;

namespace System
{
    public static class NumericTypesExtensions
    {
        public static string ToStringInvariant(this double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToStringInvariant(this int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToStringInvariant(this uint value)
        {
            return ((int)value).ToStringInvariant();
        }
    }
}
