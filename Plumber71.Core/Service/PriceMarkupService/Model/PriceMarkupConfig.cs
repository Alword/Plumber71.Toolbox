using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Service.PriceMarkupService.Model
{
    public class PriceMarkupConfig
    {
        public double GlobalRate { get; set; }
        public Dictionary<string, double> CategoryRate { get; set; }
        public Dictionary<int, double> ProductRate { get; set; }

        public PriceMarkupConfig() => GlobalRate = 1;
    }
}
