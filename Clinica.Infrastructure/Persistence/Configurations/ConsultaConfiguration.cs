using Clinica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Infrastructure.Persistence.Configurations
{
    public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Status)
                .HasConversion<string>();
        }
    }
}
