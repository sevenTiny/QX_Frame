using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_FlowchartToCode_DG
{
    public abstract class CodeToJavascript
    {
        public static string CreateJavaScriptCode(List<object> CreateInfo)
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


            //判断是否需要 Get代码
            if (MethodInfo[0])
            {
                str.Append("\t" + "$.get('/控制器/方法', function (data) {" + "\r\n");
                str.Append("\t\t" + "//get数据处理的方法" + "\r\n");
                str.Append("\t" + "});" + "\r\n");

                str.Append("\r\n");
            }
            //判断是否需要 Post代码
            if (MethodInfo[1])
            {
                str.Append("\t" + "$.post('/控制器/方法'," + "\r\n");
                str.Append("\t\t" + "{ : , :  }," + "\r\n");
                str.Append("\t\t" + "function (response) {" + "\r\n");
                str.Append("\t\t" + "//post数据处理方法" + "\r\n");
                str.Append("\t" + "});" + "\r\n");

                str.Append("\r\n");
            }

            //判断是否需要 Ajax 代码
            if (MethodInfo[2])
            {
                str.Append("\t" + "$.ajax({" + "\r\n");
                str.Append("\t\t" + "type: 'post'," + "\r\n");
                str.Append("\t\t" + "url: '/控制器/方法'," + "\r\n");
                str.Append("\t\t" + "async: true,//异步" + "\r\n");
                str.Append("\t\t" + "data: {'':''}," + "\r\n");
                str.Append("\t\t" + "dataType: 'json'," + "\r\n");
                str.Append("\t\t" + "beforeSend: function () {" + "\r\n");
                str.Append("\t\t\t" + "//发送前事件" + "\r\n");
                str.Append("\t\t" + "}," + "\r\n");
                str.Append("\t\t" + "success: function (data) {" + "\r\n");
                str.Append("\t\t\t" + "//请求成功部分代码" + "\r\n");
                str.Append("\t\t\t" + "if (data.TFMark) {" + "\r\n");
                str.Append("\t\t\t\t" + "" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t\t" + " else if (!data.TFMark) {" + "\r\n");
                str.Append("\t\t\t" + "//alert(data.Msg)" + "\r\n");
                str.Append("\t\t\t\t" + "" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t" + "}," + "\r\n");
                str.Append("\t\t" + "error: function () {" + "\r\n");
                str.Append("\t\t\t" + "//请求失败部分代码" + "\r\n");
                str.Append("\t\t\t" + "//alert(data.Msg)" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
                str.Append("\t" + "})" + "\r\n");


                str.Append("\r\n");
            }

            //判断是否需要 带Model的Post代码
            if (MethodInfo[3])
            {
                if (FeildName.Count > 0)
                {
                    str.Append("\t" + "$.post('/控制器/方法'," + "\r\n");
                    str.Append("\t\t" + "{ " + "\r\n");
                    for (int i = 0; i < FeildName.Count; i++)
                    {
                        if (i<FeildName.Count-1)
                        {
                            str.Append("\t\t\t" + FeildName[i].Trim() + ": $(\"#t_" + TableName + "_" + FeildName[i].Trim() + "\").val()," + "\r\n");
                        }
                        else
                        {
                            str.Append("\t\t\t" + FeildName[i].Trim() + ": $(\"#t_" + TableName + "_" + FeildName[i].Trim() + "\").val()" + "\r\n");
                        }
                        
                    }
                    str.Append("\t\t" + "}," + "\r\n");
                    str.Append("\t\t" + "function (response) {" + "\r\n");
                    str.Append("\t\t" + "//post数据处理方法" + "\r\n");
                    str.Append("\t" + "});" + "\r\n");
                }
                str.Append("\r\n");
            }

            //判断是否需要 带Model的Ajax 代码
            if (MethodInfo[4])
            {
                str.Append("\t" + "$(\"#ipt_bt_formSubmit\").click(function () {" + "\r\n");
                str.Append("\t" + "//判断是否全部通过验证" + "\r\n");
                str.Append("\t" + "if ($(\"#ipt_V_mark_DG\").val() == \"1\") {" + "\r\n");
                str.Append("\t" + "//验证成功" + "\r\n");

                str.Append("\t" + "$.ajax({" + "\r\n");
                str.Append("\t\t" + "type: 'post'," + "\r\n");
                str.Append("\t\t" + "url: '/控制器/方法'," + "\r\n");
                str.Append("\t\t" + "async: true,//异步" + "\r\n");
                str.Append("\t\t" + "data: {" + "\r\n");
                for (int i = 0; i < FeildName.Count; i++)
                {
                    if (i < FeildName.Count - 1)
                    {
                        str.Append("\t\t\t" + FeildName[i].Trim() + ": $(\"#t_" + TableName + "_" + FeildName[i].Trim() + "\").val()," + "\r\n");
                    }
                    else
                    {
                        str.Append("\t\t\t" + FeildName[i].Trim() + ": $(\"#t_" + TableName + "_" + FeildName[i].Trim() + "\").val() " + "\r\n");
                    }

                }
                str.Append("\t\t" + "}," + "\r\n");
                str.Append("\t\t" + "dataType: 'json'," + "\r\n");
                str.Append("\t\t" + "beforeSend: function () {" + "\r\n");
                str.Append("\t\t\t" + "//发送前事件" + "\r\n");
                str.Append("\t\t" + "}," + "\r\n");
                str.Append("\t\t" + "success: function (data) {" + "\r\n");
                str.Append("\t\t\t" + "//请求成功部分代码" + "\r\n");
                str.Append("\t\t\t" + "if (data.TFMark) {" + "\r\n");
                str.Append("\t\t\t\t" + "//alert(data.Msg);" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t\t" + " else if (!data.TFMark) {" + "\r\n");
                str.Append("\t\t\t" + "//alert(data.Msg);" + "\r\n");
                str.Append("\t\t\t\t" + "" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t" + "}," + "\r\n");
                str.Append("\t\t" + "error: function () {" + "\r\n");
                str.Append("\t\t\t" + "//请求失败部分代码" + "\r\n");
                str.Append("\t\t\t" + "//alert(data.Msg)" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
                str.Append("\t" + "});//end Ajax" + "\r\n");

                str.Append("\t" + "}" + "\r\n");
                str.Append("\t" + "else {" + "\r\n");
                str.Append("\t" + " $(\".ipt_V_DG\").ipt_V_all_DG();" + "\r\n");
                str.Append("\t" + "//alert(\"您输入的信息有误！\");" + "\r\n");
                str.Append("\t" + "}" + "\r\n");
                str.Append("\t" + " });//end formSubmit click" + "\r\n");

                str.Append("\r\n");
            }



            //判断是否需要 jQuery框架
            if (MethodInfo[5])
            {
                str.Append("\t" + "jQuery(function ($) {" + "\r\n");
                str.Append("\t\t" + "$(document).ready(function (e) {" + "\r\n");
                str.Append("\t\t" + "" + "\r\n");
                str.Append("\t\t" + "" + "\r\n");
                str.Append("\t\t" + "});" + "\r\n");
                str.Append("\t" + "});" + "\r\n");

                str.Append("\r\n");
            }

            return str.ToString();
        }
    }
}
