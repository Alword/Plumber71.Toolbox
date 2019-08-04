using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Model
{
    /// <summary>
    /// Тип продукта для приложения
    /// </summary>
    public class ProductDomain
    {
        //id
        public int Sku { get; set; }
        //название stringi
        public string Name { get; set; }
        //штуки
        public int Pieces { get; set; }
        //Валюта
        public Currencies Currency { get; set; }

        public double TotalPrice { get; set; }

        public ProductDomain() { }
        public ProductDomain(int id)
        {
            this.Sku = id;
        }

        public override string ToString()
        {
            return $"Sku: {Sku} Name: {Name} Pieces: {Pieces} Currency: {Currency.ToString()} TotalPrice: {TotalPrice}";
        }
    }
}
