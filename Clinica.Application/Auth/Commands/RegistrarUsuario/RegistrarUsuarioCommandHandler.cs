using Clinica.Application.Interfaces;
using Clinica.Domain.Entities;
using MediatR;

namespace Clinica.Application.Auth.Commands.RegistrarUsuario
{
    public class RegistrarUsuarioCommandHandler : IRequestHandler<RegistrarUsuarioCommand, Guid>
    {
        private readonly IUsuarioRepository _repository;
        private readonly IAuthService _authService;

        public RegistrarUsuarioCommandHandler(IAuthService authService, IUsuarioRepository repository)
        {
            _authService = authService;
            _repository = repository;
        }

        public async Task<Guid> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var senhaHash = _authService.ComputeHash(request.Senha);

            var usuario = new Usuario(
                request.Email,
                senhaHash,
                request.Role
                );

            await _repository.AdicionarASync(usuario);

            return usuario.Id;
        }
    }
}
