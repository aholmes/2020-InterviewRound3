using System.Collections.Generic;

namespace UCLARound3.Domain
{
    public class ProductKeys
    {
        public readonly List<string> Keys = new List<string>
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

        public void AddKey(string productType) => Keys.Add(productType);
    }
}
