using UCLARound3.Domain.Value;

namespace UCLARound3.Domain
{
    public class ProductIdValue: ValueBase<string>
    {
        public ProductIdValue(string value)
            : base(value)
        {
        }

        public static implicit operator string(ProductIdValue obj) => obj.Value;
        public static implicit operator ProductIdValue(string obj) => new ProductIdValue(obj);
        public static bool operator ==(ProductIdValue a, ProductIdValue b) => a.Value == b.Value;
        public static bool operator !=(ProductIdValue a, ProductIdValue b) => a.Value != b.Value;
    }
}
