namespace BagelCat.Domain
{
    public abstract class SimpleValueObject<T> : ValueObject<SimpleValueObject<T>>
    {
        protected SimpleValueObject(T value)
        {
            ValidateValue(value);

            Value = value;
        }

        public T Value { get; }

        public override int GetHashCode()
        {
            return ReferenceEquals(default(T), Value) ? 0 : Value.GetHashCode();
        }

        public override string ToString()
        {
            return ReferenceEquals(default(T), Value) ? string.Empty : Value.ToString();
        }

        protected abstract void ValidateValue(T value);

        protected override bool DetermineEquality(SimpleValueObject<T> other)
        {
            return ReferenceEquals(default(T), Value) ? ReferenceEquals(default(T), other.Value) : Value.Equals(other.Value);
        }

        public static implicit operator T(SimpleValueObject<T> value)
        {
            if (value == null)
            {
                return default;
            };

            return value.Value;
        }
    }
}
