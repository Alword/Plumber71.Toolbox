using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Model
{
    public class Catalogue
    {
        public DateTime PriceDate { get; set; }
        public double DollarRate { get; set; }
        public double EuroRate { get; set; }
        public string Info { get; set; }
        public List<Category> Categorys { get; set; }

    }
}