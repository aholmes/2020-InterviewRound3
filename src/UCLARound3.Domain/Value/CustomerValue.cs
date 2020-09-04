using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using UCLARound3.Domain.Value;

namespace UCLARound3.Domain
{
    public class CustomerValue: ValueBase<string>
    {
        public CustomerValue(string value)
            :base(value)
        {
        }

        public static implicit operator string(CustomerValue obj) => obj.Value;
        public static implicit operator CustomerValue(string obj) => new CustomerValue(obj);
        public static bool operator ==(CustomerValue a, CustomerValue b) => a.Value == b.Value;
        public static bool operator !=(CustomerValue a, CustomerValue b) => a.Value != b.Value;
    }
}
