using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsole.Domain.Core
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public bool Equals(ValueObject obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = obj;
            using (var thisValues = GetAtomicValues().GetEnumerator())
            using (var otherValues = other.GetAtomicValues().GetEnumerator())
            {
                while (thisValues.MoveNext() && otherValues.MoveNext())
                {
                    if (thisValues.Current is null ^ otherValues.Current is null)
                        return false;
                    if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                        return false;
                }
                return !thisValues.MoveNext() && !otherValues.MoveNext();
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ValueObject)obj);
        }

        public override int GetHashCode()
            => GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);

        protected abstract IEnumerable<object> GetAtomicValues();

        #region Static methods
        public static bool operator ==(ValueObject left, ValueObject right) => Equals(left, right);
        public static bool operator !=(ValueObject left, ValueObject right) => !Equals(left, right);
        #endregion
    }
}
