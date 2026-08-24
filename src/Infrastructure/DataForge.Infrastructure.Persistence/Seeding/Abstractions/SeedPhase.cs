namespace DataForge.Infrastructure.Persistence.Seeding.Abstractions
{
    public enum SeedPhase
    {
        ReferenceData = 100,

        MasterData = 200,

        Relations = 300,

        DevelopmentData = 400
    }
}
