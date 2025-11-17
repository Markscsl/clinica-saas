namespace Clinica.Application.Consultas.Queries
{
    public class ConsultaResponse
    {
        public Guid Id { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public string Status { get; set; }
        public Guid PacienteId { get; set; }
        public string NomePaciente { get; set; }
        public Guid MedicoId { get; set; }
        public string NomeMedico { get; set; }
    }
}
