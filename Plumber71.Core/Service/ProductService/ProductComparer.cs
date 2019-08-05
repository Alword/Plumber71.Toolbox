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
        public readonly PlumberCategory[] chacheCategoryes = null;
        public ProductComparer(PlumberCategory[] chacheCategory)
        {
            this.chacheCategoryes = chacheCategory;
        }

        public List<PlumberProduct> GetChangedProducts(List<PriselistCategory> categoryExcels)
        {
            var productsList = from category in chacheCategoryes
                               from product in category.Products
                               select product;

            var chacedProductsDictionary = productsList.ConvertToDictionary(p => p.Name);

            return GetChangedProducts(categoryExcels, chacedProductsDictionary);
        }

        private static List<PlumberProduct> GetChangedProducts(List<PriselistCategory> categoryExcels,
            Dictionary<string, PlumberProduct> chacedProductsDictionary)
        {
            PlumberProduct currentProduct = null;
            IEnumerable<PriselistProduct> allProducts = from category in categoryExcels
                                                    from product in category.Products
                                                    where chacedProductsDictionary.ContainsKey(product.Name)
                                                    select product;
            List<PlumberProduct> changedProducts = new List<PlumberProduct>();

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
