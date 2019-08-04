using Plumber71.Core.Model;
using Plumber71.Core.Service.Woocomerce;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WooCommerceNET.WooCommerce.v3;

namespace Plumber71.Core.Controller
{
    public class PlumberProductController
    {
        private readonly WooClient wooClient;
        public PlumberProductController(WooClient wooClient)
        {
            this.wooClient = wooClient;
        }

        public async Task ChacheProducts()
        {
            Dictionary<string, List<Category>> categories = new Dictionary<string, List<Category>>();

            int productsCount = 0;
            int currentPage = 1;
            do
            {
                var wooProducts = await wooClient.GetProductsPage(page: currentPage);
                productsCount = wooProducts.Count;

                foreach (var wooProduct in wooProducts)
                {
                    

                    // handle product
                    ProductDomain product = HandleProduct(wooProduct);

                    // check category 
                    if (categories.ContainsKey(wooProduct.categories[0].name))
                    {
                        //
                    }

                    
                // add product in category

                }



            } while (productsCount > 0);

        }

        private ProductDomain HandleProduct(Product wooProduct)
        {
            var product = new ProductDomain()
            {
                Id = (int)wooProduct.id,
                Currency = Currencies.RUB,
                Name = wooProduct.name,
                TotalPrice = (double)wooProduct.price,
            };
            return null;
        }
    }
}
