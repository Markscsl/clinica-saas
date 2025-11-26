using Clinica.Application.Interfaces;
using Clinica.Domain.Constants;
using Clinica.Domain.Entities;
using Clinica.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Api.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ClinicaDbContext>();
                    var authService = services.GetRequiredService<IAuthService>();

                    await context.Database.MigrateAsync();

                    if (await context.Usuarios.AnyAsync())
                    {
                        return;
                    }

                    Console.WriteLine("Criando usuário Admin padrão...");

                    var senhaHash = authService.ComputeHash("Admin@123");

                    var admin = new Usuario(
                        "admin@clinica.com",
                        senhaHash,
                        Roles.Admin
                        );

                    context.Usuarios.Add(admin);
                    await context.SaveChangesAsync();

                    Console.WriteLine("Admin padrão criado com sucesso!");

                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao semear o banco: {ex.Message}");
                }
            }
        }
    }
}

