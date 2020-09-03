using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UCLARound3.Domain.Value;

[assembly: InternalsVisibleTo("UCLARound3.UnitTests")]
namespace UCLARound3.Domain.Entity
{
    public class PurchaseEntity
    {
        public DateTime Timestamp { get; private set; }
        public string Customer { get; private set; }
        private List<BarcodeValue> _barcodes;
        public List<BarcodeValue> Barcodes
        {
            get => _barcodes;
            private set
            {
                if (!value.Any()) throw new InvalidDataException("The purchase data does not contain any purchased products.");
                _barcodes = value;
            }
        }

        internal PurchaseEntity() { }

        internal PurchaseEntity(DateTime timestamp, string customer, IEnumerable<BarcodeValue> barcodes)
        {
            Timestamp = timestamp;
            Customer = customer;
            Barcodes = barcodes.ToList();
        }

        public static async Task<PurchaseEntity> CreateFromStream(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            if (stream.Length == 0) throw new InvalidDataException("The stream cannot be empty.");

            try
            {
                using (var sr = new StreamReader(stream))
                {
                    var (timestamp, customer) = await ParseHeader(sr);
                    var products = await ParseProducts(sr);

                    return new PurchaseEntity
                    {
                        Timestamp = timestamp,
                        Customer = customer,
                        Barcodes = products
                    };
                }
            }
            catch(Exception e)
            {
                throw new InvalidDataException("Error processing input data.", e);
            }
        }

        private static async Task<(DateTime timestamp, string customer)> ParseHeader(StreamReader sr)
        {
            var buffer = new char[8];
            await sr.ReadAsync(buffer, 0, buffer.Length);
            var timestamp = DateTime.ParseExact(buffer, "MMddyyyy", CultureInfo.InvariantCulture);

            // the remainder of the line is the customer's name
            var customer = await sr.ReadLineAsync();

            return (timestamp, customer);
        }

        private static async Task<List<BarcodeValue>> ParseProducts(StreamReader sr)
        {
            var products = new List<BarcodeValue>();
            await foreach(var (type, subtype, id) in ReadProductsFromStream(sr))
            {
                products.Add(new BarcodeValue
                {
                    ProductType = type,
                    ProductSubtype = subtype,
                    Id = id
                });
            }

            return products;
        }

        private static async IAsyncEnumerable<(string type, string subtype, string id)> ReadProductsFromStream(StreamReader sr)
        {
            while(!sr.EndOfStream)
            {
                var line = await sr.ReadLineAsync();
                var type = line.Substring(0, 4);
                var subtype = line.Substring(4, 6);
                var id = line.Substring(10);
                yield return (type, subtype, id);
            }
        }
    }
}
