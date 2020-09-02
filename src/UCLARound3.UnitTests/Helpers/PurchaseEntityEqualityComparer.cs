using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using UCLARound3.Domain.Entity;
using UCLARound3.Domain.Value;

namespace UCLARound3.UnitTests.Helpers
{
    public class BarcodeValueEqualityComparer : IEqualityComparer<BarcodeValue>
    {
        public bool Equals([DisallowNull] BarcodeValue x, [DisallowNull] BarcodeValue y)
            => x.ProductType == y.ProductType
               && x.ProductSubtype == y.ProductSubtype
               && x.Id == y.Id;

        public int GetHashCode([DisallowNull] BarcodeValue obj)
            => obj.GetHashCode();
    }
}
