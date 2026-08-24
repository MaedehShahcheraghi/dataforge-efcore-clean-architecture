using DataForge.Infrastructure.Persistence.Contexts;
using DataForge.Infrastructure.Persistence.Seeding.Abstractions;
using Microsoft.Extensions.Hosting;

namespace DataForge.Infrastructure.Persistence.Seeding
{
    public sealed class DatabaseSeedRunner(
        ApplicationDbContext dbContext,
        IEnumerable<IDataSeeder> seeders,
        IHostEnvironment hostEnvironment)
    {
        public async Task RunAsync(
            CancellationToken cancellationToken = default)
        {
            var currentEnvironment =
                ResolveEnvironment(hostEnvironment);

            var executableSeeders = seeders
                .Where(x =>
                    x.SeedEnvironment.HasFlag(currentEnvironment))
                .GroupBy(x => x.Phase)
                .OrderBy(x => x.Key);

            foreach (var phase in executableSeeders)
            {
                foreach (var seeder in phase)
                {
                    await seeder.SeedDataAsync(
                        cancellationToken);
                }

                await dbContext.SaveChangesAsync(
                    cancellationToken);
            }
        }

        private static SeedEnvironment ResolveEnvironment(
            IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                return SeedEnvironment.Development;
            }

            if (environment.IsStaging())
            {
                return SeedEnvironment.Staging;
            }

            if (environment.IsProduction())
            {
                return SeedEnvironment.Production;
            }

            if (environment.IsEnvironment("Testing"))
            {
                return SeedEnvironment.Testing;
            }

            return SeedEnvironment.None;
        }
    }
}
