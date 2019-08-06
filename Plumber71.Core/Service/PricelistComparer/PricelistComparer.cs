using Plumber71.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Plumber71.Core.Extentions;
using System.Linq;

namespace Plumber71.Core.Service.PricelistComparer
{
    public class PricelistComparer
    {
        public static List<ProductDTO> GetChangedProducts(PricelistDTO currentPricelist, PricelistDTO updatedList)
        {
            return GetChangedProducts(currentPricelist.Categories, updatedList.Categories);
        }
        public static List<ProductDTO> GetChangedProducts(IEnumerable<CategoryDTO> currentCategories, IEnumerable<CategoryDTO> updatedCategories)
        {
            IEnumerable<ProductDTO> productsList = currentCategories.AsProductsIEnumerable();

            Dictionary<string, ProductDTO> currentProductsDict = productsList.ConvertToDictionary(p => p.Name);

            return GetChangedProducts(currentProductsDict, updatedCategories);
        }

        private static List<ProductDTO> GetChangedProducts(Dictionary<string, ProductDTO> curCats, IEnumerable<CategoryDTO> updCats)
        {
            IEnumerable<ProductDTO> sameProducts = updCats
                .AsProductsIEnumerable()
                .Where(p => curCats.ContainsKey(p.Name));

            List<ProductDTO> changedProducts = new List<ProductDTO>();
            ProductDTO currentProduct = null;
            foreach (var newProduct in sameProducts)
            {
                currentProduct = curCats[newProduct.Name];
                if (currentProduct.TotalPrice != newProduct.TotalPrice)
                {
                    currentProduct.TotalPrice = newProduct.TotalPrice;
                    changedProducts.Add(currentProduct);
                }
            }
            return changedProducts;
        }
    }
}
