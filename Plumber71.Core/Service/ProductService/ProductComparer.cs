using Plumber71.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Plumber71.Core.Extentions;
using System.Linq;

namespace Plumber71.Core.Service.ProductService
{
    public class ProductComparer
    {
        public readonly IEnumerable<CategoryDTO> chacheCategoryes = null;
        public ProductComparer(CategoryDTO[] chacheCategory)
        {
            chacheCategoryes = chacheCategory;
        }

        public List<ProductDTO> GetChangedProducts(List<CategoryDTO> categoryExcels)
        {
            var productsList = chacheCategoryes.AsProductsIEnumerable();

            var chacedProductsDictionary = productsList.ConvertToDictionary(p => p.Name);

            return GetChangedProducts(categoryExcels, chacedProductsDictionary);
        }

        private static List<ProductDTO> GetChangedProducts(List<CategoryDTO> categoryList,
            Dictionary<string, ProductDTO> chacedProductsDictionary)
        {
            IEnumerable<ProductDTO> allProducts = categoryList
                .AsProductsIEnumerable()
                .Where(p => chacedProductsDictionary.ContainsKey(p.Name));

            List<ProductDTO> changedProducts = new List<ProductDTO>();
            ProductDTO currentProduct = null;
            foreach (var product in allProducts)
            {
                currentProduct = chacedProductsDictionary[product.Name];
                if (currentProduct.TotalPrice != product.TotalPrice)
                {
                    currentProduct.TotalPrice = product.TotalPrice;
                    changedProducts.Add(currentProduct);
                }
            }
            return changedProducts;
        }
    }
}
