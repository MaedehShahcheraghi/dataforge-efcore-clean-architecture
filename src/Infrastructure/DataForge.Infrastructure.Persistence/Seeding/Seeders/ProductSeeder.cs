using DataForge.Domain.Entities;
using DataForge.Infrastructure.Persistence.Contexts;
using DataForge.Infrastructure.Persistence.Seeding.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataForge.Infrastructure.Persistence.Seeding.Seeders
{
    public class ProductSeeder(ApplicationDbContext dbContext) : IDataSeeder
    {
        public SeedEnvironment SeedEnvironment => SeedEnvironment.Development;
        public SeedPhase Phase => SeedPhase.MasterData;

        public async Task SeedDataAsync(CancellationToken cancellationToken)
        {
            var seedData = new[]
            {
                new { Name = "Laptop", Sku = "LAPTOP-001", Price = 1500m }, new { Name = "AirPod", Sku = "AIRPOD-001", Price = 2000m },
                new { Name = "Mobile", Sku = "MOBILE-001", Price = 3000m }
            };

            var skus = seedData
                .Select(x => x.Sku)
                .ToArray();

            var existingSkus = await dbContext.Products
                .Where(x => skus.Contains(x.Sku))
                .Select(x => x.Sku)
                .ToHashSetAsync(cancellationToken);

            var products = seedData
                .Where(x => !existingSkus.Contains(x.Sku))
                .Select(x => new Product(
                    x.Name,
                    x.Sku,
                    x.Price))
                .ToArray();

            if (products.Length == 0)
            {
                return;
            }

            await dbContext.Products.AddRangeAsync(
                products,
                cancellationToken);
        }
    }
}
