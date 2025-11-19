using MediatR;

namespace Clinica.Application.Pacientes.Commands.CriarPaciente
{
    public record CriarPacienteCommand(string Nome, string Cpf, string Telefone, string Email, string Senha) : IRequest<Guid>;
}
