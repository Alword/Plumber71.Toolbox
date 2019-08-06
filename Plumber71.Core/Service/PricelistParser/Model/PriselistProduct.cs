using Plumber71.Core.Enums;
using Plumber71.Core.Model;

namespace Plumber71.Core.Service.ExelPriceProvider.Model
{
    public class PriselistProduct : ProductDTO
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
            return $"Id: {Sku} Name: {Name} Pieces: {Pieces} Currency: {Currency.ToString()} " +
                $"TradePriceInCurrency: {TradePriceInCurrency} TradePriceInRubbles: {TradePriceInRubbles} Price7Ka {Price7Ka}";
        }
    }
}
