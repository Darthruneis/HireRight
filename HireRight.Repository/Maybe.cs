using System;

namespace HireRight.Repository
{
    public struct Maybe<T> where T: class
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if(HasNoValue)
                    throw new InvalidOperationException("Can not access the value of a null Maybe.");

                return _value;
            }
        }

        public bool HasValue => _value != null;
        public bool HasNoValue => !HasValue;

        public Maybe(T value)
        {
            _value = value;
        }

        public static Maybe<T> Empty()
        {
            return new Maybe<T>();
        }

        public static implicit operator Maybe<T>(T value)
        {
            return value == null ? Empty() : new Maybe<T>(value);
        }
    }
}