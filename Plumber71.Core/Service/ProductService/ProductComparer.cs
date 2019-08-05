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
            var productsList = chacheCategoryes.AsProductsIEnumerable();

            var chacedProductsDictionary = productsList.ConvertToDictionary(p => p.Name);

            return GetChangedProducts(categoryExcels, chacedProductsDictionary);
        }

        private static List<PlumberProduct> GetChangedProducts(List<PriselistCategory> categoryExcels,
            Dictionary<string, PlumberProduct> chacedProductsDictionary)
        {
            IEnumerable<PriselistProduct> allProducts = categoryExcels
                .AsProductsIEnumerable()
                .Where(p => chacedProductsDictionary.ContainsKey(p.Name));

            List<PlumberProduct> changedProducts = new List<PlumberProduct>();
            PlumberProduct currentProduct = null;
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
