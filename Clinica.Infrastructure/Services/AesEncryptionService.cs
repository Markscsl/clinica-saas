using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Clinica.Infrastructure.Services
{
    // A classe que faz o trabalho sujo.
    // Ela é segura pois não sabe de onde vêm as chaves.
    public class AesEncryptionService
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        // Ela recebe as chaves via Injeção de Dependência
        public AesEncryptionService(IConfiguration configuration)
        {
            // Lê as chaves do appsettings.json
            var keyBase64 = configuration["EncryptionSettings:Key"];
            var ivBase64 = configuration["EncryptionSettings:IV"];

            _key = Convert.FromBase64String(keyBase64);
            _iv = Convert.FromBase64String(ivBase64);
        }

        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            using (var aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            var cipherBytes = Convert.FromBase64String(cipherText);
            using (var aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var msDecrypt = new MemoryStream(cipherBytes))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}