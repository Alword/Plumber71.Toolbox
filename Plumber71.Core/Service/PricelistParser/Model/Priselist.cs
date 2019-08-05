using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plumber71.Core.Service.ExelPriceProvider.Model
{
    public class Priselist
    {
        public DateTime PriceDate { get; set; }
        public double DollarRate { get; set; }
        public double EuroRate { get; set; }
        public string Reference { get; set; }
        public List<PriselistCategory> Categorys { get; set; }

        public int CategoryCount { get => Categorys.Count; }
        public int ProductsCount { get => Categorys.Sum(x => x.Products.Count); }
        public Priselist()
        {
            Categorys = new List<PriselistCategory>();
        }

        public override string ToString()
        {
            return $"PriceDate: {PriceDate} DollarRate: {DollarRate} EurRate: {EuroRate} Reference: {Reference} (Cat/Prod): {CategoryCount}/{ProductsCount}";
        }
    }
}
