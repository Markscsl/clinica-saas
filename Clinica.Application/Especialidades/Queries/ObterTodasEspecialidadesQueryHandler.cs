using Clinica.Application.Common.DTOs;
using Clinica.Application.Interfaces;
using MediatR;

namespace Clinica.Application.Especialidades.Queries
{
    public class ObterTodasEspecialidadesQueryHandler : IRequestHandler<ObterTodasEspecialidadesQuery, IEnumerable<EspecialidadeDto>
    {
        private readonly IEspecialidadeRepository _especialidadeRepository;

        public ObterTodasEspecialidadesQueryHandler(IEspecialidadeRepository especialidadeRepository)
        {
            _especialidadeRepository = especialidadeRepository;
        }

        public async Task<IEnumerable<EspecialidadeDto>> Handle(ObterTodasEspecialidadesQueryHandler request, CancellationToken cancellationToken)
        {
            var lista = await _especialidadeRepository.ObterTodasAsync();

            return lista.Select(e => new EspecialidadeDto(e.Id, e.Nome));
        }
    }
}
