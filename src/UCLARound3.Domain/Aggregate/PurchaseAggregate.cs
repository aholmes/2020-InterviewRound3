using System;
using System.Collections.Generic;
using System.Linq;
using UCLARound3.Domain.Entity;
using UCLARound3.Domain.Value;

namespace UCLARound3.Domain.Aggregate
{
    public class PurchaseAggregate
    {
        public readonly PurchaseEntity Purchase;

        public PurchaseAggregate(PurchaseEntity purchase)
        {
            Purchase = purchase;
        }

        public List<string> GetUniqueIds() => throw new NotImplementedException();
        public BarcodeValue GetMostCommonProductByType() => throw new NotImplementedException();
        public List<string> GetProductSubtypes(string productType) => throw new NotImplementedException();
    }
}
