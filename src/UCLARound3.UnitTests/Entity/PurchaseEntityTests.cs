using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCLARound3.Domain.Entity;
using UCLARound3.Domain.Value;
using UCLARound3.UnitTests.Helpers;
using Xunit;

namespace UCLARound3.UnitTests.Entity
{
    public class PurchaseEntityTests
    {
        private const string SampleFileData = @"01232020Jamie
BEVGTTKYGDGJHGTFBNGDVZJGDIPXVS
CANFDNSKAVOUXSCGSYBHQYHNMDQOBL
FRZNQQNPSESCHIMIXOUHNAWLXRZEPT
BEVGZYUFGNIHDCZIPWLZJLPDSGNEAH
CDNFDNSKAVOUXSCGSYBHQYHNMDQOBL"; // this last one has an invalid product type - it should be CANF

        private readonly PurchaseEntity _samplePurchaseEntity = new PurchaseEntity
        {
            Timestamp = new DateTime(2020, 1, 23),
            Customer = "Jamie",
            Barcodes = new List<BarcodeValue>
            {
                new BarcodeValue
                {
                    ProductType = "BEVG",
                    ProductSubtype = "TTKYGD",
                    Id = "GJHGTFBNGDVZJGDIPXVS"
                },
                new BarcodeValue
                {
                    ProductType = "CANF",
                    ProductSubtype = "DNSKAV",
                    Id = "OUXSCGSYBHQYHNMDQOBL"
                },
                new BarcodeValue
                {
                    ProductType = "FRZN",
                    ProductSubtype = "QQNPSE",
                    Id = "SCHIMIXOUHNAWLXRZEPT"
                },
                new BarcodeValue
                {
                    ProductType = "BEVG",
                    ProductSubtype = "ZYUFGN",
                    Id = "IHDCZIPWLZJLPDSGNEAH"
                },
                // this is the corrected value from the corrupt line
                new BarcodeValue
                {
                    ProductType = "CANF",
                    ProductSubtype = "DNSKAV",
                    Id = "OUXSCGSYBHQYHNMDQOBL"
                }
            }
        };

        [Fact]
        public async Task CreateFromStream_Disallows_Null_Input()
        {
            #region Arrange/Act
            async Task create() => await PurchaseEntity.CreateFromStream(null);
            #endregion

            #region Assert
            await Assert.ThrowsAsync<ArgumentNullException>(create);
            #endregion
        }

        [Fact]
        public async Task CreateFromStream_Disallows_Empty_Input()
        {
            using (var ms = new MemoryStream())
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
        public async Task CreateFromStream_Rejects_Invalid_Data()
        {
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms))
            {
                #region Arrange
                await sw.WriteAsync(SampleFileData.Substring(0, 50));
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
        public async Task CreateFromStream_Sets_All_Properties_Correctly_When_Data_Are_Uncorrupt()
        {
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms))
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
                Assert.Equal(_samplePurchaseEntity.Timestamp, purchaseEntity.Timestamp);
                Assert.Equal(_samplePurchaseEntity.Customer, purchaseEntity.Customer);
                Assert.Equal(_samplePurchaseEntity.Barcodes.SkipLast(1), purchaseEntity.Barcodes, new BarcodeValueEqualityComparer());
                #endregion
            }
        }

        [Fact]
        public async Task CreateFromStream_Sets_All_Properties_Correctly_When_Some_Data_Are_Corrupt()
        {
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms))
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
                Assert.Equal(_samplePurchaseEntity.Timestamp, purchaseEntity.Timestamp);
                Assert.Equal(_samplePurchaseEntity.Customer, purchaseEntity.Customer);
                Assert.Equal(_samplePurchaseEntity.Barcodes, purchaseEntity.Barcodes, new BarcodeValueEqualityComparer());
                #endregion
            }
        }
    }
}
