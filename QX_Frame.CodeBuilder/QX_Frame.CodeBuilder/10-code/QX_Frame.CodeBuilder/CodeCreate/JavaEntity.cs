using CSharp_FlowchartToCode_DG.QX_Frame.Helper;
using System.Collections.Generic;
using System.Text;

namespace CSharp_FlowchartToCode_DG.CodeCreate
{
    public class JavaEntity
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
            string ClassNameExtends = CreateCodeDic["ClassExtends"];                            //ClassExtends
            string ClassNameAndExtends = ClassName + ClassNamePlus + ClassNameExtends;          //Class whole name
            List<string> FeildName = CreateCodeDic["FeildName"];                                //表字段名称
            List<string> FeildType = CreateCodeDic["FeildType"];                                //表字段类型
            List<string> FeildLength = CreateCodeDic["FeildLength"];                            //表字段长度
            List<string> FeildIsNullable = CreateCodeDic["FeildIsNullable"];                    //表字段可空
            List<string> FeildDescription = CreateCodeDic["FeildDescription"];                  //表字段说明
            List<string> FeildIsPK = CreateCodeDic["FeildIsPK"];                                //表字段是否主键
            List<string> FeildIsIdentity = CreateCodeDic["FeildIsIdentity"];                    //表字段是否自增

            StringBuilder str = new StringBuilder();

            #region 版权信息
            //版权信息
            str.Append(Info.CopyRight);
            str.Append("\r\n");
            #endregion

            //添加命名空间
            str.Append($"package {NameSpace}{NameSpaceCommonPlus}\r\n");
            str.Append("\r\n");

            //添加using
            str.Append($"{usings}\r\n");
            str.Append("\r\n");//引用结束换行

            //添加实体类
            str.Append($"public class {ClassName}{{\r\n");
            //添加构造方法
            str.Append("\t" + "/*" + "\r\n");
            str.Append("\t" + " * construction method" + "\r\n");
            str.Append("\t" + " */" + "\r\n");
            str.Append("\t" + "public " + ClassName + ClassNamePlus + "(){}" + "\r\n");
            //add filed
            for (int i = 0; i < FeildName.Count; i++)
            {
                str.Append("\t" + $"//{ TypeConvert.RT_PK(FeildIsPK[i])} {FeildDescription[i]}" + "\r\n");
                str.Append("\t" + $"private {SqlTypeConvert.SqlTypeToLanguageType(CommonVariables.currentDataBaseType,Options.Opt_Language.Java,FeildType[i])} {FeildName[i]};" + "\r\n");
            }
            str.Append("\r\n");

            for (int i = 0; i < FeildName.Count; i++)
            {
                str.Append("\r\n");
                str.Append("\t" + $"public {SqlTypeConvert.SqlTypeToLanguageType(CommonVariables.currentDataBaseType,Options.Opt_Language.Java,FeildType[i])} get{FeildName[i]}() {{" + "\r\n");
                str.Append("\t\t" + $"return this.{FeildName[i]};" + "\r\n");
                str.Append("\t" + "}" + "\r\n");
                str.Append("\t" + $"public void set{FeildName[i]}({SqlTypeConvert.SqlTypeToLanguageType(CommonVariables.currentDataBaseType,Options.Opt_Language.Java,FeildType[i])} {FeildName[i]}) {{" + "\r\n");
                str.Append("\t\t" + $"this.{FeildName[i]}={FeildName[i]};" + "\r\n");
                str.Append("\t" + "}" + "\r\n");

            }

            str.Append("}" + "\r\n");//public class }

            return str.ToString();
        }
    }
}
