using MediatR;

namespace Clinica.Application.Auth.Commands.RedefinirSenha
{
    public record RedefinirSenhaCommand(string token, string novaSenha) : IRequest;
}
