using System;
using System.Collections.Generic;
using System.Linq;
using UCLARound3.Domain.Entity;
using UCLARound3.Domain.Value;

namespace UCLARound3.Domain.Aggregate
{
    public class PurchaseAggregate
    {
        public readonly PurchaseEntity[] Purchases;

        public PurchaseAggregate(IEnumerable<PurchaseEntity> purchases)
        {
            Purchases = purchases.ToArray();
        }

        public List<string> GetUniqueIds() => throw new NotImplementedException();
        public BarcodeValue GetMostCommonProductType() => throw new NotImplementedException();
        public List<string> GetProductSubTypes() => throw new NotImplementedException();
    }
}
