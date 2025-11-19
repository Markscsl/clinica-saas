using Clinica.Application.Interfaces;

namespace Clinica.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        public string ComputeHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
