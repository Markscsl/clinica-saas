using Clinica.Application.Consultas.Queries;
using Clinica.Application.Interfaces;
using MediatR;

namespace Clinica.Application.Consultas.Queries
{
    public class ObterConsultasDoUsuarioQueryHandler : IRequestHandler<ObterConsultasDoUsuarioQuery, IEnumerable<ConsultaDto>>
    {
        private readonly IConsultaRepository _repository;

        public ObterConsultasDoUsuarioQueryHandler(IConsultaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ConsultaDto>> Handle(ObterConsultasDoUsuarioQuery request, CancellationToken cancellationToken)
        {
            var consultas = await _repository.ObterPorUsuarioAsync(request.UsuarioId, request.Role);

            return consultas.Select(c => new ConsultaDto(
                c.Id,
                c.DataHoraInicio,
                c.Medico.Nome,
                c.Medico.Especialidade?.Nome ?? "Geral",
                c.Paciente.Nome,
                c.Status.ToString()
            ));
        }
    }
}