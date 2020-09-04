using System.Collections.Generic;

namespace UCLARound3.Domain
{
    public static class ProductKeys
    {
        public static readonly HashSet<ProductTypeValue> Keys = new HashSet<ProductTypeValue>
        {
            "BEVG",
            "BAKE",
            "CANF",
            "CNSB",
            "SNCN",
            "DREG",
            "FRZN",
            "FRVG",
            "GRPA",
            "MTSF",
            "MISC"
        };

        public static void AddKey(string productType) => Keys.Add(productType);
        public static void AddKey(ProductTypeValue productType) => Keys.Add(productType);
    }
}
