using Clinica.Application.Consultas.Queries;
using Clinica.Domain.Entities;

namespace Clinica.Application.Interfaces
{
    public interface IConsultaRepository
    {
        Task AdicionarAsync(Consulta consulta);
        Task<Consulta?> ObterPorIdAsync(Guid id);
        Task AtualizarAsync(Consulta consulta);
        Task<bool> ExisteConflitoDeHorarioAsync(Guid medicoId, DateTime dataHoraInicio);
        Task<ConsultaResponse?> ObterConsultaDtoPorIdAsync(Guid consultaId);
        Task<IEnumerable<Consulta>> ObterPorUsuarioAsync(Guid usuarioId, string role);
    }
}
