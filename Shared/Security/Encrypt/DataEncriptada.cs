using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Security.Encrypt
{
    public class DataEncriptada : IDataEncriptada
    {
        public static string keyEncript = "PAJHS84z1nlVVkXUxajaewLJcvQP4/qMBr4nu6KWRII=";

        public DataEncriptada() { }

        public string EncriptarData(string data)
        {
            var crt = new Aes256Cipher(keyEncript);
            return crt.Encrypt(data);
        }

        public string DesencriptarData(string data)
        {
            var crt = new Aes256Cipher(keyEncript);
            return crt.Decrypt(data);
        }

        public bool VerifyPassword(string plainPassword, string encryptedPassword)
        {
            try
            {
                var decryptedPassword = DesencriptarData(encryptedPassword);
                return decryptedPassword == plainPassword;
            }
            catch
            {
                return false; 
            }
        }

    }

    public class Aes256Cipher
    {
        private readonly byte[] _key;

        public Aes256Cipher(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new NullReferenceException("The key is empty");
            _key = Convert.FromBase64String(key);
        }

        public string Decrypt(string value)
        {
            var ivAndCipherText = Convert.FromBase64String(value);
            using var aes = Aes.Create();
            var ivLength = aes.BlockSize / 8;
            aes.IV = ivAndCipherText.Take(ivLength).ToArray();
            aes.Key = _key;
            using var cipher = aes.CreateDecryptor();
            var cipherText = ivAndCipherText.Skip(ivLength).ToArray();
            var text = cipher.TransformFinalBlock(cipherText, 0, cipherText.Length);
            return Encoding.UTF8.GetString(text);
        }

        public string Encrypt(string value)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.GenerateIV();
            using var cipher = aes.CreateEncryptor();
            var text = Encoding.UTF8.GetBytes(value);
            var cipherText = cipher.TransformFinalBlock(text, 0, text.Length);
            return Convert.ToBase64String(aes.IV.Concat(cipherText).ToArray());
        }

        public static string GenerateNewKey()
        {
            using var aes = Aes.Create();
            aes.GenerateKey();
            return Convert.ToBase64String(aes.Key);
        }
    }
}
