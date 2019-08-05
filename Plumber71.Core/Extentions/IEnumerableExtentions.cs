using Plumber71.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plumber71.Core.Extentions
{
    public static class IEnumerableExtentions
    {
        public static Dictionary<K, T> ConvertToDictionary<K, T>(this IEnumerable<T> array, Func<T, K> keyParam)
        {
            var dictionary = new Dictionary<K, T>();
            foreach (var elem in array)
            {
                dictionary[keyParam.Invoke(elem)] = elem;
            }
            return dictionary;
        }

        public static IEnumerable<T> AsProductsIEnumerable<T>(this IEnumerable<CategoryAbstraction<T>> categoryAbstractions) 
            where T : ProductAbstraction
        {
            return from category in categoryAbstractions
                   from product in category.Products
                   select product;
        }
    }
}
