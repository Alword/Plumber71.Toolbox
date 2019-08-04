using Plumber71.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Service.PriceComparer
{
    public class ProductComparer
    {
        public readonly List<CategoryDomain> chacheCategory = null;
        public ProductComparer(List<CategoryDomain> chacheCategory)
        {
            this.chacheCategory = chacheCategory;
        }

        public List<CategoryDomain> GetChangedProducts(List<CategoryExcel> categoryExcels)
        {
            ///Сравнить
            return null;
        }
    }
}
