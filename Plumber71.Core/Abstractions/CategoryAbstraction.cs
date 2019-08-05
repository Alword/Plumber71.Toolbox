using System.Collections.Generic;

namespace Plumber71.Core.Abstractions
{
    public abstract class CategoryAbstraction<T>
    {
        public string Name { get; set; }
        public List<T> Products { get; set; }
        public CategoryAbstraction() { }
        public CategoryAbstraction(string Name)
        {
            this.Name = Name;
            Products = new List<T>();
        }
        public override string ToString()
        {
            return $"Name: {Name} ProductsCount: {Products.Count}";
        }
    }
}