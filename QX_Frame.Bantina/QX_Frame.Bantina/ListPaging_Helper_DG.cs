using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QX_Frame.Bantina
{
    /// <summary>
    /// author:qixiao
    /// create 20161030 14:27:23
    /// </summary>
    public static class ListPaging_Helper_DG
    {
        public static List<T> ListPaging<T>(this List<T> list, int pageIndex, int pageSize)
        {
            List<T> newList = new List<T>();
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            if (pageIndex > 0)
            {
                newList = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                newList = list.Skip(0).Take(pageSize).ToList();
            }
            return newList;
        }
        public static List<T> ListPaging<T>(this List<T> list, int pageIndex, int pageSize, out int Count)
        {
            List<T> newList = new List<T>();
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            if (pageIndex > 0)
            {
                newList = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                newList = list.Skip(0).Take(pageSize).ToList();
            }
            Count = newList.Count;
            return newList;
        }
    }
}
