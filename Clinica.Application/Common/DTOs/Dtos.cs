namespace Clinica.Application.Common.DTOs
{
    public record EspecialidadeDto(Guid Id, string Nome);

    public record MedicoDto(Guid Id, string Nome, string Crm, string NomeEspecialidade);

    public record PacienteDto(Guid Id, string Nome, string Cpf);
}
