using System;
using System.Text;

namespace DotFramework.Core
{
    public static class EncryptUtility
    {
        public static string EncryptText(string text)
        {
            Byte[] byteArray = ASCIIEncoding.ASCII.GetBytes(text);
            string encryptedConnectionString = Convert.ToBase64String(byteArray);

            return encryptedConnectionString;
        }

        public static string DecryptText(string text)
        {
            Byte[] byteArray = Convert.FromBase64String(text);
            string decryptedConnectionString = ASCIIEncoding.ASCII.GetString(byteArray);

            return decryptedConnectionString;
        }
    }
}
