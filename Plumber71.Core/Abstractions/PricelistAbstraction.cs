using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Abstractions
{
    public abstract class PricelistAbstraction<C, P> where C : CategoryAbstraction<P> where P : ProductAbstraction
    {
        public DateTime Timestamp { get; set; }
        public List<C> Categories { get; set; }
    }
}
