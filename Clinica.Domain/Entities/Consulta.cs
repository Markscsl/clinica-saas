using Clinica.Domain.Enums;

namespace Clinica.Domain.Entities
{
    public class Consulta
    {
        public Guid Id { get; private set; }
        public Guid PacienteId { get; private set; }
        public virtual Paciente? Paciente { get; private set; }
        public Guid MedicoId { get; private set; }
        public virtual Medico? Medico { get; private set; }
        public DateTime DataHoraInicio { get; private set; }
        public TimeSpan Duracao { get; private set; }
        public StatusConsulta Status { get; private set; }

        private Consulta() { }

        public Consulta(Guid id, Guid pacienteId, Guid medicoId, DateTime dataHoraInicio)
        {
            if (dataHoraInicio < DateTime.Now)
            {
                throw new Exception("Não é possível agendar uma consulta em data/hora passada.");
            }

            if(id == Guid.Empty || pacienteId == Guid.Empty || medicoId == Guid.Empty)
            {
                throw new Exception("IDs de consulta, paciente e médico são obrigatórios.");
            }

            Id = id;
            PacienteId = pacienteId;
            MedicoId = medicoId; 
            DataHoraInicio = dataHoraInicio;

            Status = StatusConsulta.Agendada;
            Duracao = TimeSpan.FromMinutes(30);
        }

        public void Cancelar()
        {
            if (Status == StatusConsulta.Agendada || Status == StatusConsulta.Confirmada)
            {
                Status = StatusConsulta.Cancelada;
            }

            else
            {
                throw new Exception($"Não é possível cancelar uma consulta com status: {Status}.");
            }
        }

        public void Confirmar()
        {
            if (Status == StatusConsulta.Agendada)
            {
                Status = StatusConsulta.Confirmada;
            }
        }

        public void MarcarComoRealizada()
        {
            if (Status == StatusConsulta.Agendada || Status == StatusConsulta.Confirmada)
            {
                Status = StatusConsulta.Realizada;
            }
           
            throw new Exception($"Não é possivel marcar como realizada uma consulta com o status: '{Status}'");
        }
    }
}
