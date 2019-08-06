using Plumber71.Core.Abstractions;
using Plumber71.Core.Enums;

namespace Plumber71.Core.Model
{
    /// <summary>
    /// Каталог товаров на сайте
    /// </summary>
    public class CategoryDTO : CategoryAbstraction<ProductDTO>
    {
        public CategoryDTO() { }
        public CategoryDTO(string name) : base(name) { }
    }
}
