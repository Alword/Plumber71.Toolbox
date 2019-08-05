using Plumber71.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Plumber71.Core.Extentions;
using System.Linq;
using Plumber71.Core.Service.ExelPriceProvider.Model;

namespace Plumber71.Core.Service.PriceComparer
{
    public class ProductComparer
    {
        public readonly CategoryDomain[] chacheCategoryes = null;
        public ProductComparer(CategoryDomain[] chacheCategory)
        {
            this.chacheCategoryes = chacheCategory;
        }

        public List<ProductDomain> GetChangedProducts(List<ExcelCategory> categoryExcels)
        {
            var productsList = from category in chacheCategoryes
                               from product in category.Products
                               select product;

            var chacedProductsDictionary = productsList.ConvertToDictionary(p => p.Name);

            return GetChangedProducts(categoryExcels, chacedProductsDictionary);
        }

        private static List<ProductDomain> GetChangedProducts(List<ExcelCategory> categoryExcels,
            Dictionary<string, ProductDomain> chacedProductsDictionary)
        {
            ProductDomain currentProduct = null;
            IEnumerable<ExcelProduct> allProducts = from category in categoryExcels
                                                    from product in category.Products
                                                    where chacedProductsDictionary.ContainsKey(product.Name)
                                                    select product;
            List<ProductDomain> changedProducts = new List<ProductDomain>();

            foreach (var product in allProducts)
            {
                currentProduct = chacedProductsDictionary[product.Name];
                if (currentProduct.TotalPrice != product.TradePriceInRubbles)
                {
                    currentProduct.TotalPrice = product.TradePriceInRubbles;
                    changedProducts.Add(currentProduct);
                }
            }
            return changedProducts;
        }
    }
}
