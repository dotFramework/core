using System;
using System.Text;

namespace DotFramework.Core
{
    public static class ByteExtensions
    {
        public static string ByteArrayToString(this byte[] byteArray)
        {
            if (byteArray == null)
            {
                return null;
            }
            else
            {
                return Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            }
        }
    }
}
