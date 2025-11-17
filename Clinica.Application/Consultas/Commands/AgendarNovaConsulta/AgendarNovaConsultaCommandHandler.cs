using Clinica.Application.Interfaces;
using Clinica.Domain.Entities;
using MediatR;

namespace Clinica.Application.Consultas.Commands.AgendarNovaConsulta
{
    public class AgendarNovaConsultaCommandHandler : IRequestHandler<AgendarNovaConsultaCommand, Guid>
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicoRepository _medicoRepository;

        public AgendarNovaConsultaCommandHandler(IConsultaRepository consultaRepository, IPacienteRepository pacienteRepository, IMedicoRepository medicoRepository)
        {
            _consultaRepository = consultaRepository;
            _pacienteRepository = pacienteRepository;
            _medicoRepository = medicoRepository;
        }

        public async Task<Guid> Handle(AgendarNovaConsultaCommand request, CancellationToken cancellationToken)
        {

            if (await _pacienteRepository.ObterPorIdAsync(request.PacienteId) == null) {

                throw new Exception("Paciente não encontrado!");

            }

            if (await _medicoRepository.ObterPorIdASync(request.MedicoId) == null)
            {

                throw new Exception("Médico não encontrado!");

            }

            var temConflito = await _consultaRepository.ExisteConflitoDeHorarioAsync(request.MedicoId, request.DataHoraInicio);

            if (temConflito)
            {
                throw new Exception("Horário já ocupado ou em conflito.");
            }

            var consulta = new Consulta(
                Guid.NewGuid(),
                request.PacienteId,
                request.MedicoId,
                request.DataHoraInicio
                );

            await _consultaRepository.AdicionarAsync(consulta);

            return consulta.Id;
        }
    }
}
