using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Clinica.Infrastructure.Services; 

namespace Clinica.Infrastructure.Persistence
{
    public class ClinicaDbContextFactory : IDesignTimeDbContextFactory<ClinicaDbContext>
    {
        public ClinicaDbContext CreateDbContext(string[] args)
        {
            var apiProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Clinica.Api");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(apiProjectPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ClinicaDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);


            var encryptionService = new AesEncryptionService(configuration);

            return new ClinicaDbContext(optionsBuilder.Options, encryptionService);
        }
    }
}