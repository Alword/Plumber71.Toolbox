using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plumber71.Core.Model;
using Plumber71.Core.Service.JsonFileService;
using Plumber71.Core.Service.PricelisDataSetParser.Model;
using Plumber71.Core.Service.PricelistComparer;
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

        public async Task<PricelistDTO> LoadOnDevice()
        {
            // Кеширование товаров
            PricelistDTO chacheProducts = await productsDownloader.DownloadAll();

            JsonFileStorage.Save(chacheProducts, PRISELIST_CHACHE);

            return chacheProducts;
        }

        public async void UpdatePricesFromExcel(string path)
        {
            // load chache 
            PricelistDTO currentPricelist = JsonFileStorage.Load<PricelistDTO>(PRISELIST_CHACHE);
            currentPricelist = currentPricelist ?? await LoadOnDevice();
            // load excel
            PricelistDTO newPricelist = (PricelistDTO)(new ExcelPricelistController(path).GetExcelPricelist());
            // Price markup
            PricelistDTO updatedPricelist = new PriceMarkupController().ApplySetting(newPricelist);
            // GetChangedProducts
            List<ProductDTO> changedProducts = PricelistComparer.GetChangedProducts(currentPricelist, updatedPricelist);
            // Upload Products
            ProductsUpdater productsUpdater = new ProductsUpdater(WooClient.DefaultClient());
            await productsUpdater.UploadRange(changedProducts);
            JsonFileStorage.Save(currentPricelist,PRISELIST_CHACHE);
        }

        public void UpdatePrices()
        {

        }
    }
}
