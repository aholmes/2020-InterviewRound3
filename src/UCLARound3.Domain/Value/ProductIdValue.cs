using System;
using UCLARound3.Domain.Value;

namespace UCLARound3.Domain.Value
{
    /// <summary>
    /// A 20-character unique product identifier.
    /// </summary>
    public class ProductIdValue: ValueBase<string>
    {
        /// <summary>
        /// Get a new product ID.
        /// </summary>
        /// <param name="value"></param>
        public ProductIdValue(string value)
            : base(value)
        {
            if(value.Length != 20) throw new ArgumentException("A Product ID must be 20 characters long.", nameof(value));
        }

        public static implicit operator string(ProductIdValue obj) => obj?.Value;
        public static implicit operator ProductIdValue(string obj) => new ProductIdValue(obj);
        public static bool operator ==(ProductIdValue a, ProductIdValue b) => a?.Value == b?.Value;
        public static bool operator !=(ProductIdValue a, ProductIdValue b) => a?.Value != b?.Value;
    }
}
