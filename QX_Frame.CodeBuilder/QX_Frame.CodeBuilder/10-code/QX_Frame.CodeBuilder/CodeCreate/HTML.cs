using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_FlowchartToCode_DG
{
    public abstract class CodeToHTML
    {
        public static string CreateHTMLCode(List<object> CreateInfo)
        {
            string TableName = CreateInfo[0].ToString();                             //表名 即类名
            List<string> FeildName = CreateInfo[1] as List<string>;                  //表字段名称
            List<string> FeildType = CreateInfo[2] as List<string>;                  //表字段类型
            List<string> FeildLength = CreateInfo[3] as List<string>;                //表字段长度
            List<string> FeildIsNullable = CreateInfo[4] as List<string>;            //表字段可空
            List<string> FeildMark = CreateInfo[5] as List<string>;                  //表字段说明
            List<string> FeildIsPK = CreateInfo[6] as List<string>;                  //表字段是否主键
            List<string> FeildIsIdentity = CreateInfo[7] as List<string>;            //表字段是否自增
            Boolean[] MethodInfo = CreateInfo[8] as Boolean[];                       //checkbox选项


            StringBuilder str = new StringBuilder();


            #region 版权信息
            //版权信息
            str.Append(Info.CopyRight);
            str.Append("\r\n");
            #endregion


            //判断是否需要 生成form表单
            if (MethodInfo[0])
            {
                if (FeildName.Count > 0)
                {
                    str.Append("\t" + "<input type=\"hidden\" id=\"ipt_V_mark_DG\" value=\"0\"/>" + "\r\n");
                    str.Append("\t\r\n");
                    for (int i = 0; i < FeildName.Count; i++)
                    {
                        str.Append("\t" + "<label class=\"label_class\">" + FeildName[i].Trim() + ":</label>" + "\r\n");
                        str.Append("\t" + "<input type=\"text\" class=\"ipt_V_DG ipt_V_null_DG\" id=\"t_" + TableName + "_" + FeildName[i].Trim() + "\" name=\"" + FeildName[i].Trim() + "\" placeholder =\"please input "+ FeildName[i].Trim() + "\">" + "\r\n");
                        str.Append("\t" + "<span class=\"sp_V_msg_DG\"></span><br /><br />" + "\r\n");
                        str.Append("\r\n");
                        str.Append("\r\n");
                    }
                    str.Append("\t" + "<input type=\"button\" id=\"ipt_bt_formSubmit\" value=\"提交\"/>" + "\r\n");
                }
                str.Append("\r\n");
            }

            return str.ToString();
        }
    }
}
