using Clinica.Application.Interfaces;
using Clinica.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Infrastructure.Persistence.Repositories
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        private readonly ClinicaDbContext _context;

        public EspecialidadeRepository(ClinicaDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Especialidade especialidade)
        {
            await _context.Especialidades.AddAsync(especialidade);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Especialidade>> ObterTodasAsync()
        {
            return await _context.Especialidades
                .AsNoTracking()
                .OrderBy(e => e.Nome)
                .ToListAsync();
        }
    }
}
