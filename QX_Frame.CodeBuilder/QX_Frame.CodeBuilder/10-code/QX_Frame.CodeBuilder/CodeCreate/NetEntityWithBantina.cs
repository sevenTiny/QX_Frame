using CSharp_FlowchartToCode_DG.QX_Frame.Helper;
using QX_Frame.Bantina.Extends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_FlowchartToCode_DG.CodeCreate
{
    public class NetEntityWithBantina
    {
        public static string CreateCode(Dictionary<string, dynamic> CreateCodeDic)
        {
            string usings = CreateCodeDic["Using"];                                             //Using
            string[] usingsArray = usings.Split(';');
            string NameSpace = CreateCodeDic["NameSpace"];                                      //NameSpace
            string NameSpaceCommonPlus = CreateCodeDic["NameSpaceCommonPlus"];                  //NameSpaceCommonPlus
            string DataBaseName = CreateCodeDic["DataBaseName"];                                //DataBaseName
            string TableName = CreateCodeDic["TableName"];                                      //TableName
            string ClassName = CreateCodeDic["Class"];                                          //ClassName
            string ClassNamePlus = CreateCodeDic["ClassNamePlus"];                              //ClassName Plus
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
            str.Append("using System;\r\n");
            str.Append("using QX_Frame.App.Base;\r\n");
            str.Append("using QX_Frame.Bantina.Bankinate;\r\n");
            str.Append("\r\n");//引用结束换行

            //添加命名空间
            str.Append($"namespace {NameSpace}{NameSpaceCommonPlus}\r\n");
            str.Append("{" + "\r\n");

            //添加实体类
            str.Append("\t" + "/// <summary>" + "\r\n");
            str.Append("\t" + $"/// public class {ClassName}\r\n");
            str.Append("\t" + "/// </summary>" + "\r\n");
            str.Append("\t" + "[Serializable]" + "\r\n");
            str.Append("\t" + $"[Table(TableName = \"{ClassName}\")]" + "\r\n");
            str.Append("\t" + $"public class {ClassName}: Entity<{DataBaseName}, {ClassName}>\r\n");
            str.Append("\t" + "{" + "\r\n");
            //添加构造方法
            str.Append("\t\t" + "/// <summary>" + "\r\n");
            str.Append("\t\t" + "/// construction method" + "\r\n");
            str.Append("\t\t" + "/// </summary>" + "\r\n");
            str.Append("\t\t" + "public " + ClassName + ClassNamePlus + "(){}" + "\r\n");
            str.Append("\r\n");

            //add filed
            for (int i = 0; i < FeildName.Count; i++)
            {
                //add description
                str.Append("\t\t" + $"//{ TypeConvert.RT_PK(FeildIsPK[i])} {FeildDescription[i]}" + "\r\n");

                if (FeildIsPK[i].ToInt() == 1)
                {
                    str.Append($"\t\t[Key]\r\n");
                }
                else
                {
                    str.Append($"\t\t[Column]\r\n");
                }

                if (FeildIsIdentity[i].ToInt() == 1)
                {
                    str.Append($"\t\t[AutoIncrease]\r\n");
                }

                string IsNull = SqlTypeConvert.SqlTypeToLanguageType(CommonVariables.currentDataBaseType, Options.Opt_Language.Net, FeildType[i]).Equals("String") ? "" : TypeConvert.RT_Nullable(FeildIsNullable[i]);
                str.Append("\t\t" + $"public {SqlTypeConvert.SqlTypeToLanguageType(CommonVariables.currentDataBaseType, Options.Opt_Language.Net, FeildType[i])}{IsNull} {FeildName[i]} {"{ get;set; }"}" + "\r\n");
            }

            str.Append("\t" + "}" + "\r\n");//public class }
            str.Append("}" + "\r\n");//namespace class }

            return str.ToString();
        }
    }
}
