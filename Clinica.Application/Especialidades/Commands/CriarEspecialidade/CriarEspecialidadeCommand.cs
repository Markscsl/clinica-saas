using MediatR;

namespace Clinica.Application.Especialidades.Commands.CriarEspecialidade
{
    public record CriarEspecialidadeCommand(string Nome, string Descricao) : IRequest<Guid>;
}
