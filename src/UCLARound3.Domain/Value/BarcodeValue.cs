using System.Diagnostics;

namespace UCLARound3.Domain.Value
{
    [DebuggerDisplay("{ProductType} {ProductSubtype} {Id}")]
    public class BarcodeValue
    {
        public string ProductType;
        public string ProductSubtype;
        public string Id;
    }
}
