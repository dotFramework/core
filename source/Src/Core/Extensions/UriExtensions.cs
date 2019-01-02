using System;

namespace DotFramework.Core
{
    public static class UriExtensions
    {
        public static string GetRootUrl(this Uri uri)
        {
            return uri.AbsoluteUri.Replace(uri.AbsolutePath, String.Empty);
        }
    }
}
