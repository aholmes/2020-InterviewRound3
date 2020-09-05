using System;
using System.Linq;
using UCLARound3.Domain.Value;

namespace UCLARound3.Domain.Value
{
    /// <summary>
    /// A 4-character product type.
    /// </summary>
    public class ProductTypeValue: ValueBase<string>
    {
        /// <summary>
        /// Get a new product type.
        /// </summary>
        /// <param name="value"></param>
        public ProductTypeValue(string value)
            : base(value)
        {
            if(value.Length != 4) throw new ArgumentException("A Product Type must be 4 characters long.", nameof(value));

            var key = ProductTypeValueKeys.GetValidProductTypeKey(value);
            if (key != value)
            {
                Value = value;
            }
        }

        public static implicit operator string(ProductTypeValue obj) => obj?.Value;
        public static implicit operator ProductTypeValue(string obj) => new ProductTypeValue(obj);
        public static bool operator ==(ProductTypeValue a, ProductTypeValue b) => a?.Value == b?.Value;
        public static bool operator !=(ProductTypeValue a, ProductTypeValue b) => a?.Value != b?.Value;
    }
}
