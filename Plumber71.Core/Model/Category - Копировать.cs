using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Model
{
    public class CategoryDomain
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public List<ProductDomain> Products { get; set; }

        public CategoryDomain(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
            Products = new List<ProductDomain>();
        }

        public override string ToString()
        {
            return $"Id: {Id} Name: {Name} ProductsCount: {Products.Count}";
        }
    }
}
