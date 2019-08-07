using Plumber71.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WooCommerceNET.WooCommerce.v3;

namespace Plumber71.Core.Service.Woocomerce
{
    public class ProductsDownloader
    {
        public delegate void ProductDownloadedDelegate(int totalCount);

        public event ProductDownloadedDelegate OnProductDownloaded;

        WooClient wooClient = null;
        public ProductsDownloader(WooClient wooClient)
        {
            this.wooClient = wooClient;
        }
        public async Task<PricelistDTO> DownloadAll()
        {
            int currentPage = 0;
            int productsPerPage = WooClient.PRODUCTS_PER_PAGE;
            int totalProducts = 0;
            int productsOnCurrentPage;
            Dictionary<string, CategoryDTO> categories = new Dictionary<string, CategoryDTO>();
            do
            {
                var wooProducts = await wooClient.GetProductsPage(productsPerPage, ++currentPage); // Скачиваем страницу
                productsOnCurrentPage = wooProducts.Count;
                totalProducts += productsOnCurrentPage;
                HandleProductsPage(categories, wooProducts); // Обрабатываем товары
                Debug.WriteLine($"Page {currentPage} ProductsCount {productsOnCurrentPage} Total {totalProducts}");
                OnProductDownloaded?.Invoke(totalProducts);

            } while (productsOnCurrentPage == productsPerPage);

            PricelistDTO pricelist = new PricelistDTO()
            {
                //TODO load from config;
                ProductsCurrency = Enums.Currencies.RUB,
                Timestamp = DateTime.Now,
                Categories = categories.Values.ToList()
            };

            return pricelist;
        }

        private void HandleProductsPage(Dictionary<string, CategoryDTO> categories, List<Product> wooProducts)
        {
            foreach (var wooProduct in wooProducts)
            {
                // handle product
                ProductDTO product = HandleProduct(wooProduct);
                // check category 
                CheckCategory(categories, wooProduct, product);
            }
        }

        private ProductDTO HandleProduct(Product wooProduct)
        {
            var product = new ProductDTO()
            {
                Id = (int)wooProduct.id,
                Name = wooProduct.name,
                Sku = wooProduct.sku,
                TotalPrice = (double)wooProduct.price,
            };
            return product;
        }

        private static void CheckCategory(Dictionary<string, CategoryDTO> categories, Product wooProduct, ProductDTO product)
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

        private static void AddProductInCategoryIfNotExist(Dictionary<string, CategoryDTO> categories, ProductDTO product, string categoryName)
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

        private static void CreateCategoryAndAddProduct(Dictionary<string, CategoryDTO> categories, ProductDTO product, string categoryName)
        {
            CategoryDTO newCategory = new CategoryDTO(categoryName);// Создаём категорию
            categories[categoryName] = newCategory;// Добавляем в словарь
            newCategory.Products.Add(product);
        }
    }
}
