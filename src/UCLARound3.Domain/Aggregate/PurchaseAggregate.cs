using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UCLARound3.Domain.Entity;
using UCLARound3.Domain.Value;

namespace UCLARound3.Domain.Aggregate
{
    public class PurchaseAggregate
    {
        public readonly PurchaseEntity Purchase;

        public PurchaseAggregate(PurchaseEntity purchase)
        {
            if (purchase == null) throw new ArgumentNullException(nameof(purchase));

            Purchase = purchase;
        }

        public List<string> GetUniqueIds()
            => new HashSet<string>(Purchase.Barcodes.Select(barcode => barcode.Id)).ToList();

        public IGrouping<string, BarcodeValue> GetMostCommonProductByType()
        {
            var productTypeCounts = new Dictionary<(string productType, BarcodeValue barcode), int>();
            foreach (var barcode in Purchase.Barcodes)
            {
                productTypeCounts.TryGetValue((barcode.ProductType, barcode), out int productTypeCount);
                productTypeCounts[(barcode.ProductType, barcode)] = productTypeCount + 1;
            }

            var productTypeGroups = from barcode in Purchase.Barcodes
                                         group barcode by barcode.ProductType
                                         into grouping
                                         select grouping;

            return productTypeGroups.OrderByDescending(g => g.Count()).First();
        }

        public List<string> GetProductSubtypes(string productType)
        {
            return (from barcode in Purchase.Barcodes
                   where barcode.ProductType == productType
                   select barcode.ProductSubtype).Distinct().ToList();
        }
    }
}
