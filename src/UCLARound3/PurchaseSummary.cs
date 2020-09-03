using System;
using System.Collections.Generic;
using System.Text;
using UCLARound3.Domain.Aggregate;
using UCLARound3.Domain.Entity;

namespace UCLARound3
{
    public class ConsoleWritingVisitor
    {
    
        public void WriteLine(ConsoleWriter writer)
            => Console.WriteLine(writer.GetOutput() + "\n");
    }

    public abstract class ConsoleWriter
    {
        public virtual void Accept(ConsoleWritingVisitor visitor)
            => visitor.WriteLine(this);
        public abstract string GetOutput();
    }

    public class PurchaseSummary : ConsoleWriter
    {
        private readonly PurchaseEntity _purchaseEntity;

        public PurchaseSummary(PurchaseEntity purchaseEntity)
        {
            _purchaseEntity = purchaseEntity;
        }

        public override string GetOutput()
            => $@"Customer: {_purchaseEntity.Customer}
Date: {_purchaseEntity.Timestamp}
Total Items Purchased: {_purchaseEntity.Barcodes.Count}";
    }

    public class PurchaseDetail : ConsoleWriter
    {
        private readonly PurchaseAggregate _purchaseAggregate;

        public PurchaseDetail(PurchaseAggregate purchaseAggregate)
        {
            _purchaseAggregate = purchaseAggregate;
        }

        public override string GetOutput()
        {
            var uniqueIds = _purchaseAggregate.GetUniqueIds();
            var commonProductsByType = _purchaseAggregate.GetMostCommonProductByType();
            return $@"The number of unique items purchased: {uniqueIds.Count}
The unique IDs that were purchased:
{"\t" + string.Join("\n\t", uniqueIds)}
The most common product type purchased: {commonProductsByType.Key}";
        }
    }

    public class ProductDetail : ConsoleWriter
    {
        private readonly PurchaseAggregate _purchaseAggregate;

        public ProductDetail(PurchaseAggregate purchaseAggregate)
        {
            _purchaseAggregate = purchaseAggregate;
        }

        public override string GetOutput()
        {
            var commonProductsByType = _purchaseAggregate.GetMostCommonProductByType();
            return $@"Subtypes for product type {commonProductsByType.Key}:
{"\t"+string.Join("\n\t", _purchaseAggregate.GetProductSubtypes(commonProductsByType.Key))}";
        }
    }
}
