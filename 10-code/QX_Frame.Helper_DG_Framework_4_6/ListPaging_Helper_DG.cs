using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QX_Frame.Helper_DG_Framework
{
    /// <summary>
    /// 20161030 14:27:23 qixiao
    /// </summary>
    public abstract class ListPaging_Helper_DG
    {
        public static List<T> ListPaging<T>(List<T> list, int pageIndex, int pageSize, out int Count)
        {
                List<T> newList = new List<T>();
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
