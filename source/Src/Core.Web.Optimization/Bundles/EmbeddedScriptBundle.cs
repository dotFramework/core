using System.Reflection;

namespace DotFramework.Core.Web.Optimization
{
    public sealed class EmbeddedScriptBundle : EmbeddedBundle
    {
        #region Constructors

        public EmbeddedScriptBundle(Assembly assembly, string virtualPath, string bundleType) 
            : base(assembly, virtualPath, bundleType, new EmbeddedScriptTransformer(assembly))
        {
        }

        #endregion
    }
}
