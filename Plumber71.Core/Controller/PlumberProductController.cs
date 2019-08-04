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
            Dictionary<string, CategoryDomain> categories = new Dictionary<string, CategoryDomain>();

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
                    string categoryName = wooProduct.categories[0].name;
                    if (categories.ContainsKey(wooProduct.categories[0].name))
                    {
                        // Если существует
                        
                        // проверить наличее товара
                        var category = categories["categoryName"];

                        var findedProduct = category.Products.Find(p => p.Name == product.Name); //null

                        // если товара нет
                        if (findedProduct == null)
                        {
                            category.Products.Add(product);
                        }
                    }
                    else
                    {
                        // Если нет
                    }

                    
                // add product in category

                }



            } while (productsCount > 0);

        }

        private ProductDomain HandleProduct(Product wooProduct)
        {
            var product = new ProductDomain()
            {
                Sku = (int)wooProduct.id,
                Currency = Currencies.RUB,
                Name = wooProduct.name,
                TotalPrice = (double)wooProduct.price,
            };
            return null;
        }
    }
}
