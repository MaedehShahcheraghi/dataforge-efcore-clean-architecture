namespace DataForge.Infrastructure.Persistence.Seeding.Abstractions
{
    public interface IDataSeeder
    {
        public SeedEnvironment SeedEnvironment { get; }
        public SeedPhase Phase { get; }
        Task SeedDataAsync(CancellationToken cancellationToken);
    }
}
