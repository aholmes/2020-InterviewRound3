using System.Diagnostics;

namespace UCLARound3.Domain.Value
{
    [DebuggerDisplay("{Value}")]
    public abstract class ValueBase<T>
    {
        protected readonly T Value;

        internal ValueBase(T value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
            => (obj as ValueBase<T>)?.Value?.Equals(Value) ?? false;
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();
    }
}
