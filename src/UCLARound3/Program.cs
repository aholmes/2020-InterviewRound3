using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UCLARound3.Domain;
using UCLARound3.Domain.Aggregate;
using UCLARound3.Domain.Entity;
using UCLARound3.Domain.Value;
using UCLARound3.Writer;

namespace UCLARound3
{
    class Program
    {
        private static IConsole _console = new ConsoleWrapper();
        static async Task Main(string[] args)
        {
            try
            {
                var purchaseFilename = args.Length > 0 ? args[0] : "CustomerG.txt";

                PurchaseAggregate purchaseAggregate;
                PurchaseEntity purchaseEntity;
                using (var stream = GetOpenFileStream(purchaseFilename))
                {
                    purchaseEntity = await PurchaseEntity.CreateFromStream(stream);
                    purchaseAggregate = new PurchaseAggregate(purchaseEntity);
                }

                var consoleWritingVisitor = new ConsoleWritingVisitor(_console);

                var writers = new ConsoleWriter[]
                {
                    new PurchaseSummary(purchaseEntity),
                    new PurchaseDetail(purchaseAggregate),
                    new ProductDetail(purchaseAggregate)
                };

                foreach(var writer in writers)
                {
                    writer.Accept(consoleWritingVisitor);
                    _console.WriteLine();
                }
            }
            catch(Exception e)
            {
#if DEBUG
                throw;
#else
                string serializeException(Exception e, string accumulator)
                {
                    var exceptionData = (e.Message, e.StackTrace);
                    var serializer = new XmlSerializer(exceptionData.GetType());
                    using(var sw = new StringWriter())
                    {
                        serializer.Serialize(sw, exceptionData);
                        accumulator += sw.ToString() + "\n\n";
                    }

                    if (e.InnerException != null) return serializeException(e.InnerException, accumulator);

                    return accumulator;
                };

                Console.WriteLine($"\nSomething bad happened: {e.Message}");
                Console.WriteLine("\nDebug output follows.\n\n");

                Console.WriteLine(serializeException(e, ""));
#endif
            }

            Console.WriteLine("\n\nPress enter to exit.");
            Console.ReadLine();
        }

        static Stream GetOpenFileStream(string filename)
        {
            if (filename == null) throw new ArgumentNullException(nameof(filename));

#if READ_FILE_FROM_DISK
            if (!File.Exists(filename)) throw new FileNotFoundException("File not found on disk.", filename);

            return File.OpenRead(filename);
#else
            // This would ideally return a file stream,
            // but I embedded the file for portability
            var fullFilename = $"{nameof(UCLARound3)}.{filename}";
            var assembly = Assembly.GetEntryAssembly();

            if (!assembly.GetManifestResourceNames().Contains(fullFilename)) throw new FileNotFoundException("File not found in embedded resources.", fullFilename);

            return assembly.GetManifestResourceStream(fullFilename);
#endif
        }
    }
}
