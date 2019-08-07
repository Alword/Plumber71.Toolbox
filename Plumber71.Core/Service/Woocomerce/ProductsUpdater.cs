using Plumber71.Core.Extentions;
using Plumber71.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooCommerceNET.WooCommerce.v3;

namespace Plumber71.Core.Service.Woocomerce
{
    public class ProductsUpdater
    {
        public delegate void OnProductsUploadedDelegate(int totalUploaded);
        public event OnProductsUploadedDelegate OnProductsUpdated;

        private const int MAX_PRODUCTS_PER_REQUEST = 100;
        private readonly WooClient wooClient = null;
        public ProductsUpdater(WooClient wooClient)
        {
            this.wooClient = wooClient;
        }

        public async Task<int> UploadRange(IEnumerable<CategoryDTO> plumberCategories)
        {
            var productList = plumberCategories.AsProductsIEnumerable();
            return await UploadRange(productList);
        }

        public async Task<int> UploadRange(IEnumerable<ProductDTO> products)
        {
            if (products.Count() == 0) return 0;

            int totalUpdated = 0;

            IEnumerable<Product> wooProducts = products.Select(p => new Product()
            {
                id = p.Id,
                regular_price = (decimal)p.TotalPrice
            });

            do
            {
                IEnumerable<Product> updatePack = wooProducts.Take(MAX_PRODUCTS_PER_REQUEST);
                totalUpdated += (await wooClient.UpdateProductRange(updatePack)).Count();
                wooProducts = wooProducts.Skip(MAX_PRODUCTS_PER_REQUEST);
                OnProductsUpdated?.Invoke(totalUpdated);
                Debug.WriteLine($"Current pack {updatePack.Count()} TotalUpdated: {totalUpdated}");
            }
            while (wooProducts.Count() > 0);

            return totalUpdated;
        }
    }
}
