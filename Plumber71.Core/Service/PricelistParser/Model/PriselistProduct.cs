using Plumber71.Core.Model;

namespace Plumber71.Core.Service.ExelPriceProvider.Model
{
    public class PriselistProduct : PlumberProduct
    {
        //Оптовая цена
        public double TradePriceInCurrency { get; set; }
        //Оптовая рубли
        public double TradePriceInRubbles { get; set; }
        //Цена 7-ка
        public double Price7Ka { get; set; }

        public PriselistProduct(int id)
        {
            Sku = id;
        }

        public override string ToString()
        {
            return $"Id: {Sku} Name: {Name} Pieces: {Pieces} Currency: {Currency.ToString()} " +
                $"TradePriceInCurrency: {TradePriceInCurrency} TradePriceInRubbles: {TradePriceInRubbles} Price7Ka {Price7Ka}";
        }
    }
}
