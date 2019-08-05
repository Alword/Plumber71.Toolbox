using Plumber71.Core.Abstractions;
using Plumber71.Core.Enums;

namespace Plumber71.Core.Model
{
    /// <summary>
    /// Каталог товаров на сайте
    /// </summary>
    public class PlumberCategory : CategoryAbstraction<PlumberProduct>
    {
        public Currencies ProductsCurrency;
        public PlumberCategory() { }
        public PlumberCategory(string name) : base(name) { }
    }
}
