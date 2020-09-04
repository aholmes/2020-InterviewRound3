using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using UCLARound3.Domain.Value;

namespace UCLARound3.Domain
{
    public class ProductTypeValue: ValueBase<string>
    {
        public ProductTypeValue(string value)
            :base(value)
        {
        }

        public static implicit operator string(ProductTypeValue obj) => obj.Value;
        public static implicit operator ProductTypeValue(string obj) => new ProductTypeValue(obj);
        public static bool operator ==(ProductTypeValue a, ProductTypeValue b) => a.Value == b.Value;
        public static bool operator !=(ProductTypeValue a, ProductTypeValue b) => a.Value != b.Value;
    }
}
