using Clinica.Domain.Entities;

namespace Clinica.Application.Interfaces
{
    public interface IEspecialidadeRepository
    {
        Task AdicionarAsync(Especialidade especialidade);
    }
}
