using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
        public const string PRICE_CONFIG = "plumberPriceConfig.json";

        public readonly PriceMarkupController PriceMarkup;
        public readonly ProductsDownloader ProductDownloader;
        public readonly ProductsUpdater ProductsUpdater;

        private readonly WooClient wooClient;

        public PlumberProductController(WooClient wooClient)
        {
            this.wooClient = wooClient;
            ProductDownloader = new ProductsDownloader(wooClient);
            ProductsUpdater = new ProductsUpdater(wooClient);
            PriceMarkup = new PriceMarkupController(PRICE_CONFIG);
        }

        public PlumberProductController(string restConfigJson)
        {
            WooClient wooClient = new WooClient(restConfigJson);
            this.wooClient = wooClient;
            ProductDownloader = new ProductsDownloader(wooClient);
            ProductsUpdater = new ProductsUpdater(wooClient);
            PriceMarkup = new PriceMarkupController(PRICE_CONFIG);
        }


        public async Task<PricelistDTO> LoadOnDevice()
        {
            // Кеширование товаров
            PricelistDTO chacheProducts = await ProductDownloader.DownloadAll();

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
            PricelistDTO updatedPricelist = PriceMarkup.ApplySetting(newPricelist);
            // GetChangedProducts
            List<ProductDTO> changedProducts = PricelistComparer.GetChangedProducts(currentPricelist, updatedPricelist);
            // Upload Products
            await ProductsUpdater.UploadRange(changedProducts);
            JsonFileStorage.Save(currentPricelist, PRISELIST_CHACHE);
        }

        public void UpdatePrices()
        {

        }
    }
}
