using Clinica.Domain.Entities;
using Clinica.Infrastructure.Persistence.Converters;
using Clinica.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Clinica.Infrastructure.Persistence
{
    public class ClinicaDbContext : DbContext
    {

        private readonly AesEncryptionService _encryptionService;

        public ClinicaDbContext(
            DbContextOptions<ClinicaDbContext> options,
            AesEncryptionService encryptionService)
            : base(options)
        {
            _encryptionService = encryptionService;
        }

        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Paciente>()
                .Property(p => p.CPF)
                .HasConversion(new EncryptedStringConverter(_encryptionService));

            base.OnModelCreating(modelBuilder);
        }
    }
}
