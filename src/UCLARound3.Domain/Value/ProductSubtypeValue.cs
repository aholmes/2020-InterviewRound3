using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using UCLARound3.Domain.Value;

namespace UCLARound3.Domain
{
    public class ProductSubtypeValue: ValueBase<string>
    {
        public ProductSubtypeValue(string value)
            :base(value)
        {
        }

        public static implicit operator string(ProductSubtypeValue obj) => obj.Value;
        public static implicit operator ProductSubtypeValue(string obj) => new ProductSubtypeValue(obj);
        public static bool operator ==(ProductSubtypeValue a, ProductSubtypeValue b) => a.Value == b.Value;
        public static bool operator !=(ProductSubtypeValue a, ProductSubtypeValue b) => a.Value != b.Value;
    }
}
