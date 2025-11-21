using Clinica.Application.Interfaces;
using Clinica.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Infrastructure.Persistence.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ClinicaDbContext _context;

        public PacienteRepository(ClinicaDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Paciente paciente)
        {
            await _context.Pacientes.AddAsync(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task<Paciente?> ObterPorIdAsync(Guid id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task AtualizarAsync(Paciente paciente)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Paciente>> ObterTodosAsync()
        {
            return await _context.Pacientes
                .AsNoTracking()
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }
    }
}
