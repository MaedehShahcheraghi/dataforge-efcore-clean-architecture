namespace DataForge.Domain.Common.Entities
{
    public abstract class Entity<TId> : IEntity where TId : IEquatable<TId>
    {
        private readonly List<BaseEvent> _domainEvents = new();
        public TId Id { get; protected set; } = default!;

        public IReadOnlyList<BaseEvent> GetDomainEvents() => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents() => _domainEvents.Clear();

        public override bool Equals(object? obj)
        {
            if (obj is not Entity<TId> other)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            if (EqualityComparer<TId>.Default.Equals(Id, default) ||
                EqualityComparer<TId>.Default.Equals(other.Id, default))
            {
                return false;
            }

            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode() => HashCode.Combine(GetType(), Id);

        public static bool operator ==(Entity<TId>? left, Entity<TId>? right) =>
            left?.Equals(right) ?? right is null;

        public static bool operator !=(Entity<TId>? left, Entity<TId>? right) =>
            !(left == right);
    }
}
