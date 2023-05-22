using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_FlowchartToCode_DG.CodeCreate
{
    public class QX_FrameToDataService
    {
        public static string CreateCode(Dictionary<string, dynamic> CreateCodeDic)
        {
            string usings = CreateCodeDic["Using"];                                             //Using
            string[] usingsArray = usings.Split(';');
            string NameSpace = CreateCodeDic["NameSpace"];                                      //NameSpace
            string NameSpaceCommonPlus = CreateCodeDic["NameSpaceCommonPlus"];                  //NameSpaceCommonPlus
            string TableName = CreateCodeDic["TableName"];                                      //TableName
            string ClassName = CreateCodeDic["Class"];                                          //ClassName
            string ClassNamePlus = CreateCodeDic["ClassNamePlus"];                               //ClassName Plus
            string ClassNameExtends = CreateCodeDic["ClassExtends"];                            //ClassExtends
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

            //foreach (var item in usingsArray)
            //{
            //    if (usingsArray.Last().Equals(item))
            //    {
            //        break;
            //    }
            //    str.Append($"{item};\r\n");
            //}


            #region 版权信息
            //版权信息
            str.Append(Info.CopyRight);
            str.Append("\r\n");
            #endregion

            //添加using
            str.Append($"{usings}\r\n");
            str.Append("\r\n");//引用结束换行

            //添加命名空间
            str.Append($"namespace {NameSpace}{NameSpaceCommonPlus}\r\n");
            str.Append("{" + "\r\n");

            //添加实体类
            str.Append("\t" + "/// <summary>" + "\r\n");
            str.Append("\t" + "/// class " + ClassName + ClassNamePlus + "\r\n");
            str.Append("\t" + "/// </summary>" + "\r\n");
            str.Append("\t" + $"public class {ClassNameAndExtends}\r\n");
            str.Append("\t" + "{" + "\r\n");
            //add private member
            str.Append("\t\t" + $"private {TableName} _{TableName};" + "\r\n");
            //添加构造方法
            str.Append("\t\t" + "/// <summary>" + "\r\n");
            str.Append("\t\t" + "/// construction method" + "\r\n");
            str.Append("\t\t" + "/// </summary>" + "\r\n");
            str.Append("\t\t" + "public " + ClassName + ClassNamePlus + "()" + "\r\n");
            str.Append("\t\t" + "{}" + "\r\n" + "\r\n");
            //add method
            str.Append("\t\t" + "public " + ClassName + ClassNamePlus + $"({TableName} {TableName})" + "\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + $"this._{TableName} = {TableName};" + "\r\n");
            str.Append("\t\t" + "}" + "\r\n");

            str.Append("\t\t" + $"public bool Add({TableName} {TableName})" + "\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + $"return {TableName}.Add({TableName});" + "\r\n");
            str.Append("\t\t" + "}" + "\r\n");

            str.Append("\t\t" + $"public bool Update({TableName} {TableName})" + "\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + $"return {TableName}.Update({TableName});" + "\r\n");
            str.Append("\t\t" + "}" + "\r\n");

            str.Append("\t\t" + $"public bool Delete({TableName} {TableName})" + "\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + $"return {TableName}.Delete({TableName});" + "\r\n");
            str.Append("\t\t" + "}" + "\r\n");


            str.Append("\t" + "}" + "\r\n");//public class }
            str.Append("}" + "\r\n");//namespace class }

            return str.ToString();
        }
    }
}
