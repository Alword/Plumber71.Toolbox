using Plumber71.Core.Abstractions;

namespace Plumber71.Core.Model
{
    /// <summary>
    /// Каталог товаров на сайте
    /// </summary>
    public class CategoryDomain : CategoryAbstraction<ProductDomain>
    {
        public CategoryDomain() { }
        public CategoryDomain(string name) : base(name) { }
    }
}
