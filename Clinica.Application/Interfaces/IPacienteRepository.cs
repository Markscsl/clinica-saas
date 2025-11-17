using Clinica.Domain.Entities;

namespace Clinica.Application.Interfaces
{
    public interface IPacienteRepository
    {
        Task AdicionarAsync(Paciente paciente);
        Task<Paciente?> ObterPorIdAsync(Guid id);
        Task AtualizarAsync(Paciente paciente); 
    }
}
