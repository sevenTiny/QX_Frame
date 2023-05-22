using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_FlowchartToCode_DG.CodeCreate
{
    /**
    * author:qixiao
    * time:2017年4月3日12:32:14
    * */
    public class SqlServerSqlStatement
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


            str.Append("\r\n");//引用结束换行

            str.Append("\r\n");
            str.Append("//sql statements\r\n");
            str.Append("\r\n");

            #region Add
            str.Append("//Add ->\r\n\r\n");
            str.Append($"INSERT INTO {TableName} (");
            for (int i = 0; i < FeildName.Count; i++)
            {
                if (FeildIsIdentity[i].Trim() != "1")
                {
                    str.Append(FeildName[i].Trim());
                    if (i < FeildName.Count - 1)
                    {
                        str.Append(",");
                    }
                }
                if (i == FeildName.Count - 1)
                {
                    str.Append(")");
                }
            }
            str.Append(" VALUES (");
            for (int i = 0; i < FeildName.Count; i++)
            {
                if (FeildIsIdentity[i].Trim() != "1")
                {
                    str.Append("@" + FeildName[i].Trim());
                    if (i < FeildName.Count - 1)
                    {
                        str.Append(",");
                    }
                }
                if (i == FeildName.Count - 1)
                {
                    str.Append(")");
                }
            }
            str.Append("\r\n");
            #endregion

            #region Delete
            str.Append("\r\n//Delete ->\r\n\r\n");

            str.Append($"DELETE FROM {TableName} WHERE {FeildName[0].Trim()} = @{FeildName[0].Trim()}");

            str.Append("\r\n");

            #endregion

            #region Update
            str.Append("\r\n//Update ->\r\n\r\n");

            str.Append($"UPDATE {TableName} SET ");
            for (int i = 0; i < FeildName.Count; i++)
            {
                if (FeildIsIdentity[i].Trim() != "1")
                {
                    str.Append($"{FeildName[i].Trim()}= @{FeildName[i].Trim()}");
                    if (i < FeildName.Count - 1)
                    {
                        str.Append(",");
                    }
                }
            }
            str.Append($" WHERE {FeildName[0].Trim()} = @{FeildName[0].Trim()}");

            str.Append("\r\n");
            #endregion

            #region Select
            str.Append("\r\n//Select ->\r\n\r\n");

            //query count
            str.Append("\r\n//query count:\r\n\r\n");

            str.Append($"select count(0) from {TableName} WHERE 1=1");

            str.Append("\r\n");

            //query top 1
            str.Append("\r\n//query single:\r\n\r\n");

            str.Append($"SELECT TOP (1) * FROM {TableName} WHERE {FeildName[0].Trim()} = @{FeildName[0].Trim()}");

            str.Append("\r\n");

            //query all
            str.Append("\r\n//query all:\r\n\r\n");

            str.Append($"SELECT * FROM {TableName} WHERE 1=1");

            str.Append("\r\n");

            //query all paging
            str.Append("\r\n//query paging by RowNumber:\r\n\r\n");

            str.Append($"SELECT TOP \" + PageSize + \" * FROM (SELECT ROW_NUMBER() OVER (ORDER BY \" + DataOrderBy + \") AS RowNumber,* FROM {TableName} WHERE 1=1  ) AS T  WHERE RowNumber > (\" + PageSize + \"*(\" + PageNumber + \"-1))");

            str.Append("\r\n");

            #endregion

            #region SqlParameters
            str.Append("\r\n//SqlParameters ->\r\n\r\n");

            str.Append("SqlParameter[] parms = new SqlParameter[]{" + "\r\n");
            for (int i = 0; i < FeildName.Count; i++)
            {
                if (FeildIsIdentity[i].Trim() != "1")
                {
                    str.Append("\t\t" + $"new SqlParameter(\"@{FeildName[i].Trim()}\",{TableName}.{FeildName[i].Trim()})," + "\r\n");
                }
            }
            str.Append("" + "};" + "\r\n");
            str.Append("\r\n");
            #endregion

            #region MySqlParameters
            str.Append("\r\n//MySqlParameters ->\r\n\r\n");

            str.Append("MySqlParameter[] parms = new MySqlParameter[]{" + "\r\n");
            for (int i = 0; i < FeildName.Count; i++)
            {
                if (FeildIsIdentity[i].Trim() != "1")
                {
                    str.Append("\t\t" + $"new MySqlParameter(\"@{FeildName[i].Trim()}\",{TableName}.{FeildName[i].Trim()})," + "\r\n");
                }
            }
            str.Append("" + "};" + "\r\n");
            str.Append("\r\n");
            #endregion

            #region OracleParameters
            str.Append("\r\n//OracleParameters ->\r\n\r\n");

            str.Append("OracleParameter[] parms = new OracleParameter[]{" + "\r\n");
            for (int i = 0; i < FeildName.Count; i++)
            {
                if (FeildIsIdentity[i].Trim() != "1")
                {
                    str.Append("\t\t" + $"new OracleParameter(\"@{FeildName[i].Trim()}\",{TableName}.{FeildName[i].Trim()})," + "\r\n");
                }
            }
            str.Append("" + "};" + "\r\n");
            str.Append("\r\n");
            #endregion

            #region Dictionary
            str.Append("\r\n//Dictionary ->\r\n\r\n");

            str.Append("Dictionary<string,object> dictionary = new Dictionary<string,object> {" + "\r\n");
            for (int i = 0; i < FeildName.Count; i++)
            {
                if (FeildIsIdentity[i].Trim() != "1")
                {
                    str.Append("\t\t" + $"{{\"@{FeildName[i].Trim()}\",{TableName}.{FeildName[i].Trim()})}}," + "\r\n");
                }
            }
            str.Append("" + "};" + "\r\n");
            str.Append("\r\n");
            #endregion

            return str.ToString();
        }
    }
}