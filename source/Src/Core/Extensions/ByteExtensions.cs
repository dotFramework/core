using DotFramework.Core;
using System.Text;

namespace System
{
    public static class ByteExtensions
    {
        public static byte[] ObjectToByteArray(this Object obj)
        {
            if (obj == null)
            {
                return null;
            }
            else
            {
                return JsonSerializerHelper.Serialize(obj).ToByteArray();
            }
        }

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
