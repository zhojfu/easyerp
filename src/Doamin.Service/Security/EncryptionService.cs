namespace Doamin.Service.Security
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using EasyErp.Core.Configuration.Settings;

    public class EncryptionService : IEncryptionService
    {
        private readonly SecuritySettings _securitySettings = new SecuritySettings();

        public virtual string CreateSaltKey(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        public virtual string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1")
        {
            if (string.IsNullOrEmpty(passwordFormat))
            {
                passwordFormat = "SHA1";
            }
            var saltAndPassword = string.Concat(password, saltkey);

            var algorithm = HashAlgorithm.Create(passwordFormat);
            if (algorithm == null)
            {
                throw new ArgumentException("Unrecognized hash name");
            }

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        public virtual string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return plainText;
            }

            if (string.IsNullOrEmpty(encryptionPrivateKey))
            {
                encryptionPrivateKey = _securitySettings.EncryptionKey;
            }

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            var encryptedBinary = EncryptTextToMemory(plainText, tDESalg.Key, tDESalg.IV);
            return Convert.ToBase64String(encryptedBinary);
        }

        public virtual string DecryptText(string cipherText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return cipherText;
            }

            if (string.IsNullOrEmpty(encryptionPrivateKey))
            {
                encryptionPrivateKey = _securitySettings.EncryptionKey;
            }

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            var buffer = Convert.FromBase64String(cipherText);
            return DecryptTextFromMemory(buffer, tDESalg.Key, tDESalg.IV);
        }

        #region Utilities

        private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (
                    var cs = new CryptoStream(
                        ms,
                        new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv),
                        CryptoStreamMode.Write))
                {
                    var toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (
                    var cs = new CryptoStream(
                        ms,
                        new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv),
                        CryptoStreamMode.Read))
                {
                    var sr = new StreamReader(cs, new UnicodeEncoding());
                    return sr.ReadLine();
                }
            }
        }

        #endregion Utilities
    }
}