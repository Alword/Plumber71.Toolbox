using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Model
{
    public class ProductExcel : ProductDomain
    {
        //Оптовая цена
        public double TradePriceInCurrency { get; set; }
        //Оптовая рубли
        public double TradePriceInRubbles { get; set; }
        //Цена 7-ка
        public double Price7Ka { get; set; }

        public ProductExcel(int id)
        {
            this.Sku = id;
        }

        public override string ToString()
        {
            return $"Id: {Sku} Name: {Name} Pieces: {Pieces} Currency: {Currency.ToString()} " +
                $"TradePriceInCurrency: {TradePriceInCurrency} TradePriceInRubbles: {TradePriceInRubbles} Price7Ka {Price7Ka}";
        }
    }
}
