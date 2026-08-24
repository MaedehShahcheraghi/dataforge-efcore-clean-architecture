using DataForge.Domain.Entities;
using DataForge.Infrastructure.Persistence.Contexts;
using DataForge.Infrastructure.Persistence.Seeding.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataForge.Infrastructure.Persistence.Seeding.Seeders
{
    public class CategorySeeder(ApplicationDbContext dbContext) : IDataSeeder
    {
        public SeedEnvironment SeedEnvironment => SeedEnvironment.Development;
        public SeedPhase Phase => SeedPhase.MasterData;

        public async Task SeedDataAsync(CancellationToken cancellationToken)
        {
            var seedData = new[]
            {
                new { Name = "Electrical", Slug = "electrical" }, new { Name = "Cosmetic", Slug = "cosmetic" },
                new { Name = "Woody", Slug = "woody" }
            };

            var slugs = seedData
                .Select(x => x.Slug)
                .ToArray();

            var existingSlugs = await dbContext.Categories
                .Where(x => slugs.Contains(x.Slug))
                .Select(x => x.Slug)
                .ToHashSetAsync(cancellationToken);

            var categories = seedData
                .Where(x => !existingSlugs.Contains(x.Slug))
                .Select(x => new Category(
                    x.Name,
                    x.Slug))
                .ToArray();

            if (categories.Length == 0)
            {
                return;
            }

            await dbContext.Categories.AddRangeAsync(
                categories,
                cancellationToken);
        }
    }
}
