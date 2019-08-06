using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Abstractions
{
    public abstract class PricelistAbstraction<T> where T : ProductAbstraction
    {
        public DateTime Timestamp { get; set; }
        public List<CategoryAbstraction<T>> Categories { get; set; }
    }
}
