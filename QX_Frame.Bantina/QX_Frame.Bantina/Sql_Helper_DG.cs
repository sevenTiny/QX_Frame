/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2016-05-06
 * Update:2017-09-18 17:50:58
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;


namespace QX_Frame.Bantina
{
    /// <summary>
    /// SqlHelper
    /// 此类为抽象类，不允许实例化，在应用时直接调用即可;
    /// author qixiao;
    /// release Time :20160506;
    /// the end of update at:2017-9-27 10:55:22,class is Obsolete please use Db_Helper_DG which is new support
    /// </summary>
    [Obsolete("code has been migrated to Db_Helper_DG,please use Db_Helper_DG implement the same operatioin! -- QX_Frame 2017-9-27 15:11:12")]
    public abstract class Sql_Helper_DG
    {
        #region ConnString 链接字符串声明

        /// <summary>
        /// 连接字符串 ConnString_Default 默认，且赋值时会直接覆盖掉读写
        /// </summary>
        private static string _connString = Configs.QX_Frame_Helper_DG_Config.ConnectionString_DB_QX_Frame_Default;
        public static string ConnString_Default
        {
            get { return _connString; }
            set
            {
                _connString = value;
                ConnString_RW = _connString;
                ConnString_R = _connString;
            }
        }
        /// <summary>
        /// 连接字符串 ConnString_RW 读写数据库使用
        /// </summary>
        public static string ConnString_RW = _connString;
        /// <summary>
        /// 连接字符串 ConnString_R 读数据库使用
        /// </summary>
        public static string ConnString_R = _connString;

        #endregion

        static Sql_Helper_DG()
        {
            //if (string.IsNullOrEmpty(ConnString_RW) || string.IsNullOrEmpty(ConnString_R))
            //{
            //    throw new ArgumentNullException("ConnString Can Not Be Null !");
            //}
        }

        #region ExcuteNonQuery 执行sql语句或者存储过程,返回影响的行数---ExcuteNonQuery
        /// <summary>
        /// 执行sql语句或存储过程，返回受影响的行数,不带参数。
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型 有默认值CommandType.Text</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(string commandTextOrSpName, CommandType commandType = CommandType.Text)
        {
            using (SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_RW))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType);//参数增加了commandType 可以自己编辑执行方式
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 执行sql语句或存储过程，返回受影响的行数。
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型 t</param>
        /// <param name="parms">SqlParameter[]参数数组，允许空</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(string commandTextOrSpName, CommandType commandType, params SqlParameter[] parms)
        {
            using (SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_RW))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType, parms);//参数增加了commandType 可以自己编辑执行方式
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 执行sql命令，返回受影响的行数。
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="obj">object[]参数数组，允许空</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(string commandTextOrSpName, CommandType commandType, params object[] obj)
        {
            using (SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_RW))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType, obj);//参数增加了commandType 可以自己编辑执行方式
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region ExecuteScalar 执行sql语句或者存储过程,执行单条语句，返回单个结果---ScalarExecuteScalar
        /// <summary>
        /// 执行sql语句或存储过程 返回ExecuteScalar （返回自增的ID）不带参数
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型 有默认值CommandType.Text</param>
        /// <returns></returns>
        public static object ExecuteScalar(string commandTextOrSpName, CommandType commandType = CommandType.Text)
        {
            using (SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_RW))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType);
                    return cmd.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// 执行sql语句或存储过程 返回ExecuteScalar （返回自增的ID）
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parms">SqlParameter[]参数数组，允许空</param>
        /// <returns></returns>
        public static object ExecuteScalar(string commandTextOrSpName, CommandType commandType, params SqlParameter[] parms)
        {
            using (SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_RW))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType, parms);
                    return cmd.ExecuteScalar();
                }

            }
        }
        /// <summary>
        /// 执行sql语句或存储过程 返回ExecuteScalar （返回自增的ID）
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="obj">object[]参数数组，允许空</param>
        /// <returns></returns>
        public static object ExecuteScalar(string commandTextOrSpName, CommandType commandType, params object[] obj)
        {
            using (SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_RW))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType, obj);
                    return cmd.ExecuteScalar();
                }
            }
        }
        #endregion

        #region ExecuteReader 执行sql语句或者存储过程,返回DataReader---DaataReader
        /// <summary>
        /// 执行sql语句或存储过程 返回DataReader 不带参数
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型 有默认值CommandType.Text</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string commandTextOrSpName, CommandType commandType = CommandType.Text)
        {
            //sqlDataReader不能用using 会关闭conn 导致不能获取到返回值。注意：DataReader获取值时必须保持连接状态
            SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_R, ConnString_RW);
            SqlCommand cmd = new SqlCommand();
            PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// 执行sql语句或存储过程 返回DataReader
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parms">SqlParameter[]参数数组，允许空</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string commandTextOrSpName, CommandType commandType, params SqlParameter[] parms)
        {
            //sqlDataReader不能用using 会关闭conn 导致不能获取到返回值。注意：DataReader获取值时必须保持连接状态
            SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_R, ConnString_RW);
            SqlCommand cmd = new SqlCommand();
            PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType, parms);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary>
        /// 执行sql语句或存储过程 返回DataReader
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="obj">object[]参数数组，允许空</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string commandTextOrSpName, CommandType commandType, params object[] obj)
        {
            //sqlDataReader不能用using 会关闭conn 导致不能获取到返回值。注意：DataReader获取值时必须保持连接状态
            SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_R, ConnString_RW);
            SqlCommand cmd = new SqlCommand();
            PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType, obj);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        #endregion

        #region ExecuteDataTable 执行sql语句或者存储过程,返回一个DataTable---DataTable

        /**
         * Update At 2017-3-2 14:58:45
         * Add the ExecuteDataTable Method into Sql_Helper_DG  
         **/

        /// <summary>
        /// 执行sql语句或存储过程，返回DataTable不带参数
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型 有默认值CommandType.Text</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string commandTextOrSpName, CommandType commandType = CommandType.Text)
        {
            using (SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_RW))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            return ds.Tables[0];
                        }
                        return default(DataTable);
                    }
                }
            }
        }
        /// <summary>
        /// 执行sql语句或存储过程，返回DataTable
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parms">SqlParameter[]参数数组，允许空</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string commandTextOrSpName, CommandType commandType, params SqlParameter[] parms)
        {
            using (SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_RW))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType, parms);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            return ds.Tables[0];
                        }
                        return default(DataTable);
                    }
                }
            }
        }
        /// <summary>
        /// 执行sql语句或存储过程，返回DataTable
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型 </param>
        /// <param name="obj">object[]参数数组，允许空</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string commandTextOrSpName, CommandType commandType, params object[] obj)
        {
            using (SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_RW))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType, obj);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            return ds.Tables[0];
                        }
                        return default(DataTable);
                    }
                }
            }
        }
        #endregion

        #region ExecuteDataSet 执行sql语句或者存储过程,返回一个DataSet---DataSet
        /// <summary>
        /// 执行sql语句或存储过程，返回DataSet 不带参数
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型 有默认值CommandType.Text</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string commandTextOrSpName, CommandType commandType = CommandType.Text)
        {
            using (SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_RW))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }
        /// <summary>
        /// 执行sql语句或存储过程，返回DataSet
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parms">SqlParameter[]参数数组，允许空</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string commandTextOrSpName, CommandType commandType, params SqlParameter[] parms)
        {
            using (SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_RW))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType, parms);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }
        /// <summary>
        /// 执行sql语句或存储过程，返回DataSet
        /// </summary>
        /// <param name="ConnString">连接字符串，可以自定义，可以以使用SqlHelper_DG.ConnString</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">命令类型 </param>
        /// <param name="obj">object[]参数数组，允许空</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string commandTextOrSpName, CommandType commandType, params object[] obj)
        {
            using (SqlConnection_WR_Safe conn = new SqlConnection_WR_Safe(Options.Opt_DataBaseType.SqlServer, ConnString_RW))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PreparCommand(conn.DbConnection as SqlConnection, cmd, commandTextOrSpName, commandType, obj);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }
        #endregion

        #region ---PreparCommand 构建一个通用的command对象供内部方法进行调用---
        /// <summary>
        /// 不带参数的设置sqlcommand对象
        /// </summary>
        /// <param name="conn">sqlconnection对象</param>
        /// <param name="cmd">sqlcommmand对象</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">语句的类型</param>
        private static void PreparCommand(SqlConnection conn, SqlCommand cmd, string commandTextOrSpName, CommandType commandType)
        {
            //打开连接
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            //设置SqlCommand对象的属性值
            cmd.Connection = conn;
            cmd.CommandType = commandType;
            cmd.CommandText = commandTextOrSpName;
            cmd.CommandTimeout = 60;
        }
        /// <summary>
        /// 设置一个等待执行的SqlCommand对象
        /// </summary>
        /// <param name="conn">sqlconnection对象</param>
        /// <param name="cmd">sqlcommmand对象</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">语句的类型</param>
        /// <param name="parms">参数，sqlparameter类型，需要指出所有的参数名称</param>
        private static void PreparCommand(SqlConnection conn, SqlCommand cmd, string commandTextOrSpName, CommandType commandType, params SqlParameter[] parms)
        {
            //打开连接
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            //设置SqlCommand对象的属性值
            cmd.Connection = conn;
            cmd.CommandType = commandType;
            cmd.CommandText = commandTextOrSpName;
            cmd.CommandTimeout = 60;

            if (parms != null)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(parms);
            }
        }
        /// <summary>
        /// PreparCommand方法，可变参数为object需要严格按照参数顺序传参
        /// 之所以会用object参数方法是为了我们能更方便的调用存储过程，不必去关系存储过程参数名是什么，知道它的参数顺序就可以了 sqlparameter必须指定每一个参数名称
        /// </summary>
        /// <param name="conn">sqlconnection对象</param>
        /// <param name="cmd">sqlcommmand对象</param>
        /// <param name="commandTextOrSpName">sql语句或存储过程名称</param>
        /// <param name="commandType">语句的类型</param>
        /// <param name="parms">参数，object类型，需要按顺序赋值</param>
        private static void PreparCommand(SqlConnection conn, SqlCommand cmd, string commandTextOrSpName, CommandType commandType, params object[] parms)
        {

            //打开连接
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            //设置SqlCommand对象的属性值
            cmd.Connection = conn;
            cmd.CommandType = commandType;
            cmd.CommandText = commandTextOrSpName;
            cmd.CommandTimeout = 60;

            cmd.Parameters.Clear();
            if (parms != null)
            {
                cmd.Parameters.AddRange(parms);
            }
        }

        #endregion

        #region 通过Model反射返回结果集 Model为 T 泛型变量的真实类型---反射返回结果集
        /// <summary>
        /// 反射返回一个List T 类型的结果集
        /// </summary>
        /// <typeparam name="T">Model中对象类型</typeparam>
        /// <param name="ds">DataSet结果集</param>
        /// <returns></returns>
        public static List<T> Return_List_T_ByDataSet<T>(DataSet ds)
        {
            try
            {
                List<T> list = new List<T>();//实例化一个list对象
                PropertyInfo[] propertyInfos = typeof(T).GetProperties();     //获取T对象的所有公共属性

                DataTable dt = ds.Tables[0];    // 获取到ds的dt
                if (dt.Rows.Count > 0)
                {
                    //判断读取的行是否>0 即数据库数据已被读取
                    foreach (DataRow row in dt.Rows)
                    {
                        T model1 = System.Activator.CreateInstance<T>();//实例化一个对象，便于往list里填充数据
                        foreach (PropertyInfo propertyInfo in propertyInfos)
                        {
                            try
                            {
                                //遍历模型里所有的字段
                                if (row[propertyInfo.Name] != System.DBNull.Value)
                                {
                                    //判断值是否为空，如果空赋值为null见else
                                    if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                                    {
                                        //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                                        NullableConverter nullableConverter = new NullableConverter(propertyInfo.PropertyType);
                                        //将convertsionType转换为nullable对的基础基元类型
                                        propertyInfo.SetValue(model1, Convert.ChangeType(row[propertyInfo.Name], nullableConverter.UnderlyingType), null);
                                    }
                                    else
                                    {
                                        propertyInfo.SetValue(model1, Convert.ChangeType(row[propertyInfo.Name], propertyInfo.PropertyType), null);
                                    }
                                }
                                else
                                {
                                    propertyInfo.SetValue(model1, null, null);//如果数据库的值为空，则赋值为null
                                }
                            }
                            catch (Exception)
                            {
                                propertyInfo.SetValue(model1, null, null);//如果数据库的值为空，则赋值为null
                            }
                        }
                        list.Add(model1);//将对象填充到list中
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 反射返回一个T类型的结果
        /// </summary>
        /// <typeparam name="T">Model中对象类型</typeparam>
        /// <param name="reader">SqlDataReader结果集</param>
        /// <returns></returns>
        public static T Return_T_ByDataReader<T>(SqlDataReader reader)
        {
            try
            {
                T model = System.Activator.CreateInstance<T>();                     //实例化一个T类型对象
                PropertyInfo[] propertyInfos = model.GetType().GetProperties();     //获取T对象的所有公共属性
                using (reader)
                {
                    if (reader.Read())
                    {
                        foreach (PropertyInfo propertyInfo in propertyInfos)
                        {
                            //遍历模型里所有的字段
                            if (reader[propertyInfo.Name] != System.DBNull.Value)
                            {
                                //判断值是否为空，如果空赋值为null见else
                                if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                                {
                                    //如果convertsionType为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                                    NullableConverter nullableConverter = new NullableConverter(propertyInfo.PropertyType);
                                    //将convertsionType转换为nullable对的基础基元类型
                                    propertyInfo.SetValue(model, Convert.ChangeType(reader[propertyInfo.Name], nullableConverter.UnderlyingType), null);
                                }
                                else
                                {
                                    propertyInfo.SetValue(model, Convert.ChangeType(reader[propertyInfo.Name], propertyInfo.PropertyType), null);
                                }
                            }
                            else
                            {
                                propertyInfo.SetValue(model, null, null);//如果数据库的值为空，则赋值为null
                            }
                        }
                        return model;//返回T类型的赋值后的对象 model
                    }
                }
                return default(T);//返回引用类型和值类型的默认值0或null
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 反射返回一个T类型的结果
        /// </summary>
        /// <typeparam name="T">Model中对象类型</typeparam>
        /// <param name="ds">DataSet结果集</param>
        /// <returns></returns>
        public static T Return_T_ByDataSet<T>(DataSet ds)
        {
            return Return_List_T_ByDataSet<T>(ds).FirstOrDefault();
        }

        #endregion
    }
}
