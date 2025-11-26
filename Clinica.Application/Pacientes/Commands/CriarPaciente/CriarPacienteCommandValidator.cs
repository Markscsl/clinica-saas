using FluentValidation;

namespace Clinica.Application.Pacientes.Commands.CriarPaciente
{
    public class CriarPacienteCommandValidator : AbstractValidator<CriarPacienteCommand>
    {
        public CriarPacienteCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("O CPF é obrigatório")
                .Matches(@"^\d{11}$").WithMessage("O CPF deve conter apenas os 11 números (sem pontos ou traços).");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O formato do e-mail é inválido.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.")
                .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Matches(@"[0-9]").WithMessage("A senha deve conter pelo menos um número.")
                .Matches(@"[!@#$%^&*(),.?"":{}|<>]").WithMessage("A senha deve conter pelo menos um caractere especial.");
        }
    }
}
