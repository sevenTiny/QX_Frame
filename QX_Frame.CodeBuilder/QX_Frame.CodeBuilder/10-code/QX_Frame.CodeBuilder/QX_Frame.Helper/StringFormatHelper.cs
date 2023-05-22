using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_FlowchartToCode_DG.QX_Frame.Helper
{
    public class StringFormatHelper
    {
        //remove tb_
        private static string RemoveTB_(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.ToLower().Contains("tb_"))
                {
                    return str.Substring(3);
                }
                return str;
            }
            return null;
        }

        //first to lower
        private static string GetFirstLowerStr(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                if (s.Length > 1)
                {
                    return char.ToLower(s[0]) + s.Substring(1);
                }
                return char.ToUpper(s[0]).ToString();
            }
            return null;
        }

    }
}
