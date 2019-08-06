using Plumber71.Core.Enums;
using Plumber71.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Model
{
    /// <summary>
    /// Тип продукта для приложения
    /// </summary>
    public class ProductDTO : ProductAbstraction
    {
        public int Id { get => Key; set => Key = value; }
        private double totalPrice;
        public double TotalPrice
        {
            get => totalPrice;
            set => totalPrice = Math.Round(value, 2);
        }

        public ProductDTO() { }

        public override string ToString()
        {
            return $"Sku: {Sku} Name: {Name} Pieces: {Pieces} TotalPrice: {TotalPrice}";
        }
    }
}
