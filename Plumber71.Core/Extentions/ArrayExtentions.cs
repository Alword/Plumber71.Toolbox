using System;
using System.Collections.Generic;
using System.Text;

namespace Plumber71.Core.Extentions
{
    public static class ArrayExtentions
    {
        public static Dictionary<K, T> ConvertToDictionary<K, T>(T[] array, Func<T, K> keyParam)
        {
            int arrayLength = array.Length;
            var dictionary = new Dictionary<K, T>();
            for (int i = 0; i < arrayLength; i++)
            {
                dictionary[keyParam.Invoke(array[i])] = array[i];
            }
            return dictionary;
        }
    }
}
