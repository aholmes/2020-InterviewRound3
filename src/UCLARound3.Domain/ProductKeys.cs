using System.Collections.Generic;

namespace UCLARound3.Domain
{
    /// <summary>
    /// A set of all valid product keys for <see cref="ProductTypeValue"/>.
    /// </summary>
    public static class ProductKeys
    {
        /// <summary>
        /// The valid keys for all <see cref="ProductTypeValue"/>.
        /// </summary>
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

        // TODO add validation
        /// <summary>
        /// Add a new 4-character product type.
        /// </summary>
        /// <param name="productType"></param>
        public static void AddKey(string productType) => Keys.Add(productType);

        /// <summary>
        /// Add a new 4-character product type.
        /// </summary>
        /// <param name="productType"></param>
        public static void AddKey(ProductTypeValue productType) => Keys.Add(productType);
    }
}
