using System;
using System.Collections.Generic;
using System.Reflection;

namespace TestConsole.Domain.Core
{
    public abstract class DomainEvent<TEntity> : DomainEvent where TEntity : Entity
    {
        protected DomainEvent(string entityId)
            : base(typeof(TEntity), entityId) { }
    }

    public abstract class DomainEvent : IEquatable<DomainEvent>
    {
        public Guid Id { get; }
        public DateTime CreationDate { get; }
        public string Type => GetType().ToString();

        public string EntityType { get; }
        public string EntityId { get; }

        public IDictionary<string, object> Data
        {
            get
            {
                var dic = new Dictionary<string, object>();
                foreach (var property in GetType()
                    .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
                    dic.Add(property.Name, property.GetValue(this));
                return dic;
            }
        }

        protected DomainEvent(
            Type entityType,
            string entityId)
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            EntityType = entityType.ToString();
            EntityId = entityId;
        }

        #region Equality methods
        public bool Equals(DomainEvent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DomainEvent)obj);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(DomainEvent left, DomainEvent right)
            => Equals(left, right);

        public static bool operator !=(DomainEvent left, DomainEvent right)
            => !Equals(left, right);
        #endregion
    }
}
