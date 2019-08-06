using Plumber71.Core.Abstractions;
using Plumber71.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Service.ExelPriceProvider.Model
{
    public class PriselistCategory : CategoryAbstraction<PriselistProduct>
    {
        public int Id { get; set; }

        public PriselistCategory(int Id, string Name) : base(Name)
        {
            this.Id = Id;
        }

        public override string ToString()
        {
            return $"Id: {Id} {base.ToString()}";
        }

        public static explicit operator CategoryDTO(PriselistCategory category)
        {
            CategoryDTO categoryDTO = new CategoryDTO
            {
                Name = category.Name,
                Products = new List<ProductDTO>()
            };

            foreach (var product in category.Products)
            {
                Products.Add((ProductDTO)product)
            }

            return categoryDTO;
        }
    }
}
