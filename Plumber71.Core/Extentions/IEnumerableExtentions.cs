using System;
using System.Collections.Generic;
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
    }
}
