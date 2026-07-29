namespace DataForge.Domain.Common.Entities
{
    public interface ISoftDeletableEntity
    {
        bool IsDeleted { get; }

        DateTime? DeletedAtUtc { get; }

        void Delete(DateTime deletedAtUtc);

        void Restore();
    }
}
