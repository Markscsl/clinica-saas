using Clinica.Application.Pacientes.Commands.CriarPaciente;
using FluentValidation;

namespace Clinica.Application.Consultas.Commands.CriarPaciente
{
    public class CriarPacienteCommandValidator : AbstractValidator<CriarPacienteCommand>
    {
        public CriarPacienteCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("O CPF é obrigatório.")
                .Length(11).WithMessage("O CPF deve ter 11 dígitos.")
                .Matches("^[0-9]*$").WithMessage("O CPF deve conter apenas números.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("O e-mail fornecido não é válido.");
        }
    }
}
