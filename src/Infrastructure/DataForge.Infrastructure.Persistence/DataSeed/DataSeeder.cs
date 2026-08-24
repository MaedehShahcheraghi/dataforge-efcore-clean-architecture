namespace DataForge.Infrastructure.Persistence.DataSeed
{
    public sealed class DatabaseSeeder(IEnumerable<IDatabaseSeeder> seeders)
    {
        public async Task SeedAsync()
        {
            foreach (var seeder in seeders)
            {
                await seeder.SeedDataAsync();
            }
        }
    }
}
