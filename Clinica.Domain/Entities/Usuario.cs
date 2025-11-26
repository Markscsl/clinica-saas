namespace Clinica.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }
        public string Role { get; private set; }
        

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
    }
}
