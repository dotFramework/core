using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Optimization;

namespace DotFramework.Core.Web.Optimization
{
    public class EmbeddedBundle : Bundle
    {
        #region Constructors

        protected EmbeddedBundle(Assembly assembly, string virtualPath, string bundleType, EmbeddedTransformer transform) 
            : base(virtualPath, transform)
        {
            this.BundleType = bundleType;
        }

        protected EmbeddedBundle()
        {
        }

        #endregion

        #region Properties

        private List<String> _ResourceNames;
        public List<String> ResourceNames
        {
            get
            {
                if (_ResourceNames == null)
                {
                    _ResourceNames = new List<String>();
                }

                return _ResourceNames;
            }
            set
            {
                _ResourceNames = value;
            }
        }

        public string BundleType { get; set; }

        #endregion
    }
}
