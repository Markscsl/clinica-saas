using MediatR;

namespace Clinica.Application.Auth.Commands.SolicitarTrocaSenha
{
    public record SolicitarTrocaSenhaCommand(string Email) : IRequest;    
}
