using Plumber71.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plumber71.Core.Service.PricelisDataSetParser.Model
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

        public static explicit operator PricelistDTO(Priselist priselist)  // explicit byte to digit conversion operator
        {
            PricelistDTO pricelistDTO = new PricelistDTO()
            {
                ProductsCurrency = Enums.Currencies.RUB,
                Timestamp = priselist.PriceDate,
                Categories = new List<CategoryDTO>()
            };

            var products = from category in priselist.Categorys
                           select (CategoryDTO)category;

            pricelistDTO.Categories.AddRange(products);

            return pricelistDTO;
        }
    }
}
