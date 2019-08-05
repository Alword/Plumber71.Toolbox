using Plumber71.Core.Enums;
using Plumber71.Core.Model;
using Plumber71.Core.Service.ChacheService;
using Plumber71.Core.Service.Woocomerce;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooCommerceNET.WooCommerce.v3;

namespace Plumber71.Core.Controller.Products
{
    public class PlumberProductController
    {
        private readonly WooClient wooClient;
        private readonly ProductsDownloader productsDownloader;
        public PlumberProductController(WooClient wooClient)
        {
            this.wooClient = wooClient;
            this.productsDownloader = new ProductsDownloader(wooClient);
        }

        public async Task ChacheProducts()
        {
            // Кеширование товаров
            var chacheProducts = await productsDownloader.DownloadAll();
            //ЧАчапури под лодочкой
            ChacheService.WriteChache(chacheProducts);
        }
    }
}
