using MediatR;

namespace Clinica.Application.Auth.Commands.RegistrarUsuario
{
    public record RegistrarUsuarioCommand(string Email, string Senha, string Role) : IRequest<Guid>;
}
