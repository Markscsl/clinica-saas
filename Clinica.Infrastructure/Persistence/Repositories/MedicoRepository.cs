using Clinica.Application.Interfaces;
using Clinica.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Infrastructure.Persistence.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly ClinicaDbContext _context;

        public MedicoRepository(ClinicaDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Medico medico)
        {
            await _context.Medicos.AddAsync(medico);
            await _context.SaveChangesAsync();

        }

        public async Task<Medico?> ObterPorIdASync(Guid id)
        {
            return await _context.Medicos.FindAsync(id);
        }

        public async Task<IEnumerable<Medico>> ObterTodosAsync()
        {
            return await _context.Medicos
                .AsNoTracking()
                .Include(m => m.Especialidade)
                .ToListAsync();
        }
    }
}
