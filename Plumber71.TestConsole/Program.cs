using Plumber71.Core.Controller;
using Plumber71.Core.Controller.Products;
using Plumber71.Core.Model;
using Plumber71.Core.Service.ChacheService;
using Plumber71.Core.Service.ExelPriceProvider.Model;
using Plumber71.Core.Service.PriceComparer;
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
            Console.WriteLine("Hello World");
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
            PricelistController catalogueController = new PricelistController(originalFileName);
            Priselist catalogue = catalogueController.ParseCatalogue();
            Console.WriteLine(catalogue);
        }

        static async void TestWooProductHandler()
        {
            WooClient wooClient = new WooClient();
            PlumberProductController plumberProductController = new PlumberProductController(wooClient);
            await plumberProductController.LoadOnDevice();
        }

        static void TestChache()
        {
            PlumberProduct productDomain = new PlumberProduct
            {
                Name = "Test"
            };
            ChacheService.WriteChache(productDomain);
        }

        static void TestArrayToDictionary()
        {
            var chacheObject = ChacheService.ReadChache<List<PlumberCategory>>("chacheObject.json").ToArray();
            ProductComparer product = new ProductComparer(chacheObject);
            PricelistController catalogueController = new PricelistController(originalFileName);
            Priselist catalogue = catalogueController.ParseCatalogue();
            product.GetChangedProducts(catalogue.Categorys);
        }
    }
}
