using System;
using System.IO;
using System.Threading.Tasks;
using UCLARound3.Domain.Aggregate;
using UCLARound3.Domain.Entity;

namespace UCLARound3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            PurchaseAggregate purchaseAggregate;
            PurchaseEntity purchaseEntity;
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms))
            {
                await sw.WriteAsync(@"01232020Jamie
BEVGTTKYGDGJHGTFBNGDVZJGDIPXVS
CANFDNSKAVOUXSCGSYBHQYHNMDQOBL
FRZNQQNPSESCHIMIXOUHNAWLXRZEPT
BEVGZYUFGNIHDCZIPWLZJLPDSGNEAH");
                await sw.FlushAsync();
                ms.Position = 0;
                purchaseEntity = await PurchaseEntity.CreateFromStream(ms);
                purchaseAggregate = new PurchaseAggregate(purchaseEntity);
            }

            var consoleWritingVisitor = new ConsoleWritingVisitor();

            var purchaseSummary = new PurchaseSummary(purchaseEntity);
            var purchaseDetail = new PurchaseDetail(purchaseAggregate);
            var productDetail = new ProductDetail(purchaseAggregate);

            purchaseSummary.Accept(consoleWritingVisitor);
            purchaseDetail.Accept(consoleWritingVisitor);
            productDetail.Accept(consoleWritingVisitor);
        }
    }
}
