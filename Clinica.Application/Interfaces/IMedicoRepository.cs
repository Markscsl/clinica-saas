using Clinica.Domain.Entities;

namespace Clinica.Application.Interfaces
{
    public interface IMedicoRepository
    {
        Task AdicionarAsync(Medico medico);
        Task<Medico?> ObterPorIdASync(Guid id);
        Task<IEnumerable<Medico>> ObterTodosAsync(Guid? especialidadeId);
    }
}
