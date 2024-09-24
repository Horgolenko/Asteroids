using System;
using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    public static class StringUtil
    {
        private static byte[] key = {6, 9, 8, 7, 3, 5, 9, 2, 4, 6, 6, 2, 2, 2, 2, 5, 8, 6, 9, 6, 3, 7, 2, 8, 6, 4, 4, 4, 3, 2, 7, 2};
        private static byte[] iv = {5, 6, 4, 9, 2, 9, 5, 8, 3, 9, 3, 7, 9, 3, 2, 7};

        public static string Crypt(this string text)
        {
            SymmetricAlgorithm algorithm = Aes.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public static string Decrypt(this string text)
        {
            SymmetricAlgorithm algorithm = Aes.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }
    }
}
