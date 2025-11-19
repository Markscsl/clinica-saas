using Clinica.Application.Common.DTOs;
using Clinica.Application.Interfaces;
using MediatR;

namespace Clinica.Application.Medicos.Queries
{
    public class ObterTodosMedicosQueryHandler : IRequestHandler<ObterTodosMedicosQuery, IEnumerable<MedicoDto>>
    {
        private readonly IMedicoRepository _medicoRepository;

        public ObterTodosMedicosQueryHandler(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<IEnumerable<MedicoDto>> Handle(ObterTodosMedicosQuery request, CancellationToken cancellationToken)
        {
            var medicos = await _medicoRepository.ObterTodosAsync();

            return medicos.Select(m => new MedicoDto
            (
                m.Id,
                m.Nome,
                m.Crm,
                m.Especialidade?.Nome ?? "Sem especialidade"
            ));
        }
    }
}
