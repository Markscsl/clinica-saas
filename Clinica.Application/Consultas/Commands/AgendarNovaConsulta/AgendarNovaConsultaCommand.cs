using MediatR;

namespace Clinica.Application.Consultas.Commands.AgendarNovaConsulta
{
    public record AgendarNovaConsultaCommand(Guid PacienteId, Guid MedicoId, DateTime DataHoraInicio) : IRequest<Guid>;
}
