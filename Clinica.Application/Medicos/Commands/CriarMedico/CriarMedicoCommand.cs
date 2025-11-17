using MediatR;

namespace Clinica.Application.Medicos.Commands.CriarMedico
{
    public record CriarMedicoCommand(string Nome, string Crm, Guid EspecialidadeId) : IRequest<Guid>
    {
    }
}
