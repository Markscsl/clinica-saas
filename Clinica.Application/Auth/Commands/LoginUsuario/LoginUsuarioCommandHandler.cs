using Clinica.Application.Auth.Commands.LoginUsuario;
using Clinica.Application.Interfaces;
using MediatR;

namespace Clinica.Application.Auth.Commands
{
    public class LoginUsuarioCommandHandler : IRequestHandler<LoginUsuarioCommand, string>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public LoginUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IAuthService authService, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _authService = authService;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(request.Email);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            var senhaValida = _authService.VerifyPassword(request.Senha, usuario.SenhaHash);

            if (!senhaValida)
            {
                throw new Exception("Senha incorreta.");
            }

            var token = _tokenService.GerarToken(usuario);

            return token;
        }
    }
}
