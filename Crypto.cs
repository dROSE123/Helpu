using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace INFO2017
{
    class Crypto
    {
        public static string IV = "a25azhuj37azv6da";
        public static string KEY = "azk2oab820afzb54a4zcs25gc5z66ash";

        public static string Encrypt(string decrypt)
        {
            byte[] encbyte = ASCIIEncoding.ASCII.GetBytes(decrypt);
            AesCryptoServiceProvider encdec = new AesCryptoServiceProvider();
            encdec.BlockSize = 128;
            encdec.KeySize = 256;

            encdec.Key = ASCIIEncoding.ASCII.GetBytes(KEY);
            encdec.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            encdec.Padding = PaddingMode.PKCS7;
            encdec.Mode = CipherMode.CBC;

            ICryptoTransform icrypt = encdec.CreateEncryptor(encdec.Key, encdec.IV);

            byte[] enc = icrypt.TransformFinalBlock(encbyte, 0, encbyte.Length);
            icrypt.Dispose();

            return Convert.ToBase64String(enc);

        }

        public static string Decrypt(string encrypt)
        {
            byte[] decbyte = Convert.FromBase64String(encrypt);
            AesCryptoServiceProvider encdec = new AesCryptoServiceProvider();
            encdec.BlockSize = 128;
            encdec.KeySize = 256;

            encdec.Key = ASCIIEncoding.ASCII.GetBytes(KEY);
            encdec.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            encdec.Padding = PaddingMode.PKCS7;
            encdec.Mode = CipherMode.CBC;

            ICryptoTransform icrypt = encdec.CreateDecryptor(encdec.Key, encdec.IV);

            byte[] dec = icrypt.TransformFinalBlock(decbyte, 0, decbyte.Length);
            icrypt.Dispose();

            return ASCIIEncoding.ASCII.GetString(dec);
        }

    }
}
