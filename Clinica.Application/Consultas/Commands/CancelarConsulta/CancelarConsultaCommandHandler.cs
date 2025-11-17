using Clinica.Application.Interfaces;
using MediatR;

namespace Clinica.Application.Consultas.Commands.CancelarConsulta
{
    public class CancelarConsultaCommandHandler : IRequestHandler<CancelarConsultaCommand>
    {
        private readonly IConsultaRepository _consultaRepository;

        public CancelarConsultaCommandHandler(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task Handle(CancelarConsultaCommand request, CancellationToken cancellationToken)
        {
            var consulta = await _consultaRepository.ObterPorIdAsync(request.ConsultaId);

            if(consulta == null)
            {
                throw new Exception("Consulta não encontrada.");
            }

            consulta.Cancelar();

            await _consultaRepository.AtualizarAsync(consulta); 
        }
    }
}
