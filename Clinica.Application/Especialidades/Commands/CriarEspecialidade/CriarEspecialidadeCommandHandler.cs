using Clinica.Application.Interfaces;
using Clinica.Domain.Entities;
using MediatR;

namespace Clinica.Application.Especialidades.Commands.CriarEspecialidade
{
    public class CriarEspecialidadeCommandHandler : IRequestHandler<CriarEspecialidadeCommand, Guid>
    {
        private readonly IEspecialidadeRepository _especialidadeRepository;

        public CriarEspecialidadeCommandHandler(IEspecialidadeRepository especialidadeRepository)
        {
            _especialidadeRepository = especialidadeRepository;
        }

        public async Task<Guid> Handle(CriarEspecialidadeCommand request, CancellationToken cancellationToken)
        {
            var especialidade = new Especialidade(
                Guid.NewGuid(),
                request.Nome,
                request.Descricao
                );

            await _especialidadeRepository.AdicionarAsync(especialidade);

            return especialidade.Id;
        }
    }
}
