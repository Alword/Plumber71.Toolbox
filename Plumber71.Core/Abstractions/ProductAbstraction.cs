using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Abstractions
{
    public abstract class ProductAbstraction
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public int Pieces { get; set; }
        public string Sku { get; set; }
        public double RegularPrice { get; set; }
    }
}
