using Clinica.Application.Interfaces;
using Clinica.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ClinicaDbContext _context;

        public UsuarioRepository(ClinicaDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarASync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u =>  u.Email == email);
        }
    }
}
