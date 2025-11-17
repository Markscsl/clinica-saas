using MediatR;

namespace Clinica.Application.Consultas.Queries
{
    public record ObterConsultaPorIdQuery(Guid ConsultaId) : IRequest<ConsultaResponse?>;
}
