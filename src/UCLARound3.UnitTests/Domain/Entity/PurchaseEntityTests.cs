using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UCLARound3.Domain.Entity;
using UCLARound3.Domain.Value;
using UCLARound3.UnitTests.Helpers;
using Xunit;

using static UCLARound3.UnitTests.Helpers.SampleDataGenerator;

namespace UCLARound3.UnitTests.Domain.Entity
{
    public class PurchaseEntityTests
    {
        [Fact]
        public async Task CreateFromStream_Throws_On_Null_Input()
        {
            #region Arrange/Act
            async Task create() => await PurchaseEntity.CreateFromStream(null);
            #endregion

            #region Assert
            await Assert.ThrowsAsync<ArgumentNullException>(create);
            #endregion
        }

        [Fact]
        public async Task CreateFromStream_Throws_On_Empty_Input()
        {
            using(var ms = new MemoryStream())
            {
                #region Arrange/Act
                async Task create() => await PurchaseEntity.CreateFromStream(ms);
                #endregion

                #region Assert
                await Assert.ThrowsAsync<InvalidDataException>(create);
                #endregion
            }
        }

        [Fact]
        public void Set_Barcodes_Throws_When_Purchases_Is_Null()
        {
            #region Arrange/Act
            void create() => new PurchaseEntity(SamplePurchaseEntity.Timestamp, SampleFileDataHeaderCustomer, null);
            #endregion

            #region Assert
            Assert.Throws<ArgumentNullException>(create);
            #endregion
        }

        [Fact]
        public void Set_Barcode_Throws_When_No_Purchases()
        {
            #region Arrange/Act
            void create() => new PurchaseEntity(SamplePurchaseEntity.Timestamp, SampleFileDataHeaderCustomer, new BarcodeValue[0]);
            #endregion

            #region Assert
            Assert.Throws<InvalidDataException>(create);
            #endregion
        }

        [Theory]
        [InlineData("")]
        [InlineData(SampleFileDataHeaderTimestamp)]
        [InlineData(SampleFileDataHeaderCustomer)]
        [InlineData(SampleFileDataHeader)]
        [InlineData(SampleFileDataHeader + "abc123")]
        public async Task CreateFromStream_Throws_On_Invalid_Data(string data)
        {
            using(var ms = new MemoryStream())
            using(var sw = new StreamWriter(ms))
            {
                #region Arrange
                await sw.WriteAsync(data);
                await sw.FlushAsync();
                ms.Position = 0;
                #endregion

                #region Act
                async Task create() => await PurchaseEntity.CreateFromStream(ms);
                #endregion

                #region Assert
                await Assert.ThrowsAsync<InvalidDataException>(create);
                #endregion
            }
        }

        [Fact]
        public async Task CreateFromStream_Throws_When_ProductType_Is_Invalid()
        {
            using(var ms = new MemoryStream())
            using(var sw = new StreamWriter(ms))
            {
                #region Arrange
                await sw.WriteAsync(SampleFileData+"\nXXXXYYYYYYZZZZZZZZZZZZZZZZZZZZ");
                await sw.FlushAsync();
                ms.Position = 0;
                #endregion

                #region Act
                async Task act() => await PurchaseEntity.CreateFromStream(ms);
                #endregion

                #region Assert
                await Assert.ThrowsAsync<InvalidDataException>(act);
                #endregion
            }
        }

        [Fact]
        public async Task CreateFromStream_Does_Not_Throw_When_ProductType_Is_Added_To_Product_Keys()
        {
            using(var ms = new MemoryStream())
            using(var sw = new StreamWriter(ms))
            {
                #region Arrange
                await sw.WriteAsync(SampleFileData+"\nXXXXYYYYYYZZZZZZZZZZZZZZZZZZZZ");
                await sw.FlushAsync();
                ms.Position = 0;

                ProductTypeValueKeys.AddKey("XXXX");
                #endregion

                #region Act
                await PurchaseEntity.CreateFromStream(ms);
                #endregion
            }
        }

        [Fact]
        public async Task CreateFromStream_Sets_All_Properties_Correctly_When_Data_Are_Uncorrupt()
        {
            using(var ms = new MemoryStream())
            using(var sw = new StreamWriter(ms))
            {
                #region Arrange
                // trim off the last corrupted entry
                await sw.WriteAsync(SampleFileData.Substring(0, SampleFileData.LastIndexOf("\n") - 1));
                await sw.FlushAsync();
                ms.Position = 0;
                #endregion

                #region Act
                var purchaseEntity = await PurchaseEntity.CreateFromStream(ms);
                #endregion

                #region Assert
                Assert.Equal(SamplePurchaseEntity.Timestamp, purchaseEntity.Timestamp);
                Assert.Equal(SamplePurchaseEntity.Customer, purchaseEntity.Customer);
                Assert.Equal(4, purchaseEntity.Barcodes.Count);
                Assert.Equal(SamplePurchaseEntity.Barcodes.SkipLast(1), purchaseEntity.Barcodes, new BarcodeValueEqualityComparer());
                #endregion
            }
        }

        [Fact]
        public async Task CreateFromStream_Sets_All_Properties_Correctly_When_Some_Data_Are_Corrupt()
        {
            using(var ms = new MemoryStream())
            using(var sw = new StreamWriter(ms))
            {
                #region Arrange
                await sw.WriteAsync(SampleFileData);
                await sw.FlushAsync();
                ms.Position = 0;
                #endregion

                #region Act
                var purchaseEntity = await PurchaseEntity.CreateFromStream(ms);
                #endregion

                #region Assert
                Assert.Equal(SamplePurchaseEntity.Timestamp, purchaseEntity.Timestamp);
                Assert.Equal(SamplePurchaseEntity.Customer, purchaseEntity.Customer);
                Assert.Equal(5, purchaseEntity.Barcodes.Count);
                Assert.Equal(SamplePurchaseEntity.Barcodes, purchaseEntity.Barcodes, new BarcodeValueEqualityComparer());
                #endregion
            }
        }
    }
}
