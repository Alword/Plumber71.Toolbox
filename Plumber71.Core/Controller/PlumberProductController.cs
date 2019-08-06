using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plumber71.Core.Model;
using Plumber71.Core.Service.JsonFileService;
using Plumber71.Core.Service.Woocomerce;

namespace Plumber71.Core.Controller
{
    public class PlumberProductController
    {
        public const string PRISELIST_CHACHE = "pricelistChache.json";
        private readonly ProductsDownloader productsDownloader;
        public PlumberProductController(WooClient wooClient)
        {
            productsDownloader = new ProductsDownloader(wooClient);
        }

        public async Task LoadOnDevice()
        {
            // Кеширование товаров
            var chacheProducts = await productsDownloader.DownloadAll();
            //ЧАчапури под лодочкой
            JsonFileStorage.Save(chacheProducts.ToList(), PRISELIST_CHACHE);
        }

        public void UpdatePricesFromExcel(string path)
        {
            // load chache 
            var chacheProducts = JsonFileStorage.Load<List<CategoryDTO>>(PRISELIST_CHACHE)
            // load excel
            var excelProducts = 
            // Price markup
            // GetChanges
            // Upload Products
        }

        public void UpdatePrices()
        {

        }
    }
}
