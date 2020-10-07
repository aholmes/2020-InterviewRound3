using InterviewRound3.Domain.Aggregate;
using InterviewRound3.Domain.Entity;
using InterviewRound3.Domain.Value;
using System;
using System.Linq;
using InterviewRound3.Domain;
using Xunit;

using static InterviewRound3.UnitTests.Helpers.SampleDataGenerator;

namespace InterviewRound3.UnitTests.Domain.Aggregate
{
    public class PurchaseAggregateTests
    {
        [Fact]
        public void PurchaseAggregate_Throws_On_Null_Input()
        {
            #region Arrange/Act
            void create() => new PurchaseAggregate(null);
            #endregion

            #region Assert
            Assert.Throws<ArgumentNullException>(create);
            #endregion
        }

        [Fact]
        public void GetUniqueIds_Returns_Only_Unique_PurchaseEntity_Ids()
        {
            #region Arrange
            var purchaseAggregate = new PurchaseAggregate(SamplePurchaseEntity);
            #endregion

            #region Act
            var uniqueIds = purchaseAggregate.GetUniqueIds();
            #endregion

            #region Assert
            Assert.Equal(uniqueIds.Distinct().Count(), uniqueIds.Count);
            #endregion
        }

        [Fact]
        public void GetMostCommonProductByType_Returns_Most_Common_Product_Type()
        {
            #region Arrange
            var purchaseEntity = new PurchaseEntity(
                timestamp: SamplePurchaseEntity.Timestamp,
                customer: SamplePurchaseEntity.Customer,
                barcodes: SamplePurchaseEntity.Barcodes.GetRange(0, SamplePurchaseEntity.Barcodes.Count - 1)
            );
            var purchaseAggregate = new PurchaseAggregate(purchaseEntity);
            #endregion

            #region Act
            var mostCommonProduct = purchaseAggregate.GetMostCommonProductByType();
            #endregion

            #region Assert
            Assert.Equal("BEVG", mostCommonProduct.Key);
            #endregion
        }

        [Theory]
        [InlineData("BEVG", "TTKYGD|ZYUFGN")]
        [InlineData("CANF", "DNSKAV")]
        [InlineData("FRZN", "QQNPSE")]

        public void GetProductSubTypes_Returns_All_Product_Subtypes_For_Product_Type(string productType, string formattedSubtypesTestData)
        {
            #region Arrange
            // string[] and List<ProductSubtypeValue> are invariant
            // and must be explicity cast for the assertion below
            var subtypesTestData = formattedSubtypesTestData.Split('|').Select(o => (ProductSubtypeValue)o);
            var purchaseAggregate = new PurchaseAggregate(SamplePurchaseEntity);
            #endregion

            #region Act
            var subtypes = purchaseAggregate.GetProductSubtypes(productType);
            #endregion
            #region Assert
            Assert.Equal(subtypesTestData, subtypes);
            #endregion
        }
    }
}
