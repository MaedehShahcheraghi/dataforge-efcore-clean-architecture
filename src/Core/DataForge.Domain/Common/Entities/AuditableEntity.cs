namespace DataForge.Domain.Common.Entities
{
    public abstract class AuditableEntity<TId> : Entity<TId>, IAuditableEntity, ISoftDeletableEntity where TId : IEquatable<TId>
    {
        #region Properties

        public DateTime CreatedAtUtc { get; private set; }

        public DateTime? ModifiedAtUtc { get; private set; }

        public bool IsDeleted { get; private set; }

        public DateTime? DeletedAtUtc { get; private set; }

        #endregion

        #region Behaviors

        public void SetCreatedAt(DateTime createdAtUtc) => CreatedAtUtc = createdAtUtc;

        public void SetModifiedAt(DateTime modifiedAtUtc) => ModifiedAtUtc = modifiedAtUtc;

        public void Delete(DateTime deletedAtUtc)
        {
            if (IsDeleted)
            {
                return;
            }

            IsDeleted = true;
            DeletedAtUtc = deletedAtUtc;
        }

        public void Restore()
        {
            if (!IsDeleted)
            {
                return;
            }

            IsDeleted = false;
            DeletedAtUtc = null;
        }

        #endregion
    }
}
