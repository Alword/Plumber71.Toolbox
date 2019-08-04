using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plumber71.Core.Model
{
    public class Catalogue
    {
        public DateTime PriceDate { get; set; }
        public double DollarRate { get; set; }
        public double EuroRate { get; set; }
        public string Reference { get; set; }
        public List<CategoryExcel> Categorys { get; set; }

        public int CategoryCount { get => Categorys.Count; }
        public int ProductsCount { get => Categorys.Sum(x => x.Products.Count); }
        public Catalogue()
        {
            Categorys = new List<CategoryExcel>();
        }

        public override string ToString()
        {
            return $"PriceDate: {PriceDate} DollarRate: {DollarRate} EurRate: {EuroRate} Reference: {Reference} (Cat/Prod): {CategoryCount}/{ProductsCount}";
        } 
    }
}