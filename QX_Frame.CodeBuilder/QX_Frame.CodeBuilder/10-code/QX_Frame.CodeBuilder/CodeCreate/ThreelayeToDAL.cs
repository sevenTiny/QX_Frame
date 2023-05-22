using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_FlowchartToCode_DG
{
    public abstract class ThreelayeToDAL
    {
        public static string CreateDALCode(List<object> CreateInfo)
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
            Boolean[] MethodInfo = CreateInfo[9] as Boolean[];
            string className = CreateInfo[10].ToString();                             //获得类名


            StringBuilder str = new StringBuilder();


            str.Append("using Model;" + "\r\n");
            str.Append("using Helper_DG_Framework4_5;" + "\r\n");
            str.Append("using System;" + "\r\n");
            str.Append("using System.Collections.Generic;" + "\r\n");
            str.Append("using System.Data;" + "\r\n");
            str.Append("using System.Data.SqlClient;" + "\r\n");
            str.Append("using System.Text;" + "\r\n");
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
            str.Append("\t" + "/// 实体类" + className + "（可添加属性说明）" + "\r\n");
            str.Append("\t" + "/// </summary>" + "\r\n");
            str.Append("\t" + "public class " + className + "" + "\r\n");
            str.Append("\t" + "{" + "\r\n");

            //判断是否需要返回表总数
            if (MethodInfo[4])
            {
                #region 表总数
                str.Append("\t\t" + "/// <summary>" + "\r\n");
                str.Append("\t\t" + "/// 计算当前表内的符合条件的所有数据的数量" + "\r\n");
                str.Append("\t\t" + "/// </summary>" + "\r\n");
                str.Append("\t\t" + "/// <param name=\"safeSqlConditionInBLL\">安全的sql条件语句,从BLL层获取</param>" + "\r\n");
                str.Append("\t\t" + "/// <returns></returns>" + "\r\n");
                str.Append("\t\t" + "public int DataCount(string safeSqlConditionInBLL = \"\")" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "try" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                //sql语句部分
                str.Append("\t\t\t\t" + "string commandText = \"select count(0) from "+TableName+" WHERE \" + safeSqlConditionInBLL;" + "\r\n");
                str.Append("\t\t\t\t" + "return Convert.ToInt32(SqlHelper_DG.ExecuteScalar(SqlHelper_DG.ConnString, commandText));" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t\t" + "catch (Exception)" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                str.Append("\t\t\t\t" + "return default(int);" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
                #endregion
            }
            //判断是否需要返回是否存在
            if (MethodInfo[5])
            {
                #region 是否存在
                str.Append("\t\t" + "/// <summary>" + "\r\n");
                str.Append("\t\t" + "/// 检测是否存在条件所指示的数据------------这个方法需要按需求来修改条件，不能盲目使用！！！" + "\r\n");
                str.Append("\t\t" + "/// </summary>" + "\r\n");
                str.Append("\t\t" + "/// <param name=\""+TableName+"Object\">从对象中提取中要查找的字段是否存在(对象方式是防止数据注入！)</param>" + "\r\n");
                str.Append("\t\t" + "/// <returns></returns>" + "\r\n");
                str.Append("\t\t" + "public Boolean IsExistWhereFeild(" + TableName + " " + TableName + "Object)" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "try" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                if (FeildName.Count > 0)
                {
                    //sql语句部分
                    str.Append("\t\t\t\t" + "string commandText = \"select count(0) from " + TableName);
                    str.Append(" WHERE ");
                    str.Append(FeildName[1].Trim() + "=@" + FeildName[1].Trim());
                    str.Append("\";" + "\r\n");
                    str.Append("\t\t\t\t" + "SqlParameter[] parms = new SqlParameter[]{" + "\r\n");
                    str.Append("\t\t\t\t" + "new SqlParameter(\"@" + FeildName[1].Trim() + "\"," + TableName + "Object." + FeildName[1].Trim() + ")," + "\r\n");
                    str.Append("\t\t\t\t" + "};" + "\r\n");
                    str.Append("\t\t\t\t" + "return Convert.ToInt32(SqlHelper_DG.ExecuteScalar(SqlHelper_DG.ConnString, commandText,CommandType.Text,parms)) > 0 ? true : false;" + "\r\n");
                }
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t\t" + "catch (Exception)" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                str.Append("\t\t\t\t" + "return false;" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
                #endregion
            }

            //判断是否需要增加语句 Add
            if (MethodInfo[0])
            {
                #region 增加
                str.Append("\t\t" + "/// <summary>" + "\r\n");
                str.Append("\t\t" + "/// Insert插入语句，返回插入的结果：成功true，失败false" + "\r\n");
                str.Append("\t\t" + "/// </summary>" + "\r\n");
                str.Append("\t\t" + "/// <param name=\""+TableName+"Object\">要插入的对象，屏蔽掉自增的字段</param>" + "\r\n");
                str.Append("\t\t" + "/// <returns></returns>" + "\r\n");
                str.Append("\t\t" + "public Boolean IsInsert(" + TableName + " " + TableName + "Object)" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "try" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                //sql语句部分
                str.Append("\t\t\t\t" + "string commandText = \"INSERT INTO " + TableName + " ( ");
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
                str.Append("\";" + "\r\n");


                str.Append("\t\t\t\t" + "SqlParameter[] parms = new SqlParameter[]{" + "\r\n");
                for (int i = 0; i < FeildName.Count; i++)
                {
                    if (FeildIsIdentity[i].Trim() != "1")
                    {
                        str.Append("\t\t\t\t" + "new SqlParameter(\"@" + FeildName[i].Trim() + "\"," + TableName + "Object." + FeildName[i].Trim() + ")," + "\r\n");
                    }
                }
                str.Append("\t\t\t\t" + "};" + "\r\n");
                str.Append("\t\t\t\t" + "return SqlHelper_DG.ExecuteNonQuery(SqlHelper_DG.ConnString, commandText, CommandType.Text, parms) > 0 ? true : false;" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t\t" + "catch (Exception)" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                str.Append("\t\t\t\t" + "return false;" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
                #endregion
            }
            //判断是否需要修改语句 Update
            if (MethodInfo[1])
            {
                #region 修改
                //update 
                str.Append("\t\t" + "/// <summary>" + "\r\n");
                str.Append("\t\t" + "/// Update修改语句，返回修改的结果：成功true，失败false" + "\r\n");
                str.Append("\t\t" + "/// </summary>" + "\r\n");
                str.Append("\t\t" + "/// <param name=\""+TableName+"Object\">要修改的结果对象，屏蔽掉自增的列，条件可修改</param>" + "\r\n");
                str.Append("\t\t" + "/// <returns></returns>" + "\r\n");
                str.Append("\t\t" + "public Boolean IsUpdate(" + TableName + " " + TableName + "Object)" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "try" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                if (FeildName.Count > 0)
                {
                    //sql语句部分
                    str.Append("\t\t\t\t" + "string commandText = \"UPDATE " + TableName + " SET ");
                    for (int i = 0; i < FeildName.Count; i++)
                    {
                        if (FeildIsIdentity[i].Trim() != "1")
                        {
                            str.Append(FeildName[i].Trim() + "=@" + FeildName[i].Trim());
                            if (i < FeildName.Count - 1)
                            {
                                str.Append(",");
                            }
                        }
                    }
                    str.Append(" WHERE ");
                    str.Append(FeildName[0].Trim() + "=@" + FeildName[0].Trim());
                    str.Append("\";" + "\r\n");
                    str.Append("\t\t\t\t" + "SqlParameter[] parms = new SqlParameter[]{" + "\r\n");
                    str.Append("\t\t\t\t\t" + "new SqlParameter(\"@" + FeildName[0].Trim() + "\"," + TableName + "Object." + FeildName[0].Trim() + ")," + "\r\n");
                    for (int i = 0; i < FeildName.Count; i++)
                    {
                        if (FeildIsIdentity[i].Trim() != "1")
                        {
                            str.Append("\t\t\t\t\t" + "new SqlParameter(\"@" + FeildName[i].Trim() + "\"," + TableName + "Object." + FeildName[i].Trim() + ")," + "\r\n");
                        }
                    }
                    str.Append("\t\t\t\t" + "};" + "\r\n");
                    str.Append("\t\t\t\t" + "return SqlHelper_DG.ExecuteNonQuery(SqlHelper_DG.ConnString, commandText, CommandType.Text, parms) > 0 ? true : false;" + "\r\n");
                }
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t\t" + "catch (Exception)" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                str.Append("\t\t\t\t" + "return false;" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
                #endregion
            }
            //判断是否需要删除语句 Delete
            if (MethodInfo[2])
            {
                #region 删除
                //Delete 
                str.Append("\t\t" + "/// <summary>" + "\r\n");
                str.Append("\t\t" + "/// Delete删除语句，返回删除的结果：成功true，失败false" + "\r\n");
                str.Append("\t\t" + "/// </summary>" + "\r\n");
                str.Append("\t\t" + "/// <param name=\""+TableName+"Object\">条件对象，唯一字段或者自定义删除条件</param>" + "\r\n");
                str.Append("\t\t" + "/// <returns></returns>" + "\r\n");
                str.Append("\t\t" + "public Boolean IsDelete(" + TableName + " " + TableName + "Object)" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "try" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                if (FeildName.Count > 0)
                {
                    //sql语句部分
                    str.Append("\t\t\t\t" + "string commandText = \"DELETE FROM " + TableName);
                    str.Append(" WHERE ");
                    str.Append(FeildName[0].Trim() + "=@" + FeildName[0].Trim());
                    str.Append("\";" + "\r\n");
                    str.Append("\t\t\t\t" + "SqlParameter[] parms = new SqlParameter[]{" + "\r\n");
                    str.Append("\t\t\t\t" + "new SqlParameter(\"@" + FeildName[0].Trim() + "\"," + TableName + "Object." + FeildName[0].Trim() + ")," + "\r\n");
                    str.Append("\t\t\t\t" + "};" + "\r\n");
                    str.Append("\t\t\t\t" + "return SqlHelper_DG.ExecuteNonQuery(SqlHelper_DG.ConnString, commandText, CommandType.Text, parms) > 0 ? true : false;" + "\r\n");
                }
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t\t" + "catch (Exception)" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                str.Append("\t\t\t\t" + "return false;" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
                #endregion
            }
            //判断是否需要查询语句 Select
            if (MethodInfo[3])
            {
                #region 查询
                //查询单个对象，返回model
                str.Append("\t\t" + "/// <summary>" + "\r\n");
                str.Append("\t\t" + "/// Select Model语句，返回查询的Model结果" + "\r\n");
                str.Append("\t\t" + "/// </summary>" + "\r\n");
                str.Append("\t\t" + "/// <param name=\""+TableName+"Object\">条件对象，按需求来确定查找的条件</param>" + "\r\n");
                str.Append("\t\t" + "/// <returns></returns>" + "\r\n");
                str.Append("\t\t" + "public T SelectSingleLine_RTModel<T>(" + TableName + " " + TableName + "Object)" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "try" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                if (FeildName.Count > 0)
                {
                    //sql语句部分
                    str.Append("\t\t\t\t" + "string commandText = \"SELECT TOP (1) * FROM " + TableName);
                    str.Append(" WHERE ");
                    str.Append(FeildName[0].Trim() + "=@" + FeildName[0].Trim());
                    str.Append("\";//这里需要按需求来确定需要查找的是哪个参数 因为要返回一行数据，所以搜索的条件值必须是唯一的，主键是最佳选择！" + "\r\n");
                    str.Append("\t\t\t\t" + "SqlParameter[] parms = new SqlParameter[]{" + "\r\n");
                    str.Append("\t\t\t\t" + "new SqlParameter(\"@" + FeildName[0].Trim() + "\"," + TableName + "Object." + FeildName[0].Trim() + ")," + "\r\n");
                    str.Append("\t\t\t\t" + "};" + "\r\n");
                    str.Append("\t\t\t\t" + "return SqlHelper_DG.ReturnModelByModels<T>(SqlHelper_DG.ExecuteReader(SqlHelper_DG.ConnString, commandText, CommandType.Text, parms));" + "\r\n");
                }
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t\t" + "catch (Exception)" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                str.Append("\t\t\t\t" + "return default(T);" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
                //查询所有数据，返回List--------------------------------------------------------------------------------------
                str.Append("\t\t" + "/// <summary>" + "\r\n");
                str.Append("\t\t" + "/// Select List语句，返回查询的List结果集" + "\r\n");
                str.Append("\t\t" + "/// </summary>" + "\r\n");
                str.Append("\t\t" + "/// <param name=\"safeSqlCondition\">查询的条件,从BLL层传来的安全的sql语句</param>" + "\r\n");
                str.Append("\t\t" + "/// <returns></returns>" + "\r\n");
                str.Append("\t\t" + "public List<T> SelectALL<T>(string safeSqlCondition = \" 1=1 \")" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "try" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                //sql语句部分
                str.Append("\t\t\t\t" + "string commandText = \"SELECT * FROM " + TableName + " WHERE \" + safeSqlCondition;//这里修改条件语句 默认全部" + "\r\n");
                str.Append("\t\t\t\t" + "return SqlHelper_DG.ReturnListByModels<T>(SqlHelper_DG.ExecuteDataSet(SqlHelper_DG.ConnString, commandText));" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t\t" + "catch (Exception)" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                str.Append("\t\t\t\t" + "return null;" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
                //用RowNumber分页返回信息带参数-------------------------------------------------------------------------------
                str.Append("\t\t" + "/// <summary>" + "\r\n");
                str.Append("\t\t" + "/// 用RowNumber方法进行分页处理返回List结果集，效率最佳但不支持低版本SqlServer" + "\r\n");
                str.Append("\t\t" + "/// </summary>" + "\r\n");
                str.Append("\t\t" + "/// <param name=\"PageSize\">int页大小，每页容纳的行数</param>" + "\r\n");
                str.Append("\t\t" + "/// <param name=\"PageNumber\">int页码，第几页</param>" + "\r\n");
                str.Append("\t\t" + "/// <param name=\"DataOrderBy\">object表中按这个字段顺序排序,可以是任意字段,可以加修饰符如DESC</param>" + "\r\n");
                str.Append("\t\t" + "/// <param name=\"safeSqlCondition\">所有集合中先找出符合这个条件的结果再进行分页处理 查询的条件,从BLL层传来的安全的sql语句</param>" + "\r\n");
                str.Append("\t\t" + "/// <returns></returns>" + "\r\n");
                str.Append("\t\t" + "public List<T> SelectALLPaginByRowNumber<T>(int PageSize, int PageNumber, string DataOrderBy,string safeSqlCondition=\"1=1\")" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "try" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                //sql语句部分
                str.Append("\t\t\t\t" + "StringBuilder commandText=new StringBuilder ();" + "\r\n");
                str.Append("\t\t\t\t" + "commandText.Append(\"SELECT TOP \" + PageSize + \" * FROM (SELECT ROW_NUMBER() OVER (ORDER BY \" + DataOrderBy + \") AS RowNumber,* FROM " + TableName + " \");" + "\r\n");
                str.Append("\t\t\t\t" + "commandText.Append(\" WHERE \" + safeSqlCondition + \" \");//这里修改条件语句" + "\r\n");
                str.Append("\t\t\t\t" + "commandText.Append(\" ) AS T  WHERE RowNumber > (\" + PageSize + \"*(\" + PageNumber + \"-1))\");" + "\r\n");
                str.Append("\t\t\t\t" + "return SqlHelper_DG.ReturnListByModels<T>(SqlHelper_DG.ExecuteDataSet(SqlHelper_DG.ConnString, commandText.ToString()));" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t\t" + "catch (Exception)" + "\r\n");
                str.Append("\t\t\t" + "{" + "\r\n");
                str.Append("\t\t\t\t" + "return null;" + "\r\n");
                str.Append("\t\t\t" + "}" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
                #endregion
            }

            str.Append("\t" + "}" + "\r\n");//public class }
            str.Append("}" + "\r\n");//namespace class }
            return str.ToString();
        }
    }
}
