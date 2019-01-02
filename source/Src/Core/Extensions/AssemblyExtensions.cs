using DotFramework.Core;
using System.IO;
using System.Linq;

namespace System.Reflection
{
    public static class AssemblyExtensions
    {
        public static string GetDescription(this Assembly assembly)
        {
            var attribute = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)
                .Select(a => a as AssemblyDescriptionAttribute).FirstOrDefault();

            if (attribute == null)
            {
                return String.Empty;
            }

            return attribute.Description;
        }

        public static string GetApplicationName(this Assembly assembly)
        {
            var attribute = assembly.GetCustomAttributes(typeof(ApplicationNameAttribute), false)
                .Select(a => a as ApplicationNameAttribute).FirstOrDefault();

            if (attribute == null)
            {
                return assembly.GetName().Name;
            }

            return attribute.Name;
        }

        public static string GetManifestResourceString(this Assembly assembly, string resourceName)
        {
            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                {
                    throw new FileNotFoundException("Cannot find mappings file.", resourceName);
                }

                var reader = new StreamReader(resourceStream);
                return reader.ReadToEnd();
            }
        }
    }
}
