using Plumber71.Core.Controller;
using Plumber71.Core.Model;
using System;

namespace Plumber71.TestConsole
{
    class Program
    {
        private static string originalFileName = $"{Environment.CurrentDirectory}/Resource/price_d5.xls";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world");
            CatalogueController catalogueController = new CatalogueController(originalFileName);
            Catalogue catalogue = catalogueController.ParseCatalogue();
            Console.WriteLine(catalogue);
            Console.ReadLine();
        }
    }
}
