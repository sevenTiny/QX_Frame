using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeArts.Arrays
{
    public static class Converter
    {
        public static T[] ToArray_DG<T>(this IList<T> array)
        {
            T[] newArray = new T[array.Count];
            for (int i = 0; i < array.Count; i++)
                newArray[i] = array[i];
            return newArray;
        }
    }
}
