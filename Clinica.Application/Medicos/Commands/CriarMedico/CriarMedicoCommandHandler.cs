using Clinica.Application.Interfaces;
using Clinica.Domain.Entities;
using MediatR;

namespace Clinica.Application.Medicos.Commands.CriarMedico
{
    public class CriarMedicoCommandHandler : IRequestHandler<CriarMedicoCommand, Guid>
    {
        private readonly IMedicoRepository _medicoRepository;

        public CriarMedicoCommandHandler(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<Guid> Handle(CriarMedicoCommand request, CancellationToken cancellationToken)
        {
            var medico = new Medico(
                Guid.NewGuid(),
                request.Nome,
                request.Crm,
                request.EspecialidadeId
                );

            await _medicoRepository.AdicionarAsync(medico);

            return medico.Id;
        }
    }
}
