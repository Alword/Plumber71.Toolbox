using Plumber71.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Plumber71.Core.Extentions;
using System.Linq;

namespace Plumber71.Core.Service.PriceComparer
{
    public class ProductComparer
    {
        public readonly CategoryDomain[] chacheCategoryes = null;
        public ProductComparer(CategoryDomain[] chacheCategory)
        {
            this.chacheCategoryes = chacheCategory;
        }

        public List<ProductDomain> GetChangedProducts(List<CategoryExcel> categoryExcels)
        {
            var chacedProductsDictionary = new Dictionary<string, ProductDomain>();
            var productsList = new List<ProductDomain>();
            // Добавить все продукты в словарь
            foreach (var chacedCategory in chacheCategoryes)
            {
                productsList.AddRange(chacedCategory.Products);
            }

            // Словарь товаров
            chacedProductsDictionary = productsList.ToArray().ConvertToDictionary(p => p.Name);

            ProductDomain currentProduct = null;
            List<ProductDomain> changedProducts = new List<ProductDomain>();
            foreach (var category in categoryExcels)
            {
                foreach (var product in category.Products)
                {
                    if (chacedProductsDictionary.ContainsKey(product.Name))
                    {
                        currentProduct = chacedProductsDictionary[product.Name];
                        if (currentProduct.TotalPrice != product.TradePriceInRubbles)
                        {
                            currentProduct.TotalPrice = product.TradePriceInRubbles;
                            changedProducts.Add(currentProduct);
                        }
                    }
                }
            }
            // категори на сайте в словарь
            //var chacheDictionary = chacheCategory.ConvertToDictionary()
            return changedProducts;
        }
    }
}
