using InterviewRound3.Domain.Entity;
using InterviewRound3.Domain.Value;
using System;
using System.Collections.Generic;

namespace InterviewRound3.UnitTests.Helpers
{
    public static class SampleDataGenerator
    {
        public const string SampleFileDataHeaderTimestamp = "01232020";
        public const string SampleFileDataHeaderCustomer = "Jamie";
        public const string SampleFileDataHeader = SampleFileDataHeaderTimestamp + SampleFileDataHeaderCustomer;

        public const string SampleFileData = SampleFileDataHeader + @"
BEVGTTKYGDGJHGTFBNGDVZJGDIPXVS
CANFDNSKAVOUXSCGSYBHQYHNMDQOBL
FRZNQQNPSESCHIMIXOUHNAWLXRZEPT
BEVGZYUFGNIHDCZIPWLZJLPDSGNEAH
CDNFDNSKAVOUXSCGSYBHQYHNMDQOBL"; // this last one has an invalid product type - it should be CANF

        public static readonly PurchaseEntity SamplePurchaseEntity = new PurchaseEntity(
            timestamp: new DateTime(2020, 1, 23),
            customer: "Jamie",
            barcodes: new List<BarcodeValue>
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
            });
    }
}
