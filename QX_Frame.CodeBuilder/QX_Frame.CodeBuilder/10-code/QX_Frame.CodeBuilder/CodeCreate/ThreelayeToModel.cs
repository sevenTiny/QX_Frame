using CSharp_FlowchartToCode_DG.QX_Frame.Helper;
using System.Collections.Generic;
using System.Text;

namespace CSharp_FlowchartToCode_DG
{
    public abstract class ThreelayeToModel
    {
        public static string CreateModelsCode(List<object> CreateInfo)
        {
            string NameSpace = CreateInfo[0].ToString();                             //命名空间
            string TableName = CreateInfo[1].ToString();                             //表名
            List<string> FeildName = CreateInfo[2] as List<string>;                  //表字段名称
            List<string> FeildType = CreateInfo[3] as List<string>;                  //表字段类型
            List<string> FeildLength = CreateInfo[4] as List<string>;                //表字段长度
            List<string> FeildIsNullable = CreateInfo[5] as List<string>;            //表字段可空
            List<string> FeildMark = CreateInfo[6] as List<string>;                  //表字段说明
            List<string> FeildIsPK = CreateInfo[7] as List<string>;                  //表字段是否主键
            List<string> FeildIsIdentity = CreateInfo[8] as List<string>;            //表字段是否自增
            string className = CreateInfo[9].ToString();                             //获得类名


            StringBuilder str = new StringBuilder();

            str.Append("using System;" + "\r\n");
            str.Append("\r\n");                                                     //空间引用结束换行

            #region 版权信息
            //版权信息
            str.Append(Info.CopyRight);
            str.Append("\r\n");
            #endregion
            
            //添加命名空间
            str.Append("namespace " + NameSpace + "\r\n");
            str.Append("{" + "\r\n");
            //添加实体类
            str.Append("\t" + "/// <summary>" + "\r\n");
            str.Append("\t" + "/// 实体类" + TableName + "（可添加属性说明）" + "\r\n");
            str.Append("\t" + "/// </summary>" + "\r\n");
            str.Append("\t" + "[Serializable]" + "\r\n");
            str.Append("\t" + "public class " + className + "\r\n");
            str.Append("\t" + "{" + "\r\n");
            //添加构造方法
            str.Append("\t\t" + "/// <summary>" + "\r\n");
            str.Append("\t\t" + "/// 构造方法" + "\r\n");
            str.Append("\t\t" + "/// </summary>" + "\r\n");
            str.Append("\t\t" + "public " + className + "()" + "\r\n");
            str.Append("\t\t" + "{}" + "\r\n" + "\r\n");
            //添加私有属性行
            for (int i = 0; i < FeildName.Count; i++)
            {
                str.Append("\t\t" + "private " + SqlTypeConvert.SqlTypeToLanguageType(CommonVariables.currentDataBaseType, Options.Opt_Language.Net, FeildType[i]) + TypeConvert.RT_Nullable(FeildIsNullable[i]) + " _" + FeildName[i].ToLower() + ";\t\t//"+TypeConvert.RT_PK(FeildIsPK[i])+FeildMark[i]+ "\r\n");
            }
            str.Append("\r\n");//换行
            //添加私有属性的公有方法
            for (int i = 0; i < FeildName.Count; i++)
            {
                str.Append("\t\t" + "/// <summary>" + "\r\n" + "\t\t" + "///" + FeildMark[i] + "\r\n" + "\t\t"+"/// </summary>" + "\r\n");//添加方法介绍
                str.Append("\t\t" + "public " + SqlTypeConvert.SqlTypeToLanguageType(CommonVariables.currentDataBaseType, Options.Opt_Language.Net, FeildType[i]) + TypeConvert.RT_Nullable(FeildIsNullable[i]) + " " + FeildName[i] + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "set{_" + FeildName[i].ToLower() + "=value;}" + "\r\n");
                str.Append("\t\t\t" + "get{return _" + FeildName[i].ToLower() + ";}" + "\r\n");
                str.Append("\t\t}" + "\r\n");
            }
            str.Append("\t" + "}" + "\r\n");//public class }
            str.Append("}" + "\r\n");//namespace class }

            return str.ToString();
        }

    }
}
