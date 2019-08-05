using Plumber71.Core.Model;
using Plumber71.Core.Service.Woocomerce;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WooCommerceNET.WooCommerce.v3;

namespace Plumber71.Core.Controller.Products
{
    public class ProductsDownloader
    {
        WooClient wooClient = null;
        public ProductsDownloader(WooClient wooClient)
        {
            this.wooClient = wooClient;
        }
        public async Task<IEnumerable<PlumberCategory>> DownloadAll()
        {
            int currentPage = 0;
            int productsPerPage = WooClient.PRODUCTS_PER_PAGE;
            int totalProducts = 0;
            int productsOnCurrentPage;
            Dictionary<string, PlumberCategory> categories = new Dictionary<string, PlumberCategory>();
            do
            {
                var wooProducts = await wooClient.GetProductsPage(productsPerPage, ++currentPage); // Скачиваем страницу
                productsOnCurrentPage = wooProducts.Count;
                totalProducts += productsOnCurrentPage;
                HandleProductsPage(categories, wooProducts); // Обрабатываем товары
                Debug.WriteLine($"Page {currentPage} ProductsCount {productsOnCurrentPage} Total {totalProducts}");

            } while (productsOnCurrentPage == productsPerPage);

            return categories.Values.AsEnumerable();
        }

        private void HandleProductsPage(Dictionary<string, PlumberCategory> categories, List<Product> wooProducts)
        {
            foreach (var wooProduct in wooProducts)
            {
                // handle product
                PlumberProduct product = HandleProduct(wooProduct);
                // check category 
                CheckCategory(categories, wooProduct, product);
            }
        }

        private PlumberProduct HandleProduct(Product wooProduct)
        {
            var product = new PlumberProduct()
            {
                Id = (int)wooProduct.id,
                Name = wooProduct.name,
                Sku = wooProduct.sku,
                TotalPrice = (double)wooProduct.price,
            };
            return product;
        }

        private static void CheckCategory(Dictionary<string, PlumberCategory> categories, Product wooProduct, PlumberProduct product)
        {
            string categoryName = wooProduct.categories[0].name;
            if (categories.ContainsKey(wooProduct.categories[0].name))
            {
                AddProductInCategoryIfNotExist(categories, product, categoryName);
            }
            else
            {
                CreateCategoryAndAddProduct(categories, product, categoryName);
            }
        }

        private static void AddProductInCategoryIfNotExist(Dictionary<string, PlumberCategory> categories, PlumberProduct product, string categoryName)
        {
            // Если существует

            // проверить наличее товара
            var category = categories[categoryName];

            var findedProduct = category.Products.Find(p => p.Name == product.Name); //null

            // если товара нет
            if (findedProduct == null)
            {
                category.Products.Add(product);
            }
        }

        private static void CreateCategoryAndAddProduct(Dictionary<string, PlumberCategory> categories, PlumberProduct product, string categoryName)
        {
            PlumberCategory newCategory = new PlumberCategory(categoryName);// Создаём категорию
            categories.Add(categoryName, newCategory);// Добавляем в словарь
            newCategory.Products.Add(product);
        }
    }
}
