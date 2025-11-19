using Clinica.Domain.Entities;

namespace Clinica.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AdicionarASync(Usuario usuario);
        Task<Usuario?> ObterPorEmailAsync(string email);
    }
}
