using System.Diagnostics;

namespace UCLARound3.Domain.Value
{
    [DebuggerDisplay("{ProductType} {ProductSubtype} {Id}")]
    public class BarcodeValue
    {
        public ProductTypeValue ProductType;
        public ProductSubtypeValue ProductSubtype;
        public ProductIdValue Id;
    }
}
