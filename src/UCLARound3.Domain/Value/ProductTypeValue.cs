using UCLARound3.Domain.Value;

namespace UCLARound3.Domain
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
            // TODO validate length of input
        }

        public static implicit operator string(ProductTypeValue obj) => obj.Value;
        public static implicit operator ProductTypeValue(string obj) => new ProductTypeValue(obj);
        public static bool operator ==(ProductTypeValue a, ProductTypeValue b) => a.Value == b.Value;
        public static bool operator !=(ProductTypeValue a, ProductTypeValue b) => a.Value != b.Value;
    }
}
