namespace Clinica.Domain.Entities
{
    public class Especialidade
    {
        public Guid Id { get; private set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        private Especialidade() { }

        public Especialidade(Guid id, string nome, string descricao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
        }
    }
}
