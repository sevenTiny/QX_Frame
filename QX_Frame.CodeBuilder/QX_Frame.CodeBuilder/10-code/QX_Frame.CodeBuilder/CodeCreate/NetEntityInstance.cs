using CSharp_FlowchartToCode_DG.QX_Frame.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_FlowchartToCode_DG.CodeCreate
{
    class NetEntityInstance
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

            //add filed
            str.Append($"{TableName} {GetFirstLowerStr(RemoveTB_(TableName))} = new {TableName}();\r\n");
            for (int i = 0; i < FeildName.Count; i++)
            {
                str.Append($"{GetFirstLowerStr(RemoveTB_(TableName))}.{FeildName[i]} = default({SqlTypeConvert.SqlTypeToLanguageType(CommonVariables.currentDataBaseType, Options.Opt_Language.Net, FeildType[i])}) ;\r\n");
            }

            return str.ToString();
        }
        public static string CreateCode_otherObject(Dictionary<string, dynamic> CreateCodeDic)
        {
            string usings = CreateCodeDic["Using"];                                             //Using
            string[] usingsArray = usings.Split(';');
            string NameSpace = CreateCodeDic["NameSpace"];                                      //NameSpace
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

            //add filed
            str.Append($"{TableName} {GetFirstLowerStr(RemoveTB_(TableName))} = new {TableName}();\r\n");
            for (int i = 0; i < FeildName.Count; i++)
            {
                str.Append($"{GetFirstLowerStr(RemoveTB_(TableName))}.{FeildName[i]} = otherObject.{FeildName[i]};\r\n");
            }

            return str.ToString();
        }

        //remove tb_
        private static string RemoveTB_(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.ToLower().Contains("tb_"))
                {
                    return str.Substring(3);
                }
                return str;
            }
            return null;
        }

        //first to lower
        private static string GetFirstLowerStr(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                if (s.Length > 1)
                {
                    return char.ToLower(s[0]) + s.Substring(1);
                }
                return char.ToUpper(s[0]).ToString();
            }
            return null;
        }

    }
}
