using Microsoft.Ajax.Utilities;
using System.Reflection;
using System.Web;

namespace DotFramework.Core.Web.Optimization
{
    public sealed class EmbeddedScriptTransformer : EmbeddedTransformer
    {
        #region Constructors

        public EmbeddedScriptTransformer(Assembly assembly)
            : base(assembly)
        {
        }

        #endregion

        #region Overrided Properties

        protected override string ContentType
        {
            get
            {
                return "text/javascript";
            }
        }

        #endregion

        #region Overrided Methods

        protected override string Minify(string source)
        {
            if (!HttpContext.Current.IsDebuggingEnabled)
            {
                return new Minifier().MinifyJavaScript(source);
            }
            else
            {
                return source;
            }
        }

        #endregion
    }
}
