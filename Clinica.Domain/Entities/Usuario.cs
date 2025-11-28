namespace Clinica.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }
        public string Role { get; private set; }

        public string? PasswordResetToken { get; private set; }
        public DateTime? PasswordResetExpiry { get; private set; }

        private Usuario() { }

        public Usuario(string email, string senhaHash, string role)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email é obrigatório.");

            if (string.IsNullOrWhiteSpace(senhaHash))
                throw new Exception("Senha é obrigatória.");

            Id = Guid.NewGuid();
            Email = email;
            SenhaHash = senhaHash;
            Role = role;
        }

        public void AlterarSenha(string novoHash)
        {
            SenhaHash = novoHash;
        }

        public string GerarTokenRecuperacao()
        {
            var token = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();

            PasswordResetToken = token;

            PasswordResetExpiry = DateTime.Now.AddMinutes(15);

            return token;
        }
        
        public void RedefinirSenha(string novaSenhaHash)
        {
            SenhaHash = novaSenhaHash;

            PasswordResetToken = null;
            PasswordResetExpiry = null;
        }
    }
}
