using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UCLARound3.Domain.Aggregate;
using UCLARound3.Domain.Entity;
using UCLARound3.Domain.Value;
using UCLARound3.Writer;
#if !DEBUG
using System.Xml.Serialization;
#endif

namespace UCLARound3
{
    class Program
    {
        private static IConsole _console = new ConsoleWrapper();
        private static IVisitor<ConsoleWriter> _visitor = new ConsoleWritingVisitor(_console);

        static async Task Main(string[] args)
        {
            try
            {
                var purchaseFilename = args.Length > 0 ? args[0] : "CustomerG.txt";

                var (purchaseAggregate, purchaseEntity)
                    = await GetPurchaseInformation(purchaseFilename);

                _console.WriteLine("Question 1 solution:\n");
                WriteInformation(purchaseEntity);
                _console.WriteLine();

                _console.WriteLine("Question 2 part 1 solution:\n");
                WriteInformation(purchaseAggregate);
                _console.WriteLine();


                _console.WriteLine("Question 2 part 2 solution:\n");
                bool exit = false;
                bool skip = false;
                while(!exit)
                {
                    _console.WriteLine("Input a 4-character Product Type to list Subtypes in this Purchase.");
                    _console.WriteLine("Press Enter to stop searching.");
                    _console.Write(" > ");

                    const int ProductTypeStringLength = 4;
                    char nextChar;
                    var searchProductType = new char[ProductTypeStringLength];
                    var searchProductTypeIndex = 0;
                    List<string> matches = null;
                    do
                    {
                        nextChar = _console.ReadKey().KeyChar;
                        if(nextChar == 10 || nextChar == 13)
                        {
                            exit = true;
                            break;
                        }
                        searchProductType[searchProductTypeIndex] = char.ToUpper(nextChar);

                        matches = ProductTypeValueKeys.SearchValidProductTypeKey(new string(searchProductType), searchProductType.Length - searchProductTypeIndex);
                        if(!matches.Any())
                        {
                            _console.WriteLine($"\rNo Product Types match '{new string(searchProductType)}'");
                            nextChar = '\0';
                            searchProductType = new char[ProductTypeStringLength];
                            searchProductTypeIndex = 0;
                            matches = null;
                            skip = true;
                            break;
                        }

                        _console.WriteLine("\r - Possible matches: ");
                        foreach(var match in matches)
                        {
                            _console.WriteLine("\t" + match);
                        }
                        _console.Write($" > {new string(searchProductType)}");
                    } while(++searchProductTypeIndex < searchProductType.Length);

                    if(exit) break;
                    if(skip)
                    {
                        skip = false;
                        continue;
                    }

                    _console.WriteLine();
                    _console.WriteLine($"\nSearching for best match '{matches[0]}'\n");

                    WriteInformation(purchaseAggregate, matches[0]);

                    _console.WriteLine();
                };
            }
#if DEBUG
            catch(Exception)
            {
                throw;
#else
            catch(Exception e)
            {
                static string serializeException(Exception e, string accumulator)
                {
                    var exceptionData = (e.Message, e.StackTrace);
                    var serializer = new XmlSerializer(exceptionData.GetType());
                    using(var sw = new StringWriter())
                    {
                        serializer.Serialize(sw, exceptionData);
                        accumulator += sw.ToString() + "\n\n";
                    }

                    if(e.InnerException != null)
                        return serializeException(e.InnerException, accumulator);

                    return accumulator;
                };

                _console.WriteLine($"\nSomething bad happened: {e.Message}");
                _console.WriteLine("\nDebug output follows.\n\n");

                _console.WriteLine(serializeException(e, ""));
#endif
            }

            _console.WriteLine("\n\nPress enter to exit.");
            _console.ReadLine();
        }

        private static async Task<(PurchaseAggregate, PurchaseEntity)> GetPurchaseInformation(string filename)
        {
            using var stream = GetOpenFileStream(filename);
            var purchaseEntity = await PurchaseEntity.CreateFromStream(stream);
            var purchaseAggregate = new PurchaseAggregate(purchaseEntity);
            return (purchaseAggregate, purchaseEntity);
        }

        private static void WriteInformation(PurchaseEntity purchaseEntity)
            => new PurchaseSummary(purchaseEntity)
                .Accept(_visitor);

        private static void WriteInformation(PurchaseAggregate purchaseAggregate)
            => new PurchaseDetail(purchaseAggregate)
                .Accept(_visitor);

        private static void WriteInformation(PurchaseAggregate purchaseAggregate, ProductTypeValue productType)
            => new ProductDetail(purchaseAggregate, productType)
                .Accept(_visitor);

        /// <summary>
        /// Open the given filename and return a <see cref="Stream"/> for reading.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static Stream GetOpenFileStream(string filename)
        {
            if(filename == null)
                throw new ArgumentNullException(nameof(filename));

#if READ_FILE_FROM_DISK
            if (!File.Exists(filename)) throw new FileNotFoundException("File not found on disk.", filename);

            return File.OpenRead(filename);
#else
            // This would ideally return a file stream,
            // but I embedded the file for portability
            var fullFilename = $"{nameof(UCLARound3)}.{filename}";
            var assembly = Assembly.GetEntryAssembly();

            if(!assembly.GetManifestResourceNames().Contains(fullFilename))
                throw new FileNotFoundException("File not found in embedded resources.", fullFilename);

            return assembly.GetManifestResourceStream(fullFilename);
#endif
        }
    }
}
