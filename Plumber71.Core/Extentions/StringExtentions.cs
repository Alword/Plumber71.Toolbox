using Plumber71.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Extentions
{
    public static class StringExtentions
    {
        public static Currencies ToCurrency(this string text)
        {
            if (text.Contains("RUB")) return Currencies.RUB;
            if (text.Contains("USD")) return Currencies.USD;
            if (text.Contains("EUR")) return Currencies.EUR;
            return Currencies.Unknown;
        }

        public static double ToDouble(this string text)
        {
            if (text.Contains(".")) text = text.Replace(".", ",");
            double.TryParse(text, out double result);
            return result;
        }
    }
}
