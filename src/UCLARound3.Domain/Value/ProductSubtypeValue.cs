using UCLARound3.Domain.Value;

namespace UCLARound3.Domain
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
            // TODO validate length of input
        }

        public static implicit operator string(ProductSubtypeValue obj) => obj.Value;
        public static implicit operator ProductSubtypeValue(string obj) => new ProductSubtypeValue(obj);
        public static bool operator ==(ProductSubtypeValue a, ProductSubtypeValue b) => a.Value == b.Value;
        public static bool operator !=(ProductSubtypeValue a, ProductSubtypeValue b) => a.Value != b.Value;
    }
}
