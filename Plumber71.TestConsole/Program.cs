using Plumber71.Core.Controller;
using Plumber71.Core.Model;
using Plumber71.Core.Service.EmailExcelProvider;
using Plumber71.Core.Service.JsonFileService;
using Plumber71.Core.Service.PricelisDataSetParser.Model;
using Plumber71.Core.Service.PricelistComparer;
using Plumber71.Core.Service.Woocomerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plumber71.TestConsole
{
    class Program
    {
        private static string originalFileName = $"{Environment.CurrentDirectory}/Plumber/price_d5.xls";
        static void Main(string[] args)
        {
            var start = DateTime.Now;
            Console.WriteLine($"{start:dd-MM-yyyy hh:mm:ss}| Start price updating");
            var count = UpdatePricesFromExcel();
            var end = DateTime.Now;
            var span = end - start;
            Console.WriteLine($"{end:dd-MM-yyyy hh:mm:ss}| Updated {count} prices after {span.TotalSeconds:0.00} seconds");
            Task.Delay(3000).GetAwaiter().GetResult();
        }

        static int UpdatePricesFromExcel()
        {
            string path = EmailExcelProvider.GetLastPriceList();
            PlumberProductController plumberProductController = new PlumberProductController(WooClient.DefaultClient());
            return plumberProductController.UpdatePricesFromExcel(originalFileName).GetAwaiter().GetResult();
        }
    }
}
