namespace Clinica.Domain.Entities
{
    public class Medico
    {
        public Guid Id { get; private set; }
        public string Nome { get; set; }
        public string Crm { get; set; }
        public Guid EspecialidadeId { get; private set; }

        public virtual Especialidade? Especialidade { get; private set; }

        private Medico() { }

        public Medico(Guid id, string nome, string crm, Guid especialidadeId)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("O nome do médico é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(crm))
            {
                throw new Exception("O CRM do médico é obrigatório.");
            }

            Id = id;
            Nome = nome;
            Crm = crm;
            EspecialidadeId = especialidadeId;
        }
    }
}
