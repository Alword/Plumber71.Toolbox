using Plumber71.Core.Abstractions;
using Plumber71.Core.Enums;
using Plumber71.Core.Model;

namespace Plumber71.Core.Service.PricelisDataSetParser.Model
{
    public class PriselistProduct : ProductAbstraction
    {
        public int Code { get => Key; set => Key = value; }

        public double TradePriceInCurrency { get; set; }

        public double TradePriceInRubbles { get; set; }

        public double Price7Ka { get; set; }

        public Currencies Currency { get; set; }

        public PriselistProduct(int id)
        {
            Code = id;
        }

        public override string ToString()
        {
            return $"Id: {Sku} Name: {Name} Pieces: {Pieces} Currency: {Currency} " +
                $"TradePriceInCurrency: {TradePriceInCurrency} TradePriceInRubbles: {TradePriceInRubbles} Price7Ka {Price7Ka}";
        }

        public static explicit operator ProductDTO(PriselistProduct product)
        {
            ProductDTO productDTO = new ProductDTO()
            {
                Id = 0,
                Key = 0,
                Name = product.Name,
                Sku = $"{product.Code}",
                Pieces = product.Pieces,
                RegularPrice = product.TradePriceInRubbles,
                TotalPrice = product.TradePriceInRubbles
            };
            return productDTO;
        }
    }
}
