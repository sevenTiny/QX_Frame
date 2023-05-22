/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-8-7 10:46:07
 * Update:2017-8-22 09:26:07
 * Update:2017-9-8 09:24:53
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * Personal WebSit: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:-.
 * Thx , Best Regards ~
 *********************************************************/
using QX_Frame.Bantina.Extends;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace QX_Frame.Bantina.Bankinate
{
    /// <summary>
    /// Bantina Interface
    /// </summary>
    public interface IBankinate
    {
        bool Add<TEntity>(TEntity entity) where TEntity : class;
        bool Update<TEntity>(TEntity entity) where TEntity : class;
        bool Update<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where) where TEntity : class;
        bool Delete<TEntity>(TEntity entity) where TEntity : class;
        bool Delete<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
        bool QueryExist<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
        int QueryCount<TEntity>() where TEntity : class;
        int QueryCount<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
        TEntity QueryEntity<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
        List<TEntity> QueryEntities<TEntity>() where TEntity : class;
        List<TEntity> QueryEntities<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class;
        List<TEntity> QueryEntitiesPaging<TEntity>(int pageIndex, int pageSize, Expression<Func<TEntity, object>> orderBy, bool isDESC = false) where TEntity : class;
        List<TEntity> QueryEntitiesPaging<TEntity>(int pageIndex, int pageSize, Expression<Func<TEntity, object>> orderBy, Expression<Func<TEntity, bool>> where, bool isDESC = false) where TEntity : class;
        List<TEntity> QueryEntitiesPaging<TEntity>(int pageIndex, int pageSize, Expression<Func<TEntity, object>> orderBy, out int count, bool isDESC = false) where TEntity : class;
        List<TEntity> QueryEntitiesPaging<TEntity>(int pageIndex, int pageSize, Expression<Func<TEntity, object>> orderBy, Expression<Func<TEntity, bool>> where, out int count, bool isDESC = false) where TEntity : class;
        DataTable QueryDataTable<TEntity>(string sql, params SqlParameter[] parms) where TEntity : class;
        DataSet QueryDataDataSet<TEntity>(string sql, params SqlParameter[] parms) where TEntity : class;
        int ExecuteSql<TEntity>(string sql, params SqlParameter[] parms) where TEntity : class;
        List<TEntity> ExecuteSqlToList<TEntity>(string sql, params SqlParameter[] parms) where TEntity : class;
        int ExecuteStoredProcedure<TEntity>(string storedProcedureName, params object[] parms) where TEntity : class;
        List<TEntity> ExecuteStoredProcedureToList<TEntity>(string storedProcedureName, params object[] parms) where TEntity : class;
    }

    public abstract class Bankinate : Sql_Helper_DG, IDisposable, IBankinate
    {
        /// <summary>
        /// ConnectionString_READ
        /// </summary>
        public string ConnectionString_READ { get; set; }
        /// <summary>
        /// ConnectionString_WRITE
        /// </summary>
        public string ConnectionString_WRITE { get; set; }
        /// <summary>
        /// Execute SqlStatement
        /// </summary>
        public string SqlStatement { get; set; }
        #region Constructor
        /// <summary>
        /// Bantina
        /// </summary>
        public Bankinate()
        {
            if (string.IsNullOrEmpty(ConnString_Default))
            {
                throw new Exception_DG("ConnString_Default Must Be Declared When Initiation ! -- QX_Frame.Bantina.Bankinate");
            }
            this.ConnectionString_READ = ConnString_Default;
            this.ConnectionString_WRITE = ConnString_Default;
        }

        /// <summary>
        /// Bantina
        /// </summary>
        /// <param name="connString"></param>
        public Bankinate(string connString)
        {
            ConnString_Default = connString;
            this.ConnectionString_READ = ConnString_Default;
            this.ConnectionString_WRITE = ConnString_Default;
        }

        /// <summary>
        /// Bantina
        /// </summary>
        /// <param name="connString_RW"></param>
        /// <param name="connString_R"></param>
        public Bankinate(string connString_RW, string connString_R)
        {
            ConnString_RW = connString_RW;
            ConnString_R = connString_R;
            this.ConnectionString_READ = ConnString_RW;
            this.ConnectionString_WRITE = ConnString_R;
        }

        #endregion

        #region Add Method

        /// <summary>
        /// Add
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add<TEntity>(TEntity entity) where TEntity : class
        {
            Type entityType = typeof(TEntity);

            string tableName = GetTablaName(entityType);

            StringBuilder builder_front = new StringBuilder();
            StringBuilder builder_behind = new StringBuilder();

            List<SqlParameter> sqlParameterList = new List<SqlParameter>();

            builder_front.Append("INSERT INTO ");
            builder_front.Append(tableName);
            builder_front.Append(" (");
            builder_behind.Append(" VALUES (");

            PropertyInfo[] propertyInfos = entityType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //AutoIncrease : if property is auto increase attribute skip this column.
                if (propertyInfo.GetCustomAttribute(typeof(AutoIncreaseAttribute), true) is AutoIncreaseAttribute autoIncreaseAttr)
                {
                    continue;
                }

                //key :
                if (propertyInfo.GetCustomAttribute(typeof(KeyAttribute), true) is KeyAttribute keyAttr)
                {
                    builder_front.Append(propertyInfo.Name);
                    builder_front.Append(",");

                    builder_behind.Append("@");
                    builder_behind.Append(propertyInfo.Name);
                    builder_behind.Append(",");

                    sqlParameterList.Add(new SqlParameter("@" + propertyInfo.Name, propertyInfo.GetValue(entity)));
                }

                //Column :
                if (propertyInfo.GetCustomAttribute(typeof(ColumnAttribute), true) is ColumnAttribute columnAttr)
                {
                    builder_front.Append(propertyInfo.Name);
                    builder_front.Append(",");

                    builder_behind.Append("@");
                    builder_behind.Append(propertyInfo.Name);
                    builder_behind.Append(",");

                    sqlParameterList.Add(new SqlParameter("@" + propertyInfo.Name, propertyInfo.GetValue(entity)));
                }

                //the end
                if (propertyInfos.Last() == propertyInfo)
                {
                    builder_front.Remove(builder_front.Length - 1, 1);
                    builder_front.Append(")");

                    builder_behind.Remove(builder_behind.Length - 1, 1);
                    builder_behind.Append(")");
                }
            }

            //Generate SqlStatement
            string sql = builder_front.Append(builder_behind.ToString()).ToString();

            this.SqlStatement = sql;

            //Execute Task That Execute SqlStatement

            bool result = ExecuteNonQuery(sql, System.Data.CommandType.Text, sqlParameterList.ToArray()) > 0;

            // Cache Deal:if Table change,we should renew the cache value
            HttpRuntimeCache_Helper_DG.Cache_Add(tableName, result);

            return result;
        }

        #endregion

        #region Update Method

        /// <summary>
        /// Update Method : the [Key] Attribute must be provide!
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update<TEntity>(TEntity entity) where TEntity : class
        {
            int keyCount = 0;

            Type entityType = typeof(TEntity);

            string tableName = GetTablaName(entityType);

            StringBuilder builder_front = new StringBuilder();
            StringBuilder builder_where = new StringBuilder();

            List<SqlParameter> sqlParameterList = new List<SqlParameter>();

            builder_front.Append("UPDATE ");
            builder_front.Append(tableName);
            builder_front.Append(" SET ");
            builder_where.Append(" WHERE ");

            PropertyInfo[] propertyInfos = entityType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //key :
                if (propertyInfo.GetCustomAttribute(typeof(KeyAttribute), true) is KeyAttribute keyAttr)
                {
                    builder_where.Append(propertyInfo.Name);
                    builder_where.Append("=");
                    builder_where.Append("@");
                    builder_where.Append(propertyInfo.Name);

                    sqlParameterList.Add(new SqlParameter("@" + propertyInfo.Name, propertyInfo.GetValue(entity)));

                    keyCount++;
                    continue;
                }

                //AutoIncrease : if property is auto increase attribute skip this column.
                if (propertyInfo.GetCustomAttribute(typeof(AutoIncreaseAttribute), true) is AutoIncreaseAttribute autoIncreaseAttr)
                {
                    continue;
                }

                //Column :
                if (propertyInfo.GetCustomAttribute(typeof(ColumnAttribute), true) is ColumnAttribute columnAttr)
                {
                    builder_front.Append(propertyInfo.Name);
                    builder_front.Append("=");
                    builder_front.Append("@");
                    builder_front.Append(propertyInfo.Name);
                    builder_front.Append(",");

                    sqlParameterList.Add(new SqlParameter("@" + propertyInfo.Name, propertyInfo.GetValue(entity)));
                }

                //the end
                if (propertyInfos.Last() == propertyInfo)
                {
                    builder_front.Remove(builder_front.Length - 1, 1);
                }
            }

            if (keyCount == 0)
            {
                throw new Exception_DG("[KeyAttribute] must be provide.");
            }
            else if (keyCount > 1)
            {
                throw new Exception_DG("[KeyAttribute] only one suppport.");
            }

            //Generate SqlStatement
            string sql = builder_front.Append(builder_where.ToString()).ToString();

            this.SqlStatement = sql;

            //Execute Task That Execute SqlStatement
            bool result = ExecuteNonQuery(sql, System.Data.CommandType.Text, sqlParameterList.ToArray()) > 0;

            // Cache Deal:if Table change,we should renew the cache value
            HttpRuntimeCache_Helper_DG.Cache_Add(tableName, result);

            return result;
        }

        /// <summary>
        /// Update Method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool Update<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            string lambdaString = where.ToString();

            Type entityType = typeof(TEntity);

            string tableName = GetTablaName(entityType);

            StringBuilder builder_front = new StringBuilder();

            List<SqlParameter> sqlParameterList = new List<SqlParameter>();


            builder_front.Append("UPDATE ");
            builder_front.Append(lambdaString.Substring(0, lambdaString.IndexOf("=>")));
            builder_front.Append(" SET ");

            PropertyInfo[] propertyInfos = entityType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //AutoIncrease : if property is auto increase attribute skip this column.
                if (propertyInfo.GetCustomAttribute(typeof(AutoIncreaseAttribute), true) is AutoIncreaseAttribute autoIncreaseAttr)
                {
                    continue;
                }

                //key :
                if (propertyInfo.GetCustomAttribute(typeof(KeyAttribute), true) is KeyAttribute keyAttr)
                {
                    builder_front.Append(propertyInfo.Name);
                    builder_front.Append("=");
                    builder_front.Append("@");
                    builder_front.Append(propertyInfo.Name);
                    builder_front.Append(",");

                    sqlParameterList.Add(new SqlParameter("@" + propertyInfo.Name, propertyInfo.GetValue(entity)));
                }

                //Column :
                if (propertyInfo.GetCustomAttribute(typeof(ColumnAttribute), true) is ColumnAttribute columnAttr)
                {
                    builder_front.Append(propertyInfo.Name);
                    builder_front.Append("=");
                    builder_front.Append("@");
                    builder_front.Append(propertyInfo.Name);
                    builder_front.Append(",");

                    sqlParameterList.Add(new SqlParameter("@" + propertyInfo.Name, propertyInfo.GetValue(entity)));
                }

                //the end
                if (propertyInfos.Last() == propertyInfo)
                {
                    builder_front.Remove(builder_front.Length - 1, 1);
                    builder_front.Append(" From ");
                    builder_front.Append(tableName);
                    builder_front.Append(" ");
                }
            }

            //Generate SqlStatement

            string sql = builder_front.Append(lambdaString.LambdaToSqlStatement()).ToString();

            this.SqlStatement = sql;

            //Execute Task That Execute SqlStatement
            bool result = ExecuteNonQuery(sql, System.Data.CommandType.Text, sqlParameterList.ToArray()) > 0;

            // Cache Deal:if Table change,we should renew the cache value
            HttpRuntimeCache_Helper_DG.Cache_Add(tableName, result);

            return result;
        }

        #endregion

        #region Delete Method

        /// <summary>
        /// Delete Mothod : the [Key] Attribute must be provide!
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete<TEntity>(TEntity entity) where TEntity : class
        {
            int keyCount = 0;

            Type entityType = typeof(TEntity);

            string tableName = GetTablaName(entityType);

            StringBuilder builder_front = new StringBuilder();
            StringBuilder builder_where = new StringBuilder();

            List<SqlParameter> sqlParameterList = new List<SqlParameter>();

            builder_front.Append("DELETE ");
            builder_front.Append(tableName);
            builder_where.Append(" WHERE ");

            PropertyInfo[] propertyInfos = entityType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //key :
                if (propertyInfo.GetCustomAttribute(typeof(KeyAttribute), true) is KeyAttribute keyAttr)
                {
                    builder_where.Append(propertyInfo.Name);
                    builder_where.Append("=");
                    builder_where.Append("@");
                    builder_where.Append(propertyInfo.Name);

                    sqlParameterList.Add(new SqlParameter("@" + propertyInfo.Name, propertyInfo.GetValue(entity)));

                    keyCount++;
                }
            }

            if (keyCount == 0)
            {
                throw new Exception_DG("[KeyAttribute] must be provide.");
            }
            else if (keyCount > 1)
            {
                throw new Exception_DG("[KeyAttribute] only one suppport.");
            }

            //Generate SqlStatement
            string sql = builder_front.Append(builder_where.ToString()).ToString();

            this.SqlStatement = sql;

            //Execute Task That Execute SqlStatement
            bool result = ExecuteNonQuery(sql, System.Data.CommandType.Text, sqlParameterList.ToArray()) > 0;

            // Cache Deal:if Table change,we should renew the cache value
            HttpRuntimeCache_Helper_DG.Cache_Add(tableName, result);

            return result;
        }

        /// <summary>
        /// Delete Method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool Delete<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            string lambdaString = where.ToString();

            string tableName = GetTablaName(typeof(TEntity));

            StringBuilder builder_front = new StringBuilder();

            List<SqlParameter> sqlParameterList = new List<SqlParameter>();

            builder_front.Append("DELETE ");
            builder_front.Append(lambdaString.Substring(0, lambdaString.IndexOf("=>")));
            builder_front.Append(" From ");
            builder_front.Append(tableName);
            builder_front.Append(" ");
            builder_front.Append(lambdaString.LambdaToSqlStatement());

            //Generate SqlStatement
            string sql = builder_front.ToString();

            this.SqlStatement = sql;

            //Execute Task That Execute SqlStatement
            bool result = ExecuteNonQuery(sql, System.Data.CommandType.Text, sqlParameterList.ToArray()) > 0;

            // Cache Deal:if Table change,we should renew the cache value
            HttpRuntimeCache_Helper_DG.Cache_Add(tableName, result);

            return result;
        }

        #endregion

        #region Query Method -- QueryExist/Count/Entity/Entities/EntitiesPaging/DataTable

        /// <summary>
        /// QueryExist
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool QueryExist<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));

            StringBuilder builder = new StringBuilder();

            builder.Append("SELECT COUNT(0) FROM ");
            builder.Append(tableName);
            builder.Append(LinQLambdaToSql.ConvertWhere(where));

            //Generate SqlStatement
            string sql = builder.ToString();

            this.SqlStatement = sql;

            //Cache Support
            string cacheKey = string.Concat("QueryExist_bool", tableName, sql).GetHashCode().ToString();

            object result = CacheChannel(tableName, cacheKey, () =>
           {
               return ExecuteScalar(sql);
           });

            return result.ToInt() > 0;
        }

        /// <summary>
        /// QueryCount
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public int QueryCount<TEntity>() where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));

            //Generate SqlStatement
            string sql = "SELECT COUNT(0) FROM " + tableName;

            this.SqlStatement = sql;

            //Cache Support
            string cacheKey = string.Concat("QueryCount_int", tableName, sql).GetHashCode().ToString();

            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteScalar(sql);
            });

            return result.ToInt();
        }

        /// <summary>
        /// QueryCount
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public int QueryCount<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));

            StringBuilder builder = new StringBuilder();


            builder.Append("SELECT COUNT(0) FROM ");
            builder.Append(tableName);
            builder.Append(LinQLambdaToSql.ConvertWhere(where));

            //Generate SqlStatement
            string sql = builder.ToString();

            this.SqlStatement = sql;

            //Cache Support
            string cacheKey = string.Concat("QueryCount_int", tableName, sql).GetHashCode().ToString();

            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteScalar(sql);
            });

            return result.ToInt();
        }

        /// <summary>
        /// QueryEntity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public TEntity QueryEntity<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));

            StringBuilder builder = new StringBuilder();

            builder.Append("SELECT TOP (1) * FROM ");
            builder.Append(tableName);
            builder.Append(LinQLambdaToSql.ConvertWhere(where));

            //Generate SqlStatement
            string sql = builder.ToString();

            this.SqlStatement = sql;

            //Cache Support
            string cacheKey = string.Concat("QueryEntity_TEntity", tableName, sql).GetHashCode().ToString();

            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteDataSetTo_TEntity<TEntity>(ExecuteDataSet(sql));
            });

            return result as TEntity;
        }

        /// <summary>
        /// QueryEntities
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public List<TEntity> QueryEntities<TEntity>() where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));

            //Generate SqlStatement
            string sql = "SELECT * FROM " + tableName;

            this.SqlStatement = sql;

            //Cache Support
            string cacheKey = string.Concat("QueryEntities_List<TEntity>", tableName, sql).GetHashCode().ToString();

            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteDataSetToList_TEntity<TEntity>(ExecuteDataSet(sql));
            });

            return result as List<TEntity>;
        }

        /// <summary>
        /// QueryEntities
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<TEntity> QueryEntities<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));

            StringBuilder builder = new StringBuilder();

            builder.Append("SELECT * FROM ");
            builder.Append(tableName);
            builder.Append(LinQLambdaToSql.ConvertWhere(where));

            //Generate SqlStatement
            string sql = builder.ToString();

            this.SqlStatement = sql;

            //Cache Support
            string cacheKey = string.Concat("QueryEntities_List<TEntity>", tableName, sql).GetHashCode().ToString();

            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteDataSetToList_TEntity<TEntity>(ExecuteDataSet(sql));
            });

            return result as List<TEntity>;
        }

        /// <summary>
        /// QueryEntitiesPaging
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="isDESC"></param>
        /// <returns></returns>
        public List<TEntity> QueryEntitiesPaging<TEntity>(int pageIndex, int pageSize, Expression<Func<TEntity, object>> orderBy, bool isDESC = false) where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));

            StringBuilder builder = new StringBuilder();

            builder.Append("SELECT TOP ");
            builder.Append(pageSize);
            builder.Append(" * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ");
            builder.Append(LinQLambdaToSql.ConvertOrderBy(orderBy));
            if (isDESC) builder.Append(" DESC "); else builder.Append(" ASC ");
            builder.Append(") AS RowNumber,* FROM ");
            builder.Append(tableName);
            builder.Append(" ");
            builder.Append(orderBy.Parameters.FirstOrDefault().Name);
            builder.Append(" WHERE 1=1) AS TTTAAABBBLLLEEE  WHERE RowNumber > (");
            builder.Append(pageSize);
            builder.Append(" * (");
            builder.Append(pageIndex);
            builder.Append(" - 1))");

            //Generate SqlStatement
            string sql = builder.ToString();

            this.SqlStatement = sql;

            //Cache Support
            string cacheKey = string.Concat("QueryEntitiesPaging_List<TEntity>", tableName, sql).GetHashCode().ToString();

            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteDataSetToList_TEntity<TEntity>(ExecuteDataSet(sql));
            });

            return result as List<TEntity>;
        }

        /// <summary>
        /// QueryEntitiesPaging
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="where"></param>
        /// <param name="isDESC"></param>
        /// <returns></returns>
        public List<TEntity> QueryEntitiesPaging<TEntity>(int pageIndex, int pageSize, Expression<Func<TEntity, object>> orderBy, Expression<Func<TEntity, bool>> where, bool isDESC = false) where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));

            StringBuilder builder = new StringBuilder();


            builder.Append("SELECT TOP ");
            builder.Append(pageSize);
            builder.Append(" * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ");
            builder.Append(LinQLambdaToSql.ConvertOrderBy(orderBy));
            if (isDESC) builder.Append(" DESC "); else builder.Append(" ASC ");
            builder.Append(") AS RowNumber,* FROM ");
            builder.Append(tableName);
            builder.Append(LinQLambdaToSql.ConvertWhere(where));
            builder.Append(") AS TTTAAABBBLLLEEE  WHERE RowNumber > (");
            builder.Append(pageSize);
            builder.Append(" * (");
            builder.Append(pageIndex);
            builder.Append(" - 1))");

            //Generate SqlStatement
            string sql = builder.ToString();

            this.SqlStatement = sql;

            //Cache Support
            string cacheKey = string.Concat("QueryEntitiesPaging_List<TEntity>", tableName, sql).GetHashCode().ToString();

            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteDataSetToList_TEntity<TEntity>(ExecuteDataSet(sql));
            });

            return result as List<TEntity>;
        }

        /// <summary>
        /// QueryEntitiesPaging
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="count"></param>
        /// <param name="isDESC"></param>
        /// <returns></returns>
        public List<TEntity> QueryEntitiesPaging<TEntity>(int pageIndex, int pageSize, Expression<Func<TEntity, object>> orderBy, out int count, bool isDESC = false) where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));

            StringBuilder builder = new StringBuilder();

            builder.Append("SELECT TOP ");
            builder.Append(pageSize);
            builder.Append(" * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ");
            builder.Append(LinQLambdaToSql.ConvertOrderBy(orderBy));
            if (isDESC) builder.Append(" DESC "); else builder.Append(" ASC ");
            builder.Append(") AS RowNumber,* FROM ");
            builder.Append(tableName);
            builder.Append(" ");
            builder.Append(orderBy.Parameters.FirstOrDefault().Name);
            builder.Append(" WHERE 1=1) AS TTTAAABBBLLLEEE  WHERE RowNumber > (");
            builder.Append(pageSize);
            builder.Append(" * (");
            builder.Append(pageIndex);
            builder.Append(" - 1))");

            //Generate SqlStatement
            string sql = builder.ToString();

            this.SqlStatement = sql;

            count = QueryCount<TEntity>();

            //Cache Support
            string cacheKey = string.Concat("QueryEntitiesPaging_List<TEntity>", tableName, sql).GetHashCode().ToString();

            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteDataSetToList_TEntity<TEntity>(ExecuteDataSet(sql));
            });

            return result as List<TEntity>;
        }

        /// <summary>
        /// QueryEntitiesPaging
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="where"></param>
        /// <param name="count"></param>
        /// <param name="isDESC"></param>
        /// <returns></returns>
        public List<TEntity> QueryEntitiesPaging<TEntity>(int pageIndex, int pageSize, Expression<Func<TEntity, object>> orderBy, Expression<Func<TEntity, bool>> where, out int count, bool isDESC = false) where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));

            StringBuilder builder = new StringBuilder();


            builder.Append("SELECT TOP ");
            builder.Append(pageSize);
            builder.Append(" * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ");
            builder.Append(LinQLambdaToSql.ConvertOrderBy(orderBy));
            if (isDESC) builder.Append(" DESC "); else builder.Append(" ASC ");
            builder.Append(") AS RowNumber,* FROM ");
            builder.Append(tableName);
            builder.Append(LinQLambdaToSql.ConvertWhere(where));
            builder.Append(") AS TTTAAABBBLLLEEE  WHERE RowNumber > (");
            builder.Append(pageSize);
            builder.Append(" * (");
            builder.Append(pageIndex);
            builder.Append(" - 1))");

            //Generate SqlStatement
            string sql = builder.ToString();

            this.SqlStatement = sql;

            //Get Data Count
            count = QueryCount(where);

            //Cache Support
            string cacheKey = string.Concat("QueryEntitiesPaging_List<TEntity>", tableName, sql).GetHashCode().ToString();

            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteDataSetToList_TEntity<TEntity>(ExecuteDataSet(sql));
            });

            return result as List<TEntity>;
        }

        /// <summary>
        /// QueryDataTable<TEntity>
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="where">where</param>
        /// <returns></returns>
        public DataTable QueryDataTable<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));

            StringBuilder builder = new StringBuilder();

            builder.Append("SELECT * FROM ");
            builder.Append(tableName);
            builder.Append(LinQLambdaToSql.ConvertWhere(where));

            //Generate SqlStatement
            string sql = builder.ToString();

            this.SqlStatement = sql;

            //Cache Support
            string cacheKey = string.Concat("QueryDataTable_DataTable", tableName, sql).GetHashCode().ToString();

            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteDataTable(sql);
            });
            return result as DataTable;
        }

        public DataTable QueryDataTable<TEntity>(string sql, params SqlParameter[] parms) where TEntity : class
        {

            this.SqlStatement = sql;

            string tableName = GetTablaName(typeof(TEntity));
            string cacheKey = string.Concat("QueryDataTable_DataTable", tableName, sql).GetHashCode().ToString();
            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteDataTable(sql, CommandType.Text, parms);
            });
            return result as DataTable;
        }

        /// <summary>
        /// QueryDataSet<TEntity>
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="where">where</param>
        /// <returns></returns>
        public DataSet QueryDataDataSet<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));

            StringBuilder builder = new StringBuilder();

            builder.Append("SELECT * FROM ");
            builder.Append(tableName);
            builder.Append(LinQLambdaToSql.ConvertWhere(where));

            //Generate SqlStatement
            string sql = builder.ToString();

            this.SqlStatement = sql;

            //Cache Support
            string cacheKey = string.Concat("QueryDataDataSet_DataSet", tableName, sql).GetHashCode().ToString();

            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteDataSet(sql);
            });

            return result as DataSet;
        }

        public DataSet QueryDataDataSet<TEntity>(string sql, params SqlParameter[] parms) where TEntity : class
        {
            this.SqlStatement = sql;
            string tableName = GetTablaName(typeof(TEntity));
            string cacheKey = string.Concat("QueryDataDataSet_DataSet", tableName, sql).GetHashCode().ToString();
            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteDataSet(sql, CommandType.Text, parms);
            });
            return result as DataSet;
        }

        #endregion

        #region Execute Method

        /// <summary>
        /// ExecuteSql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public int ExecuteSql<TEntity>(string sql, params SqlParameter[] parms) where TEntity : class
        {
            this.SqlStatement = sql;
            string tableName = GetTablaName(typeof(TEntity));
            string cacheKey = string.Concat("ExecuteSql_int", tableName, sql).GetHashCode().ToString();
            object result = CacheChannel(tableName, cacheKey, () =>
              {
                  return ExecuteNonQuery(sql, CommandType.Text, parms);
              });
            // Cache Deal:if Table change,we should renew the cache value
            HttpRuntimeCache_Helper_DG.Cache_Add(tableName, result);
            return result.ToInt();
        }

        /// <summary>
        /// ExecuteList
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<TEntity> ExecuteSqlToList<TEntity>(string sql, params SqlParameter[] parms) where TEntity : class
        {
            this.SqlStatement = sql;
            string tableName = GetTablaName(typeof(TEntity));
            string cacheKey = string.Concat("ExecuteSqlToList_List<TEntity>", tableName, sql).GetHashCode().ToString();
            object result = CacheChannel(tableName + cacheKey, cacheKey, () =>
            {
                return Return_List_T_ByDataSet<TEntity>(ExecuteDataSet(sql, CommandType.Text, parms)); ;
            });
            return result as List<TEntity>;
        }

        /// <summary>
        /// ExecuteStoredProcedure
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public int ExecuteStoredProcedure<TEntity>(string storedProcedureName, params object[] parms) where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));
            string cacheKey = string.Concat("ExecuteStoredProcedure_int", tableName, storedProcedureName).GetHashCode().ToString();
            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteNonQuery(storedProcedureName, CommandType.StoredProcedure, parms);
            });
            // Cache Deal:if Table change,we should renew the cache value
            HttpRuntimeCache_Helper_DG.Cache_Add(tableName, result);
            return result.ToInt();
        }

        /// <summary>
        /// ExecuteStoredProcedureToList
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="storedProcedureName"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<TEntity> ExecuteStoredProcedureToList<TEntity>(string storedProcedureName, params object[] parms) where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));
            string cacheKey = string.Concat("ExecuteStoredProcedureToList_List<TEntity>", tableName, storedProcedureName).GetHashCode().ToString();
            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return Return_List_T_ByDataSet<TEntity>(ExecuteDataSet(storedProcedureName, CommandType.StoredProcedure, parms)); ;
            });
            return result as List<TEntity>;
        }

        #endregion

        #region prepare

        /// <summary>
        /// Get Table Name by Attribute
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetTablaName(Type type)
        {
            object[] objs = type.GetCustomAttributes(typeof(TableAttribute), true);
            if (objs.FirstOrDefault() is TableAttribute attr)
                return !string.IsNullOrEmpty(attr.TableName) == true ? attr.TableName : type.Name;
            return type.Name;
        }

        /// <summary>
        /// ExecuteDataSetTo_TEntity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ds"></param>
        /// <returns></returns>
        private static TEntity ExecuteDataSetTo_TEntity<TEntity>(DataSet ds) where TEntity : class => ExecuteDataSetToList_TEntity<TEntity>(ds).FirstOrDefault();

        /// <summary>
        /// ExecuteDataSetToList_TEntity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ds"></param>
        /// <returns></returns>
        private static List<TEntity> ExecuteDataSetToList_TEntity<TEntity>(DataSet ds) where TEntity : class
        {
            try
            {
                List<TEntity> list = new List<TEntity>();//instantiate a list object
                PropertyInfo[] propertyInfos = typeof(TEntity).GetProperties();     //get properties

                DataTable dt = ds.Tables[0];    //get datatable
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        TEntity model = System.Activator.CreateInstance<TEntity>();//create instance of entity
                        foreach (PropertyInfo propertyInfo in propertyInfos)
                        {
                            if (propertyInfo.GetCustomAttribute(typeof(KeyAttribute), true) is KeyAttribute keyAttr)
                            {
                                model = SetValueToTEntityFromRows(model, propertyInfo, row, propertyInfo.Name);
                                continue;
                            }

                            //column
                            if (propertyInfo.GetCustomAttribute(typeof(ColumnAttribute), true) is ColumnAttribute columnAttr)
                            {
                                model = SetValueToTEntityFromRows(model, propertyInfo, row, propertyInfo.Name);
                                continue;
                            }

                            //if foreignKey
                            /**
                             * Update:2017-8-22 16:22:13
                             * Desc:plus foreign search support
                             * */
                            if (propertyInfo.GetCustomAttribute(typeof(ForeignTableAttribute), true) is ForeignTableAttribute foreignTableAttr)
                            {
                                string keyName = foreignTableAttr.ForeignKeyFieldName;
                                //set current key value to keyValue then call ForeignQueryEntity to get foreign value
                                if (string.IsNullOrEmpty(keyName))
                                    throw new Exception_DG("foreign table must support key column ! -- qixiao");

                                //Generate a list.
                                Type generic = typeof(List<>).MakeGenericType(new Type[] { propertyInfo.PropertyType });
                                var propertyList = Activator.CreateInstance(generic) as IList;
                                //get query values from method ForeignQueryEntity and set value to list.
                                propertyList = _foreignQueryEntity.MakeGenericMethod(propertyInfo.PropertyType).Invoke(null, null) as IList;

                                foreach (var item in propertyList)
                                {
                                    /**
                                     * 1.get the foreign table KeyAttribute marked PropertyInfo
                                     * 2.get KeyAttribute PropertyInfo`s value from foreign table instance
                                     * 3.compare the value ,set foreign object value if match (LIKE linq FirstOrDefault() method); 
                                     * */
                                    if (GetOnlyOneKeyAttributeMarkPropertyInfo(propertyInfo.PropertyType).GetValue(item).Equals(row[keyName]))
                                    {
                                        propertyInfo.SetValue(model, item);
                                        break;
                                    }
                                }
                            }
                        }
                        list.Add(model);
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
        /// SetValueToTEntityFromRows
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="row"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private static TEntity SetValueToTEntityFromRows<TEntity>(TEntity entity, PropertyInfo propertyInfo, DataRow row, string columnName)
        {
            if (row[columnName] != System.DBNull.Value)
            {
                if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    //if convertsionType is type of nullable，declare a NullableConverter class that support method of convert Nullable class to underlyingType -- qi xiao
                    NullableConverter nullableConverter = new NullableConverter(propertyInfo.PropertyType);
                    //convert nullable type to underlying type -- qixiao
                    propertyInfo.SetValue(entity, Convert.ChangeType(row[columnName], nullableConverter.UnderlyingType), null);
                }
                else
                {
                    propertyInfo.SetValue(entity, Convert.ChangeType(row[columnName], propertyInfo.PropertyType), null);
                }
            }
            else
            {
                propertyInfo.SetValue(entity, null, null);
            }
            return entity;
        }

        #region Help Method with Reflector
        /// <summary>
        /// MethodInfo
        /// </summary>
        private static readonly MethodInfo _foreignQueryEntity = typeof(Bankinate).GetMethod("QueryForeignEntity", BindingFlags.NonPublic | BindingFlags.Static);
        #endregion

        /// <summary>
        /// ForeignQueryEntity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="value">value</param>
        /// <returns></returns>
        private static List<TEntity> QueryForeignEntity<TEntity>() where TEntity : class
        {
            string tableName = GetTablaName(typeof(TEntity));

            string sql = $"SELECT * FROM {tableName}";

            #region  Obsolete code
            /**
             * update query single each item to query * and storage into cache,then get value from cache
             * means the foreign Table may query all from database
             * -- qixiao 2017-8-22 17:12:50 
             * */

            //StringBuilder builder = new StringBuilder();

            //builder.Append("SELECT TOP (1) * FROM ");
            //builder.Append(tableName);
            //builder.Append(" where ");

            //SqlParameter[] parms = new SqlParameter[1];

            //int keyCount = 0;
            //PropertyInfo[] propertyInfos = typeof(TEntity).GetProperties();
            //foreach (PropertyInfo propertyInfo in propertyInfos)
            //{
            //    //key :
            //    if (propertyInfo.GetCustomAttribute(typeof(KeyAttribute), true) is KeyAttribute keyAttr)
            //    {
            //        builder.Append(propertyInfo.Name);
            //        builder.Append("=");
            //        builder.Append("@");
            //        builder.Append(propertyInfo.Name);
            //        parms[0] = new SqlParameter(propertyInfo.Name, value);

            //        keyCount++;
            //    }
            //}

            //if (keyCount == 0)
            //{
            //    throw new Exception_DG("[KeyAttribute] must be provide.");
            //}
            //else if (keyCount > 1)
            //{
            //    throw new Exception_DG("[KeyAttribute] only one suppport.");
            //}

            //Generate SqlStatement
            //string sql = builder.ToString();
            #endregion

            //Cache Support
            string cacheKey = string.Concat("QueryForeignEntity_TEntity", tableName, sql).GetHashCode().ToString();

            object result = CacheChannel(tableName, cacheKey, () =>
            {
                return ExecuteDataSetToList_TEntity<TEntity>(ExecuteDataSet(sql));
            });

            return result as List<TEntity>;
        }

        /// <summary>
        /// Get Key PropertyInfo from classType
        /// </summary>
        /// <param name="classType"></param>
        /// <returns></returns>
        private static PropertyInfo GetOnlyOneKeyAttributeMarkPropertyInfo(Type classType)
        {
            int keyCount = 0;
            PropertyInfo property = default(PropertyInfo);
            foreach (PropertyInfo propertyInfo in classType.GetProperties())
            {
                //key :
                if (propertyInfo.GetCustomAttribute(typeof(KeyAttribute), true) is KeyAttribute keyAttr)
                {
                    property = propertyInfo;
                    keyCount++;
                }
            }
            if (keyCount == 0)
            {
                throw new Exception_DG("[KeyAttribute] must be provide.");
            }
            else if (keyCount > 1)
            {
                throw new Exception_DG("[KeyAttribute] only one suppport.");
            }
            return property;
        }

        #endregion

        #region Cache Support

        /// <summary>
        /// Cache Channel
        /// </summary>
        /// <param name="tableName">tableName</param>
        /// <param name="cacheKey">cacheKey</param>
        /// <param name="func">Func<object></param>
        /// <returns></returns>
        private static object CacheChannel(string tableName, string cacheKey, Func<object> func)
        {
            /**
             * author:qixiao
             * create:2017-8-7 22:47:13
             * Howw to judge a cache lose efficacy when we update the table data ？
             * we add another cache name changeCache， let key = tableName，expire time default ，like 1.
             * once we gain data from cache ， validate changeCache has any value not null.if it is ,regard value changed,we should gain table data renew not from cache.
             * if it is null , regard as unchanged, if cacheCache expire ,dataCache expired too,we can gain cache relieved
             * */
            if (HttpRuntimeCache_Helper_DG.Cache_Get(tableName) == null)
            {
                object cacheValue = HttpRuntimeCache_Helper_DG.Cache_Get(cacheKey);
                if (cacheValue != null)
                {
                    return cacheValue;
                }
            }

            //Execute Action
            object result = func();

            if (result != null)
                HttpRuntimeCache_Helper_DG.Cache_Add(cacheKey, result);

            HttpRuntimeCache_Helper_DG.Cache_Delete(tableName);

            return result;
        }

        #endregion

        #region Transaction Support
        /// <summary>
        /// Transacation
        /// </summary>
        /// <param name="action"></param>
        public void Transaction(Action action)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                action();
                trans.Complete();
            }
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Dispose
        /// </summary>

        public void Dispose()
        {

        }
        #endregion
    }
}
