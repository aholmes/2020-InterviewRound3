using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UCLARound3.Domain.Value;

[assembly: InternalsVisibleTo("UCLARound3.UnitTests")]
namespace UCLARound3.Domain.Entity
{
    /// <summary>
    /// A record of a purchase containing the time of purchase,
    /// who made the purchase, and the product barcodes in that purchase.
    /// </summary>
    public class PurchaseEntity
    {
        /// <summary>
        /// The time the purchase was made.
        /// </summary>
        public TimestampValue Timestamp { get; private set; }

        /// <summary>
        /// The customer who made the purchase.
        /// </summary>
        public CustomerValue Customer { get; private set; }

        private List<BarcodeValue> _barcodes;
        /// <summary>
        /// The barcode information for each product purchased.
        /// </summary>
        public List<BarcodeValue> Barcodes
        {
            get => _barcodes;
            private set
            {
                if(!value.Any())
                    throw new InvalidDataException("The purchase data does not contain any purchased products.");
                _barcodes = value;
            }
        }

        /// <summary>
        /// Create an uninitialized instance
        /// </summary>
        internal PurchaseEntity() { }

        /// <summary>
        /// Create an instance with the parameterized values
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="customer"></param>
        /// <param name="barcodes"></param>
        internal PurchaseEntity(TimestampValue timestamp, CustomerValue customer, IEnumerable<BarcodeValue> barcodes)
        {
            Timestamp = timestamp;
            Customer = customer;
            Barcodes = barcodes.ToList();
        }

        /// <summary>
        /// Get a new instance from data parsed out of the given <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task<PurchaseEntity> CreateFromStream(Stream stream)
        {
            if(stream == null)
                throw new ArgumentNullException(nameof(stream));

            if(stream.Length == 0)
                throw new InvalidDataException("The stream cannot be empty.");

            try
            {
                using(var sr = new StreamReader(stream))
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

        /// <summary>
        /// Parse the timestamp and customer name from the stream.
        /// This must be called before <see cref="ParseProducts(StreamReader)"/>.
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        private static async Task<(TimestampValue timestamp, CustomerValue customer)> ParseHeader(StreamReader sr)
        {
            var buffer = new char[8];
            await sr.ReadAsync(buffer, 0, buffer.Length);
            var timestamp = DateTime.ParseExact(buffer, "MMddyyyy", CultureInfo.InvariantCulture);

            // the remainder of the line is the customer's name
            var customer = await sr.ReadLineAsync();

            return (timestamp, customer);
        }

        /// <summary>
        /// Parse each barcode from the stream.
        /// This must be called after <see cref="ParseHeader(StreamReader)"/>.
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Read and return one barcode from the stream at a time.
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        private static async IAsyncEnumerable<(ProductTypeValue type, ProductSubtypeValue subtype, ProductIdValue id)> ReadProductsFromStream(StreamReader sr)
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
