using Plumber71.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Extentions
{
    public static class ObjectsExtensions
    {
        public static Currencies ToCurrency(this object obj)
        {
            string text = obj.ToString();
            return text.ToCurrency();
        }

        public static double ToDouble(this object obj)
        {
            string text = obj.ToString();
            return text.ToDouble();
        }

        public static int ToInt(this object obj)
        {
            string text = obj.ToString();
            return text.ToInt();
        }
    }
}
