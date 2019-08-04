using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Model
{
    public class CategoryDomain
    {
        public string Name { get; set; }
        public List<ProductDomain> Products { get; set; }

        public CategoryDomain(string Name)
        {
            this.Name = Name;
            Products = new List<ProductDomain>();
        }

        public override string ToString()
        {
            return $"Name: {Name} ProductsCount: {Products.Count}";
        }
    }
}
