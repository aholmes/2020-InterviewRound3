using InterviewRound3.Domain.Aggregate;
using InterviewRound3.Domain.Entity;
using InterviewRound3.Domain.Value;
using System;

namespace InterviewRound3.Writer
{
    public interface IVisitor<T>
    {
        public void Visit(T dispatcher);
    }

    /// <summary>
    /// A <see cref="ConsoleWriter"/> visitor.
    /// </summary>
    public class ConsoleWritingVisitor: IVisitor<ConsoleWriter>
    {
        private IConsole _console;

        /// <summary>
        /// Get a new instance.
        /// </summary>
        /// <param name="console"></param>
        public ConsoleWritingVisitor(IConsole console)
        {
            if(console == null)
                throw new ArgumentNullException(nameof(console));

            _console = console;
        }

        /// <summary>
        /// Write the output of the <see cref="ConsoleWriter"/> to the <see cref="IConsole.WriteLine(string)"/> method.
        /// </summary>
        /// <param name="writer"></param>
        public void Visit(ConsoleWriter writer)
        {
            if(writer == null)
                throw new ArgumentNullException(nameof(writer));

            _console.WriteLine(writer.GetOutput());
        }
    }

    /// <summary>
    /// Base class for <see cref="ConsoleWritingVisitor"/> dispatchers.
    /// </summary>
    public abstract class ConsoleWriter
    {
        /// <summary>
        /// Dispatch to the visitor.
        /// </summary>
        /// <param name="visitor"></param>
        public virtual void Accept(IVisitor<ConsoleWriter> visitor)
        {
            if(visitor == null)
                throw new ArgumentNullException(nameof(visitor));

            visitor.Visit(this);
        }

        /// <summary>
        /// The action performed by the visitor.
        /// </summary>
        /// <returns></returns>
        public abstract string GetOutput();
    }

    /// <summary>
    /// Summarize a purchase.
    /// </summary>
    public class PurchaseSummary: ConsoleWriter
    {
        private readonly PurchaseEntity _purchaseEntity;

        /// <summary>
        /// Get a new instance.
        /// </summary>
        /// <param name="purchaseEntity"></param>
        public PurchaseSummary(PurchaseEntity purchaseEntity)
        {
            if(purchaseEntity == null)
                throw new ArgumentNullException(nameof(purchaseEntity));

            _purchaseEntity = purchaseEntity;
        }

        /// <summary>
        /// Summarizes the purchase name, time, and number of items purchased.
        /// </summary>
        /// <returns></returns>
        public override string GetOutput()
            => $@"a) Customer: {_purchaseEntity.Customer}
b) Date: {_purchaseEntity.Timestamp}
c) Total Items Purchased: {_purchaseEntity.Barcodes.Count}";
    }

    /// <summary>
    /// Details a purchase.
    /// </summary>
    public class PurchaseDetail: ConsoleWriter
    {
        private readonly PurchaseAggregate _purchaseAggregate;

        /// <summary>
        /// Get a new instance.
        /// </summary>
        /// <param name="purchaseAggregate"></param>
        public PurchaseDetail(PurchaseAggregate purchaseAggregate)
        {
            if(purchaseAggregate == null)
                throw new ArgumentNullException(nameof(purchaseAggregate));

            _purchaseAggregate = purchaseAggregate;
        }

        /// <summary>
        /// Details information about the items that were purchased.
        /// </summary>
        /// <returns></returns>
        public override string GetOutput()
        {
            var uniqueIds = _purchaseAggregate.GetUniqueIds();
            var commonProductsByType = _purchaseAggregate.GetMostCommonProductByType();
            return $@"a) The number of unique items purchased: {uniqueIds.Count}
    The unique IDs that were purchased:
    {string.Join("\n    ", uniqueIds)}
b) The most common product type purchased: {commonProductsByType.Key}";
        }
    }

    /// <summary>
    /// Details products.
    /// </summary>
    public class ProductDetail: ConsoleWriter
    {
        private readonly PurchaseAggregate _purchaseAggregate;
        private readonly ProductTypeValue _productType;

        /// <summary>
        /// Get a new instance.
        /// </summary>
        /// <param name="purchaseAggregate"></param>
        public ProductDetail(PurchaseAggregate purchaseAggregate)
        {
            if(purchaseAggregate == null)
                throw new ArgumentNullException(nameof(purchaseAggregate));

            _purchaseAggregate = purchaseAggregate;
        }

        public ProductDetail(PurchaseAggregate purchaseAggregate, ProductTypeValue productType)
            : this(purchaseAggregate)
        {
            _productType = productType;
        }

        /// <summary>
        /// Details the subtypes for the most common product type purchased.
        /// </summary>
        /// <returns></returns>
        public override string GetOutput()
        {
            var productType = _productType ?? _purchaseAggregate.GetMostCommonProductByType().Key;

            return $@"a) Subtypes for product type {productType}:
    {string.Join("\n    ", _purchaseAggregate.GetProductSubtypes(productType))}";
        }
    }
}
