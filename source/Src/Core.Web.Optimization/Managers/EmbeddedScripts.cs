using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace DotFramework.Core.Web.Optimization
{
    public static class EmbeddedScripts
    {
        const string scriptTag = "<script type=\"text/javascript\" src=\"{0}\"></script>";

        public static HtmlString Render()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var bundle in BundleTable.Bundles.Where(b => b is EmbeddedScriptBundle))
            {
                sb.AppendLine(String.Format(scriptTag, bundle.Path.Replace("~", "")));
            }

            sb.AppendLine();

            return new HtmlString(sb.ToString());
        }

        public static HtmlString Render(string bundleType)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var bundle in BundleTable.Bundles.Where(b => b is EmbeddedScriptBundle && (b as EmbeddedBundle).BundleType == bundleType))
            {
                sb.AppendLine(String.Format(scriptTag, bundle.Path.Replace("~", "")));
            }

            sb.AppendLine();

            return new HtmlString(sb.ToString());
        }
    }
}
