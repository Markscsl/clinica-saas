using Clinica.Application.Common.DTOs;
using MediatR;

namespace Clinica.Application.Pacientes.Commands.Queries
{
    public record ObterTodosPacientesQuery() : IRequest<IEnumerable<PacienteDto>>;
}
