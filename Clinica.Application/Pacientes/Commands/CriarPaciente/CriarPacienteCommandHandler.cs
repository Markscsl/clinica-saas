using Clinica.Application.Interfaces;
using Clinica.Domain.Entities;
using MediatR;

namespace Clinica.Application.Pacientes.Commands.CriarPaciente
{
    public class CriarPacienteCommandHandler : IRequestHandler<CriarPacienteCommand, Guid>
    {
        private readonly IPacienteRepository _pacienteRepository;

        public CriarPacienteCommandHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<Guid> Handle(CriarPacienteCommand request, CancellationToken cancellationToken)
        {
            var paciente = new Paciente(
                Guid.NewGuid(),
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
