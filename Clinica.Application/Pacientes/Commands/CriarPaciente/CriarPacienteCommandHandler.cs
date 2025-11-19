using Clinica.Application.Interfaces;
using Clinica.Domain.Constants;
using Clinica.Domain.Entities;
using MediatR;

namespace Clinica.Application.Pacientes.Commands.CriarPaciente
{
    public class CriarPacienteCommandHandler : IRequestHandler<CriarPacienteCommand, Guid>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthService _authService;

        public CriarPacienteCommandHandler(IPacienteRepository pacienteRepository, IAuthService authService, IUsuarioRepository usuarioRepository)
        {
            _pacienteRepository = pacienteRepository;
            _authService = authService;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Guid> Handle(CriarPacienteCommand request, CancellationToken cancellationToken)
        {

            if (await _usuarioRepository.ObterPorEmailAsync(request.Email) != null)
                throw new Exception("Email já cadastrado.");

            var senhaHash = _authService.ComputeHash(request.Senha);

            var usuario = new Usuario(
                request.Email,
                senhaHash,
                Roles.Paciente
            );

            await _usuarioRepository.AdicionarASync(usuario);

            var paciente = new Paciente(
                usuario.Id,
                request.Nome,
                request.Cpf,
                request.Telefone,
                request.Email
            );

            await _pacienteRepository.AdicionarAsync(paciente);

            return paciente.Id;
        }
    }
}
