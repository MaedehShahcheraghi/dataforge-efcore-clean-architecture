using DataForge.Domain.Entities;
using DataForge.Infrastructure.Persistence.Contexts;

namespace DataForge.Infrastructure.Persistence.DataSeed
{
    public class ProductSeeder(ApplicationDbContext applicationDbContext) : IDatabaseSeeder
    {
        public async Task SeedDataAsync(CancellationToken cancellationToken = default)
        {
            if (applicationDbContext.Products.Any())
            {
                return;
            }

            await applicationDbContext.Products.AddRangeAsync(new Product("Laptop", Guid.NewGuid().ToString(), 1500),
                new Product("AirPod", Guid.NewGuid().ToString(), 2000), new Product("Mobile", Guid.NewGuid().ToString(), 3000));
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
