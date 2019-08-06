using Plumber71.Core.Controller;
using Plumber71.Core.Model;
using Plumber71.Core.Service.JsonFileService;
using Plumber71.Core.Service.PricelisDataSetParser.Model;
using Plumber71.Core.Service.PricelistComparer;
using Plumber71.Core.Service.Woocomerce;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plumber71.TestConsole
{
    class Program
    {
        private static string originalFileName = $"{Environment.CurrentDirectory}/Resource/price_d5.xls";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            UpdatePricesFromExcel();
            Console.ReadLine();
        }

        static async void Test()
        {
            WooClient client = WooClient.DefaultClient();
            List<WooCommerceNET.WooCommerce.v3.Product> products = await client.GetProductsPage();
        }

        static void TestExcel()
        {
            ExcelPricelistController catalogueController = new ExcelPricelistController(originalFileName);
            Priselist catalogue = catalogueController.GetExcelPricelist();
            Console.WriteLine(catalogue);
        }

        static async void TestWooProductHandler()
        {
            WooClient wooClient = WooClient.DefaultClient();
            PlumberProductController plumberProductController = new PlumberProductController(wooClient);
            await plumberProductController.LoadOnDevice();
        }

        static void TestChache()
        {
            ProductDTO productDomain = new ProductDTO
            {
                Name = "Test"
            };
            JsonFileStorage.Save(productDomain);
        }

        static void TestArrayToDictionary()
        {
            var chacheObject = JsonFileStorage.Load<List<CategoryDTO>>("chacheObject.json").ToArray();
            ExcelPricelistController catalogueController = new ExcelPricelistController(originalFileName);
            PricelistDTO catalogue = (PricelistDTO)catalogueController.GetExcelPricelist();

            var test = PricelistComparer.GetChangedProducts(catalogue.Categories, chacheObject);
        }

        static async void TestPriceMarkupService()
        {
            PriceMarkupController pricelistController = new PriceMarkupController();
            pricelistController.SetGlobalRate(1.12);
            //pricelistController.SetCategoryRate("Котлы настенные", 1.2);
            //pricelistController.SetProductRate(346, 1.3);
            var chacheObject = JsonFileStorage.Load<List<CategoryDTO>>("chacheObject.json");
            chacheObject = pricelistController.ApplySetting(chacheObject).ToList();
            JsonFileStorage.Save(chacheObject);
            ProductsUpdater pu = new ProductsUpdater(WooClient.DefaultClient());
            Console.WriteLine($"Done: {nameof(TestPriceMarkupService)}");
        }

        static async void UpdatePricesFromExcel()
        {
            List<int> test = new List<int>() { 1, 2, 3 };
            PlumberProductController plumberProductController = new PlumberProductController(WooClient.DefaultClient());
            plumberProductController.UpdatePricesFromExcel(originalFileName);
        }
    }
}
