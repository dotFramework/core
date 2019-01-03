using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace DotFramework.Core.Web.Optimization
{
    public static class EmbeddedStyles
    {
        const string styleTag = "<link rel=\"stylesheet\" href=\"{0}\">";

        public static HtmlString Render()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var bundle in BundleTable.Bundles.Where(b => b is EmbeddedStyleBundle))
            {
                sb.AppendLine(String.Format(styleTag, bundle.Path.Replace("~", "")));
            }

            sb.AppendLine();

            return new HtmlString(sb.ToString());
        }

        public static HtmlString Render(string bundleType)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var bundle in BundleTable.Bundles.Where(b => b is EmbeddedStyleBundle && (b as EmbeddedBundle).BundleType == bundleType))
            {
                sb.AppendLine(String.Format(styleTag, bundle.Path.Replace("~", "")));
            }

            sb.AppendLine();

            return new HtmlString(sb.ToString());
        }
    }
}
