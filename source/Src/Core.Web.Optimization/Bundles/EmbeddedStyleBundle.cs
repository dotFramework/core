using System.Reflection;

namespace DotFramework.Core.Web.Optimization
{
    public sealed class EmbeddedStyleBundle : EmbeddedBundle
    {
        #region Constructors

        public EmbeddedStyleBundle(Assembly assembly, string virtualPath, string bundleType) 
            : base(assembly, virtualPath, bundleType, new EmbeddedStyleTransformer(assembly))
        {
        }

        #endregion
    }
}
