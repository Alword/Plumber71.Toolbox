using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Model
{
    public class Category
    {
        // Кот
        public int Id { get; set; }
        // Название
        public string Name { get; set;}
        // Товары
        public List<Product> Products { get; set; }
    }
}
