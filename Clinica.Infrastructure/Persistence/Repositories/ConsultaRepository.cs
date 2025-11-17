using Clinica.Application.Consultas.Queries;
using Clinica.Application.Interfaces;
using Clinica.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Infrastructure.Persistence.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly ClinicaDbContext _context;

        public ConsultaRepository(ClinicaDbContext context)
        {
            _context = context;
        }


        public async Task AdicionarAsync(Consulta consulta)
        {
            await _context.Consultas.AddAsync(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task<Consulta?> ObterPorIdAsync(Guid id)
        {
            return await _context.Consultas.FindAsync(id);
        }

        public async Task AtualizarAsync(Consulta consulta)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteConflitoDeHorarioAsync(Guid medicoId, DateTime dataHoraInicio)
        {
            return await _context.Consultas
                .AnyAsync(c => c.MedicoId == medicoId &&
                c.DataHoraInicio == dataHoraInicio &&
                c.Status != Domain.Enums.StatusConsulta.Cancelada);
        }

        // (Os seus usings... Microsoft.EntityFrameworkCore, Clinica.Application.Consultas.Queries, etc)

        // ...

        public async Task<ConsultaResponse?> ObterConsultaDtoPorIdAsync(Guid consultaId)
        {
            var query =
                from consulta in _context.Consultas.AsNoTracking()

                where consulta.Id == consultaId

                join paciente in _context.Pacientes
                    on consulta.PacienteId equals paciente.Id

                join medico in _context.Medicos
                    on consulta.MedicoId equals medico.Id

                select new ConsultaResponse
                {
                    Id = consulta.Id,
                    DataHoraInicio = consulta.DataHoraInicio,
                    Status = consulta.Status.ToString(),
                    PacienteId = paciente.Id,
                    NomePaciente = paciente.Nome,
                    MedicoId = medico.Id,
                    NomeMedico = medico.Nome
                };

            return await query.FirstOrDefaultAsync();
        }
    }
}
