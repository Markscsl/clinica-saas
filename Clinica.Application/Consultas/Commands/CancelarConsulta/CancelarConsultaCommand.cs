using MediatR;

namespace Clinica.Application.Consultas.Commands.CancelarConsulta
{
    public record CancelarConsultaCommand(Guid ConsultaId) : IRequest;
}
