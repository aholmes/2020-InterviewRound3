using UCLARound3.Domain.Value;

namespace UCLARound3.Domain
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
            // TODO validate length of input
        }

        public static implicit operator string(ProductIdValue obj) => obj.Value;
        public static implicit operator ProductIdValue(string obj) => new ProductIdValue(obj);
        public static bool operator ==(ProductIdValue a, ProductIdValue b) => a.Value == b.Value;
        public static bool operator !=(ProductIdValue a, ProductIdValue b) => a.Value != b.Value;
    }
}
