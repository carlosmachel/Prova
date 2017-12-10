using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Prova.MVC.Services
{
    public class Aes256
    {
        private const string IV = "HR$2pIjHR$2pIj12";

        public static byte[] criptografar(string senha, string entrada)
        {
            string keyMd5 = GerarHashMd5(senha);
            return Encryptdata(Encoding.UTF8.GetBytes(entrada), keyMd5, IV);
        }

        public static byte[] descriptografar(string senha, byte[] criptografado)
        {
            string keyMd5 = GerarHashMd5(senha);
            return decryptdata(criptografado, keyMd5, IV);
        }

        public static string GerarHashMd5(string input)
        {
            var md5Hash = MD5.Create();
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        static public byte[] Encryptdata(byte[] bytearraytoencrypt, string key, string iv)
        {
            using (var dataencrypt = new AesCryptoServiceProvider())
            {
                dataencrypt.BlockSize = 128;
                dataencrypt.KeySize = 128;
                dataencrypt.Key = System.Text.Encoding.UTF8.GetBytes(key);
                dataencrypt.IV = System.Text.Encoding.UTF8.GetBytes(iv);
                dataencrypt.Padding = PaddingMode.PKCS7;
                dataencrypt.Mode = CipherMode.CBC;
                var crypto1 = dataencrypt.CreateEncryptor(dataencrypt.Key, dataencrypt.IV);
                var encrypteddata = crypto1.TransformFinalBlock(bytearraytoencrypt, 0, bytearraytoencrypt.Length);
                crypto1.Dispose();
                return encrypteddata;
            }
        }

        static public byte[] decryptdata(byte[] bytearraytodecrypt, string key, string iv)
        {
            using (var keydecrypt = new AesCryptoServiceProvider())
            {
                keydecrypt.BlockSize = 128;
                keydecrypt.KeySize = 128;
                keydecrypt.Key = System.Text.Encoding.UTF8.GetBytes(key);
                keydecrypt.IV = System.Text.Encoding.UTF8.GetBytes(iv);
                keydecrypt.Padding = PaddingMode.PKCS7;
                keydecrypt.Mode = CipherMode.CBC;
                var crypto1 = keydecrypt.CreateDecryptor(keydecrypt.Key, keydecrypt.IV);

                var returnbytearray = crypto1.TransformFinalBlock(bytearraytodecrypt, 0, bytearraytodecrypt.Length);
                crypto1.Dispose();
                return returnbytearray;
            }
        }


    }
}
