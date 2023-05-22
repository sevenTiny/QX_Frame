using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_FlowchartToCode_DG
{
    public abstract class ThreelayeToBLL
    {
        public static string CreateBLLCode(List<object> CreateInfo)
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
            string DALclassName = CreateInfo[11].ToString();                          //获得DAL层的类名


            StringBuilder str = new StringBuilder();


            str.Append("using System;" + "\r\n");
            str.Append("using System.Collections.Generic;" + "\r\n");
            str.Append("using DAL;" + "\r\n");
            str.Append("using Model;" + "\r\n");
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
                str.Append("\t\t" + "//返回表中的数据数量 Int 一般配合分页使用" + "\r\n");//添加方法介绍
                str.Append("\t\t" + "public int DataCount()" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "string where = \" 1=1 \";//判断条件语句可以自由发挥,默认返回全部 必须写安全的sql语句，防止数据注入!!!" + "\r\n");
                str.Append("\t\t\t" + "return new " + DALclassName + "().DataCount(where);" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
            }

            //判断是否需要 检测存在语句
            if (MethodInfo[5])
            {
                str.Append("\t\t" + "//返回是否存在" + "\r\n");//添加方法介绍
                str.Append("\t\t" + "public Boolean IsExistWhereFeild(" + TableName + " " + TableName + "Object)" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "return new " + DALclassName + "().IsExistWhereFeild(" + TableName + "Object);//这个需要按项目需求修改DAL层的条件代码以符合项目！！！" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
            }


            //判断是否需要增加语句 Add
            if (MethodInfo[0])
            {
                str.Append("\t\t" + "//插入业务" + "\r\n");//添加方法介绍
                str.Append("\t\t" + "public Boolean IsInsert(" + TableName + " " + TableName + "Object)" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "return new " + DALclassName + "().IsInsert(" + TableName + "Object);//自动过滤掉自增字段" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
            }
            //判断是否需要修改语句 Update
            if (MethodInfo[1])
            {
                //update 1
                str.Append("\t\t" + "//修改业务" + "\r\n");//添加方法介绍
                str.Append("\t\t" + "public Boolean IsUpdate(" + TableName + " " + TableName + "Object)" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "return new " + DALclassName + "().IsUpdate(" + TableName + "Object);//条件写在DAL层代码中" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
            }
            //判断是否需要删除语句 Delete
            if (MethodInfo[2])
            {
                str.Append("\t\t" + "//删除业务" + "\r\n");//添加方法介绍
                str.Append("\t\t" + "public Boolean IsDelete(" + TableName + " " + TableName + "Object)" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "return new " + DALclassName + "().IsDelete(" + TableName + "Object);//条件写在DAL层代码中" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");

            }
            //判断是否需要查询语句 Select
            if (MethodInfo[3])
            {
                //获取到某一行的业务--返回是Model类型的数据
                str.Append("\t\t" + "//获取到某一行的业务--返回是Model类型的数据" + "\r\n");//添加方法介绍
                str.Append("\t\t" + "public " + TableName + " SelectSingleLine_RTModel(" + TableName + " " + TableName + "Object)" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "return new " + DALclassName + "().SelectSingleLine_RTModel<" + TableName + ">(" + TableName + "Object);" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
                //获取到符合条件的所有值的业务--返回List T
                str.Append("\t\t" + "//获取到符合条件的所有值的业务--返回List T" + "\r\n");//添加方法介绍
                str.Append("\t\t" + "public List<" + TableName + "> SelectALL(string safeSqlCondition = \" 1 = 1 \")" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "return new " + DALclassName + "().SelectALL<" + TableName + ">(safeSqlCondition);//这里补充返回Model的条件，为空默认所有数据" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
                ////分页获取到符合条件的所有值的业务，一般配合返回总数的方法使用显示总页数！--返回List T
                str.Append("\t\t" + "//分页获取到符合条件的所有值的业务，一般配合返回总数的方法使用显示总页数！--返回List T" + "\r\n");//添加方法介绍
                str.Append("\t\t" + "public List<" + TableName + "> SelectALLPaginByRowNumber(int PageSize, int PageNumber, string DataOrderBy)" + "\r\n");
                str.Append("\t\t" + "{" + "\r\n");
                str.Append("\t\t\t" + "string where = \" 1=1 \";//判断条件语句可以自由发挥,默认返回全部 必须写安全的sql语句，防止数据注入!!!" + "\r\n");
                str.Append("\t\t\t" + "return new " + DALclassName + "().SelectALLPaginByRowNumber<" + TableName + ">(PageSize,PageNumber,DataOrderBy,where);" + "\r\n");
                str.Append("\t\t" + "}" + "\r\n");
            }
           


            str.Append("\t" + "}" + "\r\n");//public class }
            str.Append("}" + "\r\n");//namespace class }



            return str.ToString();


        }
    }
}
