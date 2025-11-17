using Clinica.Application.Interfaces;
using MediatR;

namespace Clinica.Application.Consultas.Queries
{
    public class ObterConsultaPorIdQueryHandler : IRequestHandler<ObterConsultaPorIdQuery, ConsultaResponse?>
    {
        private readonly IConsultaRepository _consultaRepository;

        public ObterConsultaPorIdQueryHandler(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<ConsultaResponse?> Handle(ObterConsultaPorIdQuery request, CancellationToken cancellationToken)
        {
            return await _consultaRepository.ObterConsultaDtoPorIdAsync(request.ConsultaId);
        }
    }
}
