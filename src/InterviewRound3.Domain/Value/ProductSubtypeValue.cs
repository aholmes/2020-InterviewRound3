using System;

namespace InterviewRound3.Domain.Value
{
    /// <summary>
    /// A 6-character product subtype.
    /// </summary>
    public class ProductSubtypeValue: ValueBase<string>
    {
        /// <summary>
        /// Get a new product subtype.
        /// </summary>
        /// <param name="value"></param>
        public ProductSubtypeValue(string value)
            : base(value)
        {
            if(value.Length != 6) throw new ArgumentException("A Product Subtype must be 6 characters long.", nameof(value));
        }

        public static implicit operator string(ProductSubtypeValue obj) => obj?.Value;
        public static implicit operator ProductSubtypeValue(string obj) => new ProductSubtypeValue(obj);
        public static bool operator ==(ProductSubtypeValue a, ProductSubtypeValue b) => a?.Value == b?.Value;
        public static bool operator !=(ProductSubtypeValue a, ProductSubtypeValue b) => a?.Value != b?.Value;
    }
}
