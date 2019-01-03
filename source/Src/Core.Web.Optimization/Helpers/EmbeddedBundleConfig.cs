using System;
using System.Linq;
using System.Reflection;
using System.Web.Optimization;

namespace DotFramework.Core.Web.Optimization
{
    public static class EmbeddedBundleConfig
    {
        public static void RegisterEmbeddedBundles(BundleCollection bundles, string virtualPath, string bundleType, Assembly assembly, string resourceFolder = "~/")
        {
            string[] resourceNames = assembly.GetManifestResourceNames();
            string resourcePath = resourceFolder.Replace("~", assembly.GetName().Name).Replace("/", ".").Replace("\\", ".");

            var scriptResources = resourceNames.Where(r => r.ToLower().StartsWith(resourcePath.ToLower()) && r.ToLower().EndsWith(".js"));

            if (scriptResources.Count() != 0)
            {
                string scriptBundleName = String.Format("{0}/{1}", virtualPath, OptimizationConstants.ScriptsBundlePostfix);
                var scriptBundle = new EmbeddedScriptBundle(assembly, scriptBundleName, bundleType);

                foreach (var resource in scriptResources)
                {
                    scriptBundle.ResourceNames.Add(resource);
                }

                bundles.Add(scriptBundle);
            }

            var styleResources = resourceNames.Where(r => r.ToLower().StartsWith(resourcePath.ToLower()) && r.ToLower().EndsWith(".css"));

            if (styleResources.Count() != 0)
            {
                string styleBundleName = String.Format("{0}/{1}", virtualPath, OptimizationConstants.StylesBundlePostfix);
                var styleBundle = new EmbeddedStyleBundle(assembly, styleBundleName, bundleType);

                foreach (var resource in styleResources)
                {
                    styleBundle.ResourceNames.Add(resource);
                }

                bundles.Add(styleBundle);
            }
        }
    }
}
