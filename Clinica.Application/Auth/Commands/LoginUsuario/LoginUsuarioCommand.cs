using MediatR;

namespace Clinica.Application.Auth.Commands.LoginUsuario
{
    public record LoginUsuarioCommand(string Email, string Senha) : IRequest<string>;
}
