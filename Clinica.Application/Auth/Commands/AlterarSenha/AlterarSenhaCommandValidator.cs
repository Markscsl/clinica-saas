using FluentValidation;

namespace Clinica.Application.Auth.Commands.AlterarSenha
{
    public class AlterarSenhaCommandValidator : AbstractValidator<AlterarSenhaCommand>
    {
        public AlterarSenhaCommandValidator()
        {
            RuleFor(x => x.SenhaAntiga).NotEmpty();

            RuleFor(x => x.NovaSenha)
                .MinimumLength(6)
                .Matches(@"[A-Z]")
                .Matches(@"[0-9]")
                .Matches(@"[!@#$%^&*(),.?"":{}|<>]");
        }
    }
}
