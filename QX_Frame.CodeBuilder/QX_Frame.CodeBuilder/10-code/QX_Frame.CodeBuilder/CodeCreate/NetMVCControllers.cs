using CSharp_FlowchartToCode_DG.QX_Frame.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_FlowchartToCode_DG
{
    public abstract class CodeToControllers
    {
        public static string CreateControllersCode(List<object> CreateInfo)
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
            string BLLclassName = CreateInfo[9].ToString();                          //获得BLL层的类名


            StringBuilder str = new StringBuilder();

            #region 版权信息
            //版权信息
            str.Append(Info.CopyRight);
            str.Append("\r\n");
            #endregion

            //判断是否需要 实例化对象 getForm
            if (MethodInfo[0])
            {
                str.Append("\t" + TableName + " " + TableName + "Object = new " + TableName + "();" + "\r\n");//添加方法介绍
                for (int i = 0; i < FeildName.Count; i++)
                {
                    if (SqlTypeConvert.SqlTypeToLanguageType(CommonVariables.currentDataBaseType, Options.Opt_Language.Net, FeildType[i].Trim()) == "Int32")
                    {
                        str.Append("\t" + TableName + "Object." + FeildName[i].Trim() + "=Convert.ToInt32(Request[\"" + FeildName[i].Trim() + "\"]);" + "\r\n");
                    }
                    else
                    {
                        str.Append("\t" + TableName + "Object." + FeildName[i].Trim() + "=(" + SqlTypeConvert.SqlTypeToLanguageType(CommonVariables.currentDataBaseType, Options.Opt_Language.Net, FeildType[i]) + ")Request[\"" + FeildName[i].Trim() + "\"];" + "\r\n");
                    }
                }
                str.Append("\r\n");
                str.Append("\t" + "//time=DateTime.Now.ToString();" + "\r\n");
                str.Append("\r\n");
            }
            //判断是否需要 实例化对象自定义
            if (MethodInfo[1])
            {
                str.Append("\t" + TableName + " " + TableName + "Object = new " + TableName + "();" + "\r\n");
                for (int i = 0; i < FeildName.Count; i++)
                {
                    str.Append("\t" + TableName + "Object." + FeildName[i].Trim() + "=\"\";" + "\r\n");
                }
                str.Append("\r\n");
                str.Append("\t" + "//time=DateTime.Now.ToString();" + "\r\n");
                str.Append("\r\n");
            }

            //判断是否需要JsonResult
            if (MethodInfo[2])
            {
                str.Append("\t" + "if (true)" + "\r\n");
                str.Append("\t" + "{" + "\r\n");
                str.Append("\t\t" + "return Json(new { TFMark = true, Msg = \"操作成功！\"/*,result=*/ }, JsonRequestBehavior.AllowGet);" + "\r\n");
                str.Append("\t" + "}" + "\r\n");
                str.Append("\t" + "else" + "\r\n");
                str.Append("\t" + "{" + "\r\n");
                str.Append("\t\t" + "return Json(new { TFMark = false, Msg = \"操作失败,请检查信息！\" }, JsonRequestBehavior.AllowGet);" + "\r\n");
                str.Append("\t" + "}" + "\r\n");
                str.Append("\r\n");
            }
            //判断是否需要带Try Catch 的JsonResult
            if (MethodInfo[3])
            {
                str.Append("\t" + "try" + "\r\n");
                str.Append("\t" + "{" + "\r\n");
                str.Append("\t\t" + "if (true)" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "return Json(new { TFMark = true, Msg = \"操作成功！\"/*,result=*/ }, JsonRequestBehavior.AllowGet);" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
                str.Append("\t\t" + "else" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "return Json(new { TFMark = false, Msg = \"操作失败,请检查信息！\" }, JsonRequestBehavior.AllowGet);" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
                str.Append("\t" + "}" + "\r\n");
                str.Append("\t" + "catch(Exception)" + "\r\n");
                str.Append("\t" + "{" + "\r\n");
                str.Append("\t\t" + "return Json(new { TFMark = false, Msg = \"操作失败,请检查信息！\" }, JsonRequestBehavior.AllowGet);" + "\r\n");
                str.Append("\t" + "}" + "\r\n");
            }
            return str.ToString();
        }
    }
}
