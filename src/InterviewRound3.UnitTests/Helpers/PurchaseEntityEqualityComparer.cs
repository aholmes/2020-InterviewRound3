using InterviewRound3.Domain.Value;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace InterviewRound3.UnitTests.Helpers
{
    /// <summary>
    /// Used to simplify equality assertions for <see cref="BarcodeValue"/> instances.
    /// </summary>
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
