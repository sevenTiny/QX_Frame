using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * author:qixiao
 * create:2017-5-16 16:02:34
 * */
namespace QX_Frame.Bantina
{
    public class Code_Helper_DG
    {
        /// <summary>
        /// 取得当前源码的哪一行
        /// </summary>
        /// <returns></returns>
        public static int GetLineNum()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(0).GetFileLineNumber();
        }

        /// <summary>
        /// 取当前源码的源文件名
        /// </summary>
        /// <returns></returns>
        public static string GetCurSourceFileName()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);

            return st.GetFrame(0).GetFileName();

        }
    }
}
