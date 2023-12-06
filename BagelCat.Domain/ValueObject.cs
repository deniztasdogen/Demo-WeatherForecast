using System;

namespace BagelCat.Domain
{
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        public bool Equals(T other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (GetType() != other.GetType()) return false;

            return DetermineEquality(other);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is T other && Equals(other);
        }

        protected abstract bool DetermineEquality(T other);

        public abstract override int GetHashCode();

        public abstract override string ToString();

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        {
            return !Equals(left, right);
        }
    }
}
