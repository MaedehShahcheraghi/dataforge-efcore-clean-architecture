using DataForge.Infrastructure.Persistence.Contexts;
using DataForge.Infrastructure.Persistence.Seeding.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataForge.Infrastructure.Persistence.Seeding.Seeders
{
    public class ProductCategorySeeder(ApplicationDbContext dbContext) : IDataSeeder
    {
        public SeedEnvironment SeedEnvironment => SeedEnvironment.Development;
        public SeedPhase Phase => SeedPhase.Relations;

        public async Task SeedDataAsync(
            CancellationToken cancellationToken = default)
        {
            var relations = new[]
            {
                new { ProductSku = "LAPTOP-001", CategorySlug = "electrical", DisplayOrder = 1 },
                new { ProductSku = "AIRPOD-001", CategorySlug = "electrical", DisplayOrder = 1 },
                new { ProductSku = "MOBILE-001", CategorySlug = "electrical", DisplayOrder = 1 }
            };

            var productSkus = relations
                .Select(x => x.ProductSku)
                .Distinct()
                .ToArray();

            var categorySlugs = relations
                .Select(x => x.CategorySlug)
                .Distinct()
                .ToArray();

            var products = await dbContext.Products
                .Where(x => productSkus.Contains(x.Sku))
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category)
                .ToDictionaryAsync(
                    x => x.Sku,
                    cancellationToken);

            var categories = await dbContext.Categories
                .Where(x => categorySlugs.Contains(x.Slug))
                .ToDictionaryAsync(
                    x => x.Slug,
                    cancellationToken);

            foreach (var relation in relations)
            {
                if (!products.TryGetValue(
                        relation.ProductSku,
                        out var product))
                {
                    throw new InvalidOperationException(
                        $"Seed product '{relation.ProductSku}' was not found.");
                }

                if (!categories.TryGetValue(
                        relation.CategorySlug,
                        out var category))
                {
                    throw new InvalidOperationException(
                        $"Seed category '{relation.CategorySlug}' was not found.");
                }

                product.AddCategory(
                    category,
                    relation.DisplayOrder);
            }
        }
    }
}
