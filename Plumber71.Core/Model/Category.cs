using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Model
{
    public class Category
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public List<ProductExcel> Products { get; set; }

        public Category(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
            Products = new List<ProductExcel>();
        }

        public override string ToString()
        {
            return $"Id: {Id} Name: {Name} ProductsCount: {Products.Count}";
        }
    }
}
