namespace Clinica.Domain.Entities
{
    public class Paciente
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }

        private Paciente() { }

        public Paciente(Guid id, string nome, string cpf, string telefone, string email)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("O nome do paciente é obrigatório.");
            }

            Id = id;
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Email = email;
        }

        public void AtualizarTelefone(string novoTelefone)
        {
            if (string.IsNullOrWhiteSpace(novoTelefone))
            {
                throw new Exception("Telefone inválido.");
            }

            Telefone = novoTelefone;
        }
    }
}
