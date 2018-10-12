using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TestConsole.Domain.Core
{
    // Markers
    public interface IAggregateRoot { }
    public interface IEntity { }

    public abstract class Entity : IEntity
    {
        private readonly List<DomainEvent> _events = new List<DomainEvent>();

        [NotMapped]
        public IReadOnlyList<DomainEvent> Events => _events.AsReadOnly();
        public bool HasEvents => _events.Any();

        protected void RaiseEvent(DomainEvent eventItem)
        {
            //Check.NotNull(eventItem, nameof(eventItem));
            if (!_events.Contains(eventItem))
                _events.Add(eventItem);
        }

        public void ClearEvents() => _events.Clear();
    }

    public abstract class Entity<TKey> : Entity, IEquatable<Entity<TKey>>
    {
        public TKey Id { get; protected set; }

        #region Equality methods
        public bool Equals(Entity<TKey> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Entity<TKey>)obj);
        }

        public override int GetHashCode()
            => EqualityComparer<TKey>.Default.GetHashCode(Id);

        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
            => Equals(left, right);

        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
            => !Equals(left, right);
        #endregion
    }
}
