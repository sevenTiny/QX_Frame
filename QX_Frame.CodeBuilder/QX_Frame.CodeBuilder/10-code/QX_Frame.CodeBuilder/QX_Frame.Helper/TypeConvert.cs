/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:5.0.0
 * Author:qixiao(柒小)
 * Create:Unknown
 * Update:2017-09-30 09:48:03
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
using System;

namespace CSharp_FlowchartToCode_DG.QX_Frame.Helper
{
    public abstract class TypeConvert
    {
        public static string RT_Nullable(string str)
        {
            switch (Int32.Parse(str))
            {
                case 0:
                    return "";
                case 1:
                    return "?";
                default:
                    return "";
            }
        }
        public static string RT_PK(string str)
        {
            switch (Int32.Parse(str))
            {
                case 0:
                    return "";
                case 1:
                    return " PK（identity） ";
                default:
                    return "";
            }
        }
        public static string RT_PK_Attribute(string str)
        {
            switch (Int32.Parse(str))
            {
                case 0:
                    return "";
                case 1:
                    return "[Key]";
                default:
                    return "";
            }
        }        
    }
}
