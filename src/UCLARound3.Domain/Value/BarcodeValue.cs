using System.Diagnostics;

namespace UCLARound3.Domain.Value
{
    /// <summary>
    /// Information for a barcode
    /// </summary>
    [DebuggerDisplay("{ProductType} {ProductSubtype} {Id}")]
    public class BarcodeValue
    {
        /// <summary>
        /// A 4-character product code.
        /// Valid values can be found in <see cref="ProductKeys"/>.
        /// </summary>
        public ProductTypeValue ProductType;

        /// <summary>
        /// A 6-character product subtype.
        /// </summary>
        public ProductSubtypeValue ProductSubtype;

        /// <summary>
        /// A 20-character unique identifier.
        /// </summary>
        public ProductIdValue Id;
    }
}
