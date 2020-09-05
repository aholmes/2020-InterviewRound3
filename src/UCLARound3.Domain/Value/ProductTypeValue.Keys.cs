using System;
using System.Collections.Generic;
using System.Linq;

namespace UCLARound3.Domain.Value
{
    /// <summary>
    /// A set of all valid product keys for <see cref="ProductTypeValue"/>.
    /// </summary>
    public static class ProductTypeValueKeys
    {
        private static BkTree KeysTree = new BkTree();

        static ProductTypeValueKeys()
        {
            var prePopulatedKeys = new[]
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

            foreach(var key in prePopulatedKeys)
            {
                KeysTree.Add(key);
            }
        }

        /// <summary>
        /// Check the Product Type dictionary for the key that most closely matches the `value` parameter.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetValidProductTypeKey(string value)
        {
            var result = KeysTree.Search(value, 1);
            KeysTree.Add(value);
            if(!result.Any()) throw new ArgumentException($"The value '{value}' is not one of the valid Product Type keys in `{nameof(ProductTypeValue)}.{nameof(KeysTree)}`.", nameof(value));

            return result.First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="distanceTolerance"></param>
        /// <returns></returns>
        public static List<string> SearchValidProductTypeKey(string value, int distanceTolerance = 1)
            => KeysTree.Search(value, distanceTolerance);

        /// <summary>
        /// Add a new 4-character product type.
        /// </summary>
        /// <param name="productType"></param>
        public static void AddKey(string productType) => KeysTree.Add(productType);

        /// <summary>
        /// Add a new 4-character product type.
        /// </summary>
        /// <param name="productType"></param>
        public static void AddKey(ProductTypeValue productType) => KeysTree.Add(productType);
    }
}
