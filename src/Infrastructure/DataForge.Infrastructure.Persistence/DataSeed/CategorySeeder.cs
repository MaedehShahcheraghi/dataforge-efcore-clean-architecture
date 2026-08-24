using DataForge.Domain.Entities;
using DataForge.Infrastructure.Persistence.Contexts;

namespace DataForge.Infrastructure.Persistence.DataSeed
{
    public class CategorySeeder(ApplicationDbContext applicationDbContext) : IDatabaseSeeder
    {
        public async Task SeedDataAsync(CancellationToken cancellationToken = default)
        {
            if (applicationDbContext.Categories.Any())
            {
                return;
            }

            await applicationDbContext.Categories.AddRangeAsync(new Category("Electrical", "12345"), new Category("Cosmetic", "12345"),
                new Category("woody", "12345"));
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
