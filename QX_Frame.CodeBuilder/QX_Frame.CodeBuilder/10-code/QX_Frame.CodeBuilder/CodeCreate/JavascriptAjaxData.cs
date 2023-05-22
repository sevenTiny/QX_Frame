using System.Collections.Generic;
using System.Text;

namespace CSharp_FlowchartToCode_DG.CodeCreate
{
    public class JavascriptAjaxData
    {
        public static string CreateCode(Dictionary<string, dynamic> CreateCodeDic)
        {
            string usings = CreateCodeDic["Using"];                                             //Using
            string[] usingsArray = usings.Split(';');
            string NameSpace = CreateCodeDic["NameSpace"];                                      //NameSpace
            string NameSpaceCommonPlus = CreateCodeDic["NameSpaceCommonPlus"];                  //NameSpaceCommonPlus
            string TableName = CreateCodeDic["TableName"];                                      //TableName
            string ClassName = CreateCodeDic["Class"];                                          //ClassName
            string ClassNamePlus = CreateCodeDic["ClassNamePlus"];                              //ClassName Plus
            string ClassNameExtends = CreateCodeDic["ClassExtends"];                             //ClassExtends
            if (!string.IsNullOrEmpty(ClassNameExtends))
            {
                ClassNameExtends = ":" + ClassNameExtends;
            }
            string ClassNameAndExtends = ClassName + ClassNamePlus + ClassNameExtends;          //Class whole name
            List<string> FeildName = CreateCodeDic["FeildName"];                                //表字段名称
            List<string> FeildType = CreateCodeDic["FeildType"];                                //表字段类型
            List<string> FeildLength = CreateCodeDic["FeildLength"];                            //表字段长度
            List<string> FeildIsNullable = CreateCodeDic["FeildIsNullable"];                    //表字段可空
            List<string> FeildDescription = CreateCodeDic["FeildDescription"];                  //表字段说明
            List<string> FeildIsPK = CreateCodeDic["FeildIsPK"];                                //表字段是否主键
            List<string> FeildIsIdentity = CreateCodeDic["FeildIsIdentity"];                    //表字段是否自增


            StringBuilder str = new StringBuilder();

            str.Append("\t" + "$.ajax({" + "\r\n");
            str.Append("\t\t" + "type: 'post'," + "\r\n");
            str.Append("\t\t" + "url: '/api'," + "\r\n");
            str.Append("\t\t" + "async: true,//异步" + "\r\n");
            str.Append("\t\t" + "dataType: 'json'," + "\r\n");
            str.Append("\t\t" + "headers:" + "\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            //str.Append("\t\t\t" + "\"token\":\"\"" + "\r\n");
            str.Append("\t\t" + "}," + "\r\n");
            str.Append("\t\t" + "data:  JSON.stringify(" + "\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            for (int i = 0; i < FeildName.Count; i++)
            {
                if (i < FeildName.Count - 1)
                {
                    str.Append("\t\t\t\"" + FeildName[i].Trim() + "\": \"\"," + "\r\n");
                }
                else
                {
                    str.Append("\t\t\t\"" + FeildName[i].Trim() + "\": \"\"" + "\r\n");
                }
            }
            str.Append("\t\t" + "})," + "\r\n");
            str.Append("\t\t" + "beforeSend: function ()" + "\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + "\r\n");
            str.Append("\t\t" + "}," + "\r\n");
            str.Append("\t\t" + "success: function (data)" + "\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + "console.log(JSON.stringify(data));" + "\r\n");
            str.Append("\t\t" + "}," + "\r\n");
            str.Append("\t\t" + "error: function (data)" + "\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + "console.log(JSON.stringify(data));" + "\r\n");
            str.Append("\t\t" + "}" + "\r\n");
            str.Append("\t" + "});//end Ajax" + "\r\n");

            str.Append("\r\n");

            return str.ToString();
        }
    }
}
