using Plumber71.Core.Controller;
using Plumber71.Core.Model;
using Plumber71.Core.Service.ChacheService;
using Plumber71.Core.Service.Woocomerce;
using System;
using System.Collections.Generic;

namespace Plumber71.TestConsole
{
    class Program
    {
        private static string originalFileName = $"{Environment.CurrentDirectory}/Resource/price_d5.xls";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world");
            TestWooProductHandler();
            Console.ReadLine();
        }

        static async void Test()
        {
            WooClient client = new WooClient();
            List<WooCommerceNET.WooCommerce.v3.Product> products = await client.GetProductsPage();
        }

        static void TestExcel()
        {
            CatalogueController catalogueController = new CatalogueController(originalFileName);
            Catalogue catalogue = catalogueController.ParseCatalogue();
            Console.WriteLine(catalogue);
        }

        static async void TestWooProductHandler()
        {
            WooClient wooClient = new WooClient();
            PlumberProductController plumberProductController = new PlumberProductController(wooClient);
            await plumberProductController.ChacheProducts();
        }

        static void TestChache()
        {
            ProductDomain productDomain = new ProductDomain
            {
                Name = "Test"
            };
            ChacheService.WriteChache(productDomain);
        }
    }
}
