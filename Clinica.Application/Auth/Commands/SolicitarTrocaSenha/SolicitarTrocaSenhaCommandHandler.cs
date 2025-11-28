using Clinica.Application.Interfaces;
using MediatR;

namespace Clinica.Application.Auth.Commands.SolicitarTrocaSenha
{
    public class SolicitarTrocaSenhaCommandHandler : IRequestHandler<SolicitarTrocaSenhaCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmailService _emailService;

        public SolicitarTrocaSenhaCommandHandler(IUsuarioRepository usuarioRepository, IEmailService emailService)
        {
            _usuarioRepository = usuarioRepository;
            _emailService = emailService;
        }

        public async Task Handle(SolicitarTrocaSenhaCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(request.Email);
            if (usuario == null)
            {
                return;
            }

            var token = usuario.GerarTokenRecuperacao();

            await _usuarioRepository.AtualizarAsync(usuario);

            await _emailService.EnviarEmailAsync(usuario.Email, "Código de recupearção - Clinica Médica", 
                $@"<div style='font-family: Arial, sans-serif; padding: 20px; border: 1px solid #eee; border-radius: 5px;>
                <h2 style='color: #2563eb;'>Recuperação de Senha</h2>
                <p>Você solicitou a alteração da sua senha. Use o código abaixo: </p>
                <h1 style='background-color: #f3f4f6; padding: 10px; text-align: center; letter-spacing: 5px;'>{token}</h1>
                <p style='color: #666; font-size: 12px;'>Este código expira em 15 minutos.</p>
                </div>");
        }
    }
}
