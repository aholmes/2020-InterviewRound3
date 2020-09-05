using System.Diagnostics;

namespace UCLARound3.Domain.Value
{
    /// <summary>
    /// A base type for consolidating domain value-type methods.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerDisplay("{Value}")]
    public abstract class ValueBase<T>
    {
        /// <summary>
        /// The value of the derived type.
        /// </summary>
        protected T Value;

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value"></param>
        internal ValueBase(T value)
        {
            Value = value;
        }

        /// <summary>
        /// ValueBase{T} are equal when their internal T-typed values are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
            => (obj as ValueBase<T>)?.Value?.Equals(Value) ?? false;
        public override int GetHashCode() => (int)Value?.GetHashCode();
        public override string ToString() => Value?.ToString();
    }
}
