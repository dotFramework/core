using Microsoft.Ajax.Utilities;
using System.Reflection;
using System.Web;

namespace DotFramework.Core.Web.Optimization
{
    public sealed class EmbeddedStyleTransformer : EmbeddedTransformer
    {
        #region Constructors

        public EmbeddedStyleTransformer(Assembly assembly)
            : base(assembly)
        {
        }

        #endregion

        #region Overrided Properties

        protected override string ContentType
        {
            get
            {
                return "text/css";
            }
        }

        #endregion

        #region Overrided Methods

        protected override string Minify(string source)
        {
            if (!HttpContext.Current.IsDebuggingEnabled)
            {
                return new Minifier().MinifyStyleSheet(source);
            }
            else
            {
                return source;
            }
        }

        #endregion
    }
}
