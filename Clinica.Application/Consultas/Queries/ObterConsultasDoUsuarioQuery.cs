using MediatR;

namespace Clinica.Application.Consultas.Queries
{
    public record ConsultaDto(
        Guid Id,
        DateTime Data,
        string NomeMedico,
        string NomeEspecialidade,
        string NomePaciente,
        string Status
        );

    public record ObterConsultasDoUsuarioQuery(Guid UsuarioId, string Role) : IRequest<IEnumerable<ConsultaDto>>;

}
