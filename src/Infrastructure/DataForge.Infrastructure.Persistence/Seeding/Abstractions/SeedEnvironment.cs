namespace DataForge.Infrastructure.Persistence.Seeding.Abstractions
{
    [Flags]
    public enum SeedEnvironment
    {
        None = 0,

        Development = 1 << 0,

        Testing = 1 << 1,

        Staging = 1 << 2,

        Production = 1 << 3,

        All =
            Development |
            Testing |
            Staging |
            Production
    }
}
