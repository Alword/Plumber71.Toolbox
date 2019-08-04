using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Model
{
    public class ProductDomain
    {
        //id
        public int Id { get; set; }
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
            this.Id = id;
        }

        public override string ToString()
        {
            return $"Id: {Id} Name: {Name} Pieces: {Pieces} Currency: {Currency.ToString()} TotalPrice: {TotalPrice}";
        }
    }
}
