using UCLARound3.Domain.Value;

namespace UCLARound3.Domain.Value
{
    /// <summary>
    /// A customer who made a purchase.
    /// </summary>
    public class CustomerValue: ValueBase<string>
    {
        /// <summary>
        /// Get a new customer instance.
        /// </summary>
        /// <param name="value">The customer's name.</param>
        public CustomerValue(string value)
            : base(value)
        {
        }

        public static implicit operator string(CustomerValue obj) => obj.Value;
        public static implicit operator CustomerValue(string obj) => new CustomerValue(obj);
        public static bool operator ==(CustomerValue a, CustomerValue b) => a.Value == b.Value;
        public static bool operator !=(CustomerValue a, CustomerValue b) => a.Value != b.Value;
    }
}
