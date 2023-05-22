using CSharp_FlowchartToCode_DG.QX_Frame.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_FlowchartToCode_DG.CodeCreate
{
    public class QX_FrameToQueryObject
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
            string ClassNameExtends= CreateCodeDic["ClassExtends"];                             //ClassExtends
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
            str.Append("\t" + "///class " + ClassName+ClassNamePlus + "\r\n");
            str.Append("\t" + "/// </summary>" + "\r\n");
            str.Append("\t" + $"public class {ClassNameAndExtends}\r\n");
            str.Append("\t" + "{" + "\r\n");
            //添加构造方法
            str.Append("\t\t" + "/// <summary>" + "\r\n");
            str.Append("\t\t" + "/// construction method" + "\r\n");
            str.Append("\t\t" + "/// </summary>" + "\r\n");
            str.Append("\t\t" + "public " + ClassName+ClassNamePlus + "()" + "\r\n");
            str.Append("\t\t" + "{}" + "\r\n" + "\r\n");
            //add filed
            for (int i = 0; i < FeildName.Count; i++)
            {
                string IsNull = SqlTypeConvert.SqlTypeToLanguageType(CommonVariables.currentDataBaseType, Options.Opt_Language.Net, FeildType[i]).Equals("String") ? "" : TypeConvert.RT_Nullable(FeildIsNullable[i]);
                str.Append("\t\t" + $"//{ TypeConvert.RT_PK(FeildIsPK[i])} {FeildDescription[i]}" + "\r\n");
                str.Append("\t\t" + $"public {SqlTypeConvert.SqlTypeToLanguageType(CommonVariables.currentDataBaseType, Options.Opt_Language.Net, FeildType[i])}{IsNull} {FeildName[i]} {"{ get;set; }"}" + "\r\n");
                str.Append("\r\n");//换行
            }

            str.Append("\t\t" + "//query condition // null default" + "\r\n");
            str.Append("\t\t" + $"public override Expression<Func<{TableName}, bool>> QueryCondition {{get {{ return base.QueryCondition; }} set {{ base.QueryCondition = value; }} }}" + "\r\n");
            str.Append("\r\n");//换行

            str.Append("\t\t" + "//query condition func // true default //if QueryCondition != null this will be override !!!" + "\r\n");
            str.Append("\t\t" + $"protected override Expression<Func<{TableName}, bool>> QueryConditionFunc()" + "\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + $"Expression<Func<{TableName}, bool>> func = t => true;" + "\r\n");
            str.Append("\r\n");//换行
            str.Append("\t\t\t" + $"if (!string.IsNullOrEmpty(\"\"))" + "\r\n");
            str.Append("\t\t\t" + "{" + "\r\n");
            str.Append("\t\t\t\t" + $"func = func.And(t => true);" + "\r\n");
            str.Append("\t\t\t" + "}" + "\r\n");
            str.Append("\r\n");//换行
            str.Append("\t\t\t" + $"return func;" + "\r\n");
            str.Append("\t\t" + "}" + "\r\n");



            str.Append("\t" + "}" + "\r\n");//public class }
            str.Append("}" + "\r\n");//namespace class }

            return str.ToString();
        }
    }
}
