using Clinica.Application.Common.DTOs;
using MediatR;

namespace Clinica.Application.Medicos.Queries
{
    public record ObterTodosMedicosQuery(Guid? EspecialidadeId) : IRequest<IEnumerable<MedicoDto>>;
}
