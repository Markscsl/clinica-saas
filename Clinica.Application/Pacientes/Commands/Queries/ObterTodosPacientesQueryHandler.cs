using Clinica.Application.Common.DTOs;
using Clinica.Application.Interfaces;
using MediatR;

namespace Clinica.Application.Pacientes.Commands.Queries
{
    public class ObterTodosPacientesQueryHandler : IRequestHandler<ObterTodosPacientesQuery, IEnumerable<PacienteDto>>
    {
        private readonly IPacienteRepository _pacienteRepository;


        public ObterTodosPacientesQueryHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<IEnumerable<PacienteDto>> Handle(ObterTodosPacientesQuery request, CancellationToken cancellationToken)
        {
            var pacientes = await _pacienteRepository.ObterTodosAsync();

            return pacientes.Select(p => new PacienteDto(
                p.Id,
                p.Nome,
                p.CPF
                ));
        }
    }
}
