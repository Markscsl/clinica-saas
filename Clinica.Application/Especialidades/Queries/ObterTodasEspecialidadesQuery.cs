using Clinica.Application.Common.DTOs;
using MediatR;

namespace Clinica.Application.Especialidades.Queries
{
    public record ObterTodasEspecialidadesQuery() : IRequest<IEnumerable<EspecialidadeDto>>;
}
