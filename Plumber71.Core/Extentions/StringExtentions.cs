using Plumber71.Core.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            text = text.Trim();
            if (text.Contains(",")) text = text.Replace(",", ".");
            var numberStyles = NumberStyles.Any;
            double.TryParse(text, numberStyles, CultureInfo.InvariantCulture, out double result);
            return result;
        }

        public static int ToInt(this string text)
        {
            text = text.Trim();
            int.TryParse(text, out int result);
            return result;
        }
    }
}
