using MediatR;

namespace Clinica.Application.Auth.Commands.AlterarSenha
{
    public class AlterarSenhaCommand : IRequest
    {
        public Guid UsuarioId { get; set; }
        public string SenhaAntiga { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
    }
}
