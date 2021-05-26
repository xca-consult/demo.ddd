using System;

namespace Demo.DDD.Domain
{
    public abstract class SingleValueObject<T>
    {
        public T Value { get; }

        protected SingleValueObject(T value)
        {
            if (Equals(value, default(T)))
                throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            return ((SingleValueObject<T>)obj).Value.Equals(Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}