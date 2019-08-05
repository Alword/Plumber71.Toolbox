using Plumber71.Core.Controller;
using Plumber71.Core.Model;
using Plumber71.Core.Service.ExelPriceProvider.Model;
using Plumber71.Core.Service.JsonFileService;
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
            TestPriceMarkupService();
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
            JsonFileStorage.Save(productDomain);
        }

        static void TestArrayToDictionary()
        {
            var chacheObject = JsonFileStorage.Load<List<PlumberCategory>>("chacheObject.json").ToArray();
            ProductComparer product = new ProductComparer(chacheObject);
            PricelistController catalogueController = new PricelistController(originalFileName);
            Priselist catalogue = catalogueController.ParseCatalogue();
            product.GetChangedProducts(catalogue.Categorys);
        }

        static void TestPriceMarkupService()
        {
            PriceMarkupController pricelistController = new PriceMarkupController();
            pricelistController.SetCategoryRate("Котлы настенные", 1.2);
            pricelistController.SetProductRate(346, 1.3);
            pricelistController.SetGlobalRate(1.4);
            var chacheObject = JsonFileStorage.Load<List<PlumberCategory>>("chacheObject.json").ToArray();
            pricelistController.ApplySetting(chacheObject);
        }
    }
}
