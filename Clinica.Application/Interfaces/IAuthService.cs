namespace Clinica.Application.Interfaces
{
    public interface IAuthService
    {
        string ComputeHash(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
