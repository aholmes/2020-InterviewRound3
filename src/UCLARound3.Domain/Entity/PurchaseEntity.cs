using System;
using System.Collections.Generic;
using UCLARound3.Domain.Value;

namespace UCLARound3.Domain.Entity
{
    public class PurchaseEntity
    {
        public DateTime Timestamp;
        public string Customer;
        public List<BarcodeValue> Barcodes;
    }
}
