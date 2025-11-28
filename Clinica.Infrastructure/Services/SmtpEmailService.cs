using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Clinica.Application.Interfaces;


namespace Clinica.Infrastructure.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SmtpEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarEmailAsync(string para, string assunto, string mensagem)
        {
            var smtpHost = _configuration["EmailSettings:Host"];
            var smtpPort = int.Parse(_configuration["EmailSettings:Port"]);

            var username = _configuration["EmailSettings:Username"];
            var password = _configuration["EmailSettings:Password"];

            var senderEmail = _configuration["EmailSettings:SenderEmail"];


            //var remetenteEmail = _configuration["EmailSettings:Email"];
            //var remetenteSenha = _configuration["EmailSettings:Password"];

            var smtpClient = new SmtpClient(smtpHost)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail, "Clinica Médica."),
                Subject = assunto,
                Body = mensagem,
                IsBodyHtml = true
            };

            mailMessage.To.Add(para);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
