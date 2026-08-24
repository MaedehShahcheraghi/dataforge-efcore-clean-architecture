namespace DataForge.Infrastructure.Persistence.DataSeed
{
    public interface IDatabaseSeeder
    {
        Task SeedDataAsync(CancellationToken cancellationToken = default);
    }
}
