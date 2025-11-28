using Clinica.Application.Interfaces;
using MediatR;

namespace Clinica.Application.Auth.Commands.RedefinirSenha
{
    public class RedefinirSenhaCommandHandler : IRequestHandler<RedefinirSenhaCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthService _authService;

        public RedefinirSenhaCommandHandler(IUsuarioRepository usuarioRepository, IAuthService authService)
        {
            _usuarioRepository = usuarioRepository;
            _authService = authService;
        }

        public async Task Handle(RedefinirSenhaCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterPorTokenRecuperacaoAsync(request.token);

            if (usuario == null)
            {
                throw new Exception("Código inválido.");
            }

            if (usuario.PasswordResetExpiry < DateTime.Now)
            {
                throw new Exception("Código expirado.");
            }

            var novoHash = _authService.ComputeHash(request.novaSenha);

            usuario.RedefinirSenha(novoHash);

            await _usuarioRepository.AtualizarAsync(usuario);
        }
    }
}
