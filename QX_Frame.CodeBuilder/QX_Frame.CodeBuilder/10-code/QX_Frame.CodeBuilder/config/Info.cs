/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2015-xx-xx xx:xx:xx
 * Modify:2017-4-4 16:39:40     4.2.0
 * Desc:Layout Update
 * Update:2017-9-29 15:11:35    5.0.0
 * Desc:Connect Type Update,MySql Connect Support
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/sevenTiny 
 * Personal web site: http://www.7tiny.com 
 * Technical WebSit: http://www.cnblogs.com/7tiny/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
using System;
using System.Text;

namespace CSharp_FlowchartToCode_DG
{
    public abstract class Info
    {
        //版权信息通用模块
        public const string VersionNum = "5.0.0";         //版本号
        public const string Author = "7tiny";         //作者
        public static readonly string Description = $"7tiny code builder start at:{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}";

        //设置版权信息
        public static string CopyRight
        {
            
            get
            {
                #region 版权信息
                StringBuilder str = new StringBuilder();    //版本信息
                str.Append("/*********************************************************" + "\r\n");
                str.Append(" * CopyRight: 7TINY CODE BUILDER. \r\n");
                str.Append(" * Version: " + VersionNum + "\r\n");
                str.Append(" * Author: " + Author + "\r\n");
                str.Append(" * Address: Earth\r\n");
                str.Append(" * Create: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
                str.Append(" * Modify: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
                str.Append(" * E-mail: dong@7tiny.com | sevenTiny@foxmail.com \r\n");
                str.Append(" * GitHub: https://github.com/sevenTiny \r\n");
                str.Append(" * Personal web site: http://www.7tiny.com \r\n");
                str.Append(" * Technical WebSit: http://www.cnblogs.com/7tiny/ \r\n");
                str.Append(" * Description: \r\n");
                str.Append(" * Thx , Best Regards ~\r\n");
                str.Append(" *********************************************************/" + "\r\n");
                #endregion
                return str.ToString(); ;
            }
        }

    }
}
/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version: 5.0.0
 * Author: qixiao(柒小)
 * Address: Earth
 * Create: 2017-10-10 11:03:01
 * Update: 2017-10-10 11:03:01
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description: 
 * Thx , Best Regards ~
 *********************************************************/

