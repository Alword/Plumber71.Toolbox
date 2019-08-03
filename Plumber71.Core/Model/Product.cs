using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Model
{
    public class Product
    {
        //id
        public int Id { get; set; }
        //название stringi
        public string Name { get; set; }
        //штуки
        public int Pieces { get; set; }
        //Валюта
        public Currencies Currency { get; set; }
        //Оптовая цена
        public double TradePriceInCurrency { get; set; }
        //Оптовая рубли
        public double TradePriceInRubbles { get; set; }
        //Цена 7-ка
        public double Price7Ka { get; set; }

        public Product(int id)
        {
            this.Id = id;
        }
    }
}
