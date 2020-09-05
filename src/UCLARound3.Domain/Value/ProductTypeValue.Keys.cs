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

        /// <summary>
        /// The valid keys for all <see cref="ProductTypeValue"/>.
        /// </summary>
        //public static readonly HashSet<ProductTypeValue> Keys = new HashSet<ProductTypeValue>
        public static readonly HashSet<string> Keys = new HashSet<string>
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

        static ProductTypeValueKeys()
        {
            foreach(var key in Keys)
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
            if(Keys.Contains(value)) return value;

            var result = KeysTree.Search(value, 1);
            if(!result.Any()) throw new ArgumentException($"The value '{value}' is not one of the valid Product Type keys in `{nameof(ProductTypeValue)}.{nameof(Keys)}`.", nameof(value));

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
        public static void AddKey(string productType) => Keys.Add(productType);

        /// <summary>
        /// Add a new 4-character product type.
        /// </summary>
        /// <param name="productType"></param>
        public static void AddKey(ProductTypeValue productType) => Keys.Add(productType);
    }
}
