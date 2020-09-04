using System;
using System.Collections.Generic;
using System.Linq;
using UCLARound3.Domain.Entity;
using UCLARound3.Domain.Value;

namespace UCLARound3.Domain.Aggregate
{
    /// <summary>
    /// Aggregation methods for a <see cref="PurchaseEntity"/>.
    /// </summary>
    public class PurchaseAggregate
    {
        private readonly PurchaseEntity _purchase;

        /// <summary>
        /// Get a new instance with the given <see cref="PurchaseEntity"/>.
        /// </summary>
        /// <param name="purchase"></param>
        public PurchaseAggregate(PurchaseEntity purchase)
        {
            if(purchase == null)
                throw new ArgumentNullException(nameof(purchase));

            _purchase = purchase;
        }

        // TODO update this to only return unique IDs for
        // products that are in the ProductKeys hashset.
        // FIXME did I understand correctly what this is supposed to do?
        /// <summary>
        /// Get unique <see cref="ProductIdValue">Product IDs</see> for the purchase being aggregated.
        /// </summary>
        /// <returns></returns>
        public List<ProductIdValue> GetUniqueIds()
            => _purchase.Barcodes.Select(barcode => barcode.Id).Distinct().ToList();

        /// <summary>
        /// Get a grouping of <see cref="BarcodeValue">Product Barcodes</see> for the most
        /// common <see cref="ProductTypeValue">Product Type</see> for the purchase being aggregated.
        /// </summary>
        /// <returns>
        /// An <see cref="IGrouping{TKey, TElement}"/> whose key is the most common <see cref="ProductTypeValue">Product Type</see>
        /// and whose sequence are the <see cref="BarcodeValue">Product Barcodes</see> for that Product Type in the purchase being aggregated.
        /// </returns>
        public IGrouping<ProductTypeValue, BarcodeValue> GetMostCommonProductByType()
        {
            var productTypeCounts = new Dictionary<(ProductTypeValue productType, BarcodeValue barcode), int>();
            foreach(var barcode in _purchase.Barcodes)
            {
                productTypeCounts.TryGetValue((barcode.ProductType, barcode), out int productTypeCount);
                productTypeCounts[(barcode.ProductType, barcode)] = productTypeCount + 1;
            }

            var productTypeGroups = from barcode in _purchase.Barcodes
                                    group barcode by barcode.ProductType
                                         into grouping
                                    select grouping;

            return productTypeGroups.OrderByDescending(g => g.Count()).First();
        }

        /// <summary>
        /// Get all <see cref="ProductSubtypeValue">Product Subtypes</see> for a given <see cref="ProductTypeValue">Product Type</see>
        /// for the purchase being aggregated.
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        public List<ProductSubtypeValue> GetProductSubtypes(ProductTypeValue productType)
        {
            return (from barcode in _purchase.Barcodes
                    where barcode.ProductType == productType
                    select barcode.ProductSubtype).Distinct().ToList();
        }
    }
}
