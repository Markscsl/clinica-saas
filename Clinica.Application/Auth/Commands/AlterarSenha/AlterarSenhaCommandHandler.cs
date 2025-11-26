using Clinica.Application.Interfaces;
using MediatR;

namespace Clinica.Application.Auth.Commands.AlterarSenha
{
    public class AlterarSenhaCommandHandler : IRequestHandler<AlterarSenhaCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public AlterarSenhaCommandHandler(IUsuarioRepository usuarioRepository, IAuthService authService, IEmailService emailService)
        {
            _usuarioRepository = usuarioRepository;
            _authService = authService;
            _emailService = emailService;
        }

        public async Task Handle(AlterarSenhaCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(request.UsuarioId);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            if (!_authService.VerifyPassword(request.SenhaAntiga, usuario.SenhaHash))
            {
                throw new Exception("A senha atual está incorreta.");
            }

            var novoHash = _authService.ComputeHash(request.NovaSenha);

            usuario.AlterarSenha(novoHash);

            await _usuarioRepository.AtualizarAsync(usuario);

            await _emailService.EnviarEmailAsync(usuario.Email, "Alteração de Senha - Clínica Médica", $"Olá, sua senha foi alterada com sucesso em {DateTime.Now}. Se não foi você, sua conta pode estar em perigo!");
        }
    }
}
