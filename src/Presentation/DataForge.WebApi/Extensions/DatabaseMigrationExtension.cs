using DataForge.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataForge.WebApi.Extensions
{
    public static class DatabaseMigrationExtension
    {
        public static async Task ApplyMigrationsAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    logger.LogInformation("Applying pending migrations...");
                    await context.Database.MigrateAsync();
                    logger.LogInformation("Migrations applied successfully.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while applying the database migrations.");
                throw;
            }
        }
    }
}
