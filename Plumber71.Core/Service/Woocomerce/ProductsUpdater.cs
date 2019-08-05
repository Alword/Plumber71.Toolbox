using Plumber71.Core.Extentions;
using Plumber71.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooCommerceNET.WooCommerce.v3;

namespace Plumber71.Core.Service.Woocomerce
{
    public class ProductsUpdater
    {
        private readonly WooClient wooClient = null;
        public ProductsUpdater(WooClient wooClient)
        {
            this.wooClient = wooClient;
        }

        public async Task<IEnumerable<Product>> UploadRange(IEnumerable<PlumberCategory> plumberCategories)
        {
            var productList = plumberCategories.AsProductsIEnumerable();
            IEnumerable<Product> wooProducts = productList.Select(p => new Product()
            {
                id = p.Id,
                regular_price = (decimal)p.TotalPrice
            });
            return await wooClient.UpdateProductRange(wooProducts);
        }
    }
}
