using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCLARound3.Domain;
using UCLARound3.Domain.Aggregate;
using UCLARound3.Domain.Entity;
using UCLARound3.Domain.Value;
using Xunit;

using static UCLARound3.UnitTests.Helpers.SampleDataGenerator;

namespace UCLARound3.UnitTests.Domain.Aggregate
{
    public class PurchaseAggregateTests
    {
        [Fact]
        public void PurchaseAggregate_Throws_On_Null_Input()
        {
            #region Arrange/Act
            PurchaseAggregate create () => new PurchaseAggregate(null);
            #endregion

            #region Assert
            Assert.Throws<ArgumentNullException>(create);
            #endregion
        }

        [Fact]
        public void GetUniqueIds_Throws_When_Purchases_Is_Null()
        {
            #region Arrange
            var purchaseEntity = new PurchaseEntity(DateTime.Now, SampleFileDataHeaderCustomer, null);
            var purchaseAggregate = new PurchaseAggregate(purchaseEntity);
            #endregion

            #region Act
            List<string> get() => purchaseAggregate.GetUniqueIds();
            #endregion

            #region Assert
            Assert.Throws<InvalidOperationException>(get);
            #endregion
        }

        [Fact]
        public void GetUniqueIds_Throws_When_No_Purchases()
        {
            #region Arrange
            var purchaseEntity = new PurchaseEntity(DateTime.Now, SampleFileDataHeaderCustomer, new BarcodeValue[0]);
            var purchaseAggregate = new PurchaseAggregate(purchaseEntity);
            #endregion

            #region Act
            List<string> get() => purchaseAggregate.GetUniqueIds();
            #endregion

            #region Assert
            Assert.Throws<InvalidOperationException>(get);
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
        public void GetMostCommonProductType_Returns_Most_Common_Product_Type()
        {
            #region Arrange
            var purchaseAggregate = new PurchaseAggregate(SamplePurchaseEntity);
            #endregion

            #region Act
            var product = purchaseAggregate.GetMostCommonProductByType();
            #endregion

            #region Assert
            Assert.Equal("CANF", product.ProductType);
            #endregion
        }

        [Theory]
        [InlineData("BEVG", "TTKYGD|ZYUFGN")]
        [InlineData("CANF", "DNSKAV")]
        [InlineData("FRZN", "QQNPSE")]

        public void GetProductSubTypes_Returns_All_Product_Subtypes_For_Product_Type(string productType, string formattedSubtypesTestData)
        {
            #region Arrange
            var subtypesTestData = formattedSubtypesTestData.Split('|'); 
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
