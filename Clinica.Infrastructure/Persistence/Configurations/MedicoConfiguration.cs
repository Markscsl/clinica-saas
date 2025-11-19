using Clinica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Infrastructure.Persistence.Configurations
{
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Nome).IsRequired();
            builder.Property(m => m.Crm).IsRequired();

            builder.HasOne(m => m.Especialidade)
                .WithMany()
                .HasForeignKey(m => m.EspecialidadeId)
                .IsRequired();
        }
    }
}
