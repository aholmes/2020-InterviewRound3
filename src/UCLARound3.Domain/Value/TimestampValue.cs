using System;

namespace InterviewRound3.Domain.Value
{
    /// <summary>
    /// A DateTime.
    /// </summary>
    public class TimestampValue: ValueBase<DateTime>
    {
        /// <summary>
        /// Get a new timestamp.
        /// </summary>
        /// <param name="value"></param>
        public TimestampValue(DateTime value)
            : base(value)
        {
        }

        public static implicit operator DateTime(TimestampValue obj) => obj.Value;
        public static implicit operator TimestampValue(DateTime obj) => new TimestampValue(obj);
        public static bool operator ==(TimestampValue a, TimestampValue b) => a?.Value == b?.Value;
        public static bool operator !=(TimestampValue a, TimestampValue b) => a?.Value != b?.Value;
    }
}
