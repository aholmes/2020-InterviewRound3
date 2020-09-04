using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace UCLARound3.Domain.Value
{
    public class TimestampValue: ValueBase<DateTime>
    {
        public TimestampValue(DateTime value)
            :base(value)
        {
        }

        public static implicit operator DateTime(TimestampValue obj) => obj.Value;
        public static implicit operator TimestampValue(DateTime obj) => new TimestampValue(obj);
        public static bool operator ==(TimestampValue a, TimestampValue b) => a.Value == b.Value;
        public static bool operator !=(TimestampValue a, TimestampValue b) => a.Value != b.Value;
    }
}
