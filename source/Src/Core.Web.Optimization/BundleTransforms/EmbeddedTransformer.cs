using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Optimization;

namespace DotFramework.Core.Web.Optimization
{
    public abstract class EmbeddedTransformer : IBundleTransform
    {
        #region Abstract Properties

        protected abstract string ContentType { get; }

        #endregion

        #region Virtual Methods

        protected virtual string Minify(string source)
        {
            return source;
        }

        #endregion

        #region Properties

        public Assembly Assembly { get; set; }

        #endregion

        #region Constructors

        public EmbeddedTransformer(Assembly assembly)
        {
            this.Assembly = assembly;
        }

        #endregion

        #region IBundleTransform

        public void Process(BundleContext context, BundleResponse response)
        {
            EmbeddedScriptBundle currentBundle = context.BundleCollection.FirstOrDefault(b => b.Path == context.BundleVirtualPath) as EmbeddedScriptBundle;

            if (currentBundle == null)
            {
                throw new TargetException("The bundle is not supported by this transformer.");
            }

            string content = String.Empty;

            foreach (var resource in currentBundle.ResourceNames)
            {
                content += Assembly.GetManifestResourceString(resource);
            }

            response.Content = Minify(content);
            response.ContentType = ContentType;
            response.Cacheability = HttpCacheability.Public;
        }

        #endregion
    }
}
