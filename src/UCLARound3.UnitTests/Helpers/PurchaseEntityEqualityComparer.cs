using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UCLARound3.Domain.Value;

namespace UCLARound3.UnitTests.Helpers
{
    public class BarcodeValueEqualityComparer: IEqualityComparer<BarcodeValue>
    {
        public bool Equals([DisallowNull] BarcodeValue a, [DisallowNull] BarcodeValue b)
            => a.ProductType == b.ProductType
               && a.ProductSubtype == b.ProductSubtype
               && a.Id == b.Id;

        public int GetHashCode([DisallowNull] BarcodeValue obj)
            => obj.GetHashCode();
    }
}
