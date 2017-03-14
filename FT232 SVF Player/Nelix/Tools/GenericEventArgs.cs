using System;

namespace Nelix.Tools
{
    public class EventArgs<T> : EventArgs
    {
        public T Value { get; private set; }

        public EventArgs() : base() { }

        public EventArgs(T value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            Value = value;
        }
    }
}
