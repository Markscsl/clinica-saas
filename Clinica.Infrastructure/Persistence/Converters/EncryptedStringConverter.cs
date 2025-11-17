using Clinica.Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Clinica.Infrastructure.Persistence.Converters
{
    public class EncryptedStringConverter : ValueConverter<string, string>
    {
        public EncryptedStringConverter(AesEncryptionService encryptionService)
            : base(
                v => encryptionService.Encrypt(v),
                
                v => encryptionService.Decrypt(v))
        {
        }
    }
}