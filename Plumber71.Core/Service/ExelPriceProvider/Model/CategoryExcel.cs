﻿using Plumber71.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Service.ExelPriceProvider.Model
{
    public class ExcelCategory : CategoryAbstraction<ExcelProduct>
    {
        public int Id { get; set; }

        public ExcelCategory(int Id, string Name) : base(Name)
        {
            this.Id = Id;
        }

        public override string ToString()
        {
            return $"Id: {Id} {base.ToString()}";
        }
    }
}
