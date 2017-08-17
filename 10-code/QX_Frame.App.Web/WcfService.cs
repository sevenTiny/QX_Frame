using System;
using Autofac;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Data.Entity;
using System.Reflection;
using QX_Frame.Helper_DG;
using System.Linq.Expressions;
using QX_Frame.App.Base;

namespace QX_Frame.App.Web
{
    //author:qixiao
    //time:2017-1-13 14:09:05
    /**
     * there WcfService can not implement IWcfService if done,influnce the RESTFul Controller Action Post method match -- qx_frame 2017-2-27 09:59:17
     **/
    public abstract class WcfService : Dependency
    {
        static WcfService() { }

        #region Ioc Factory
        private static IContainer _container;
        private static int ExecuteTimes = 0;    //register execute times
        /// <summary>
        /// RegisterComplex  execute when register complex !
        /// </summary>
        protected static void RegisterComplex()
        {
            if (ExecuteTimes <= 0)
            {
                _container = Dependency.Factory();
                ExecuteTimes++;
            }
            else
            {
                new Exception(nameof(RegisterComplex) + " Method can not be used more than one times in a class -- QX_Frame");
            }
        }
        public static ChannelFactory<TService> Wcf<TService>()
        {
            return new ChannelFactory<TService>(_container);
        }
        #endregion

        #region  help method use reflector
        //use reflector to getMethod
        private static readonly MethodInfo _getCount = typeof(WcfService).GetMethod("GetCount", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly MethodInfo _getEntities = typeof(WcfService).GetMethod("GetEntities", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly MethodInfo _getEntitiesPaging = typeof(WcfService).GetMethod("GetEntitiesPaging", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly MethodInfo _getEntity = typeof(WcfService).GetMethod("GetEntity", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly MethodInfo _executeSql = typeof(WcfService).GetMethod("ExecuteSql", BindingFlags.NonPublic | BindingFlags.Static);

        private static int _totalCount { get; set; } = 0;//the query result count

        private static int GetCount<DBEntity, TBEntity>(WcfQueryObject<DBEntity, TBEntity> query) where DBEntity : DbContext where TBEntity : class
        {
            int count = 0;
            IQueryable<TBEntity> source = EF_Helper_DG<DBEntity>.SelectAll<TBEntity>(query.BuildQueryFunc<TBEntity>(), out count);
            return count;
        }

        private static object GetEntities<DBEntity, TBEntity>(WcfQueryObject<DBEntity, TBEntity> query) where DBEntity : DbContext where TBEntity : class
        {
            IQueryable<TBEntity> source = null;
            int count = 0;
            source = EF_Helper_DG<DBEntity>.SelectAll(query.BuildQueryFunc<TBEntity>(), out count);
            _totalCount = count;
            return source.ToList();
        }

        private static object GetEntitiesPaging<DBEntity, TBEntity, TKey>(WcfQueryObject<DBEntity, TBEntity> query, Expression<Func<TBEntity, TKey>> orderBy) where DBEntity : DbContext where TBEntity : class
        {
            IQueryable<TBEntity> source = null;
            int count = 0;
            if (query.PageIndex >= 0 && query.PageSize > 0)
            {
                source = EF_Helper_DG<DBEntity>.SelectAllPaging(query.PageIndex, query.PageSize, orderBy, query.BuildQueryFunc<TBEntity>(), out count, query.IsDESC);
            }
            else
            {
                source = EF_Helper_DG<DBEntity>.SelectAll(orderBy, query.BuildQueryFunc<TBEntity>(), out count, query.IsDESC);
            }
            _totalCount = count;
            return source.ToList();
        }

        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "type")]
        private static object GetEntity<DBEntity, TBEntity>(WcfQueryObject<DBEntity, TBEntity> query) where DBEntity : DbContext where TBEntity : class
        {
            return EF_Helper_DG<DBEntity>.SelectSingle(query.BuildQueryFunc<TBEntity>());
        }
        //sql query
        private static object ExecuteSql<DBEntity, TBEntity>(WcfQueryObject<DBEntity, TBEntity> query) where DBEntity : DbContext where TBEntity : class
        {
            if (String.IsNullOrEmpty(query.SqlConnectionString))
            {
                throw new Exception("SqlConnectionString can not be null ! -- QX_Frame");
            }
            if (String.IsNullOrEmpty(query.SqlStatementTextOrSpName))
            {
                throw new Exception("QuerySqlStatementTextOrSpName can not be null ! -- QX_Frame");
            }
            //query execute
            object executeResult = new object();
            switch (query.SqlExecuteType)
            {
                case Base.Options.ExecuteType.ExecuteNonQuery:
                    executeResult = Sql_Helper_DG.ExecuteNonQuery(query.SqlStatementTextOrSpName, query.SqlCommandType, query.SqlParameters);
                    break;
                case Base.Options.ExecuteType.ExecuteScalar:
                    executeResult = Sql_Helper_DG.ExecuteScalar(query.SqlStatementTextOrSpName, query.SqlCommandType, query.SqlParameters);
                    break;
                case Base.Options.ExecuteType.ExecuteReader:
                    executeResult = Sql_Helper_DG.ExecuteReader(query.SqlStatementTextOrSpName, query.SqlCommandType, query.SqlParameters);
                    break;
                case Base.Options.ExecuteType.ExecuteDataTable:
                    executeResult = Sql_Helper_DG.ExecuteDataTable(query.SqlStatementTextOrSpName, query.SqlCommandType, query.SqlParameters);
                    break;
                case Base.Options.ExecuteType.ExecuteDataSet:
                    executeResult = Sql_Helper_DG.ExecuteDataSet(query.SqlStatementTextOrSpName, query.SqlCommandType, query.SqlParameters);
                    break;
                case Base.Options.ExecuteType.Execute_Model_T:
                    executeResult = Sql_Helper_DG.Return_T_ByDataReader<TBEntity>(Sql_Helper_DG.ExecuteReader(query.SqlStatementTextOrSpName, query.SqlCommandType, query.SqlParameters));
                    break;
                case Base.Options.ExecuteType.Execute_List_T:
                    executeResult = Sql_Helper_DG.Return_List_T_ByDataSet<TBEntity>(Sql_Helper_DG.ExecuteDataSet(query.SqlStatementTextOrSpName, query.SqlCommandType, query.SqlParameters));
                    break;
                case Base.Options.ExecuteType._ChooseOthers_IfYouChooseThisYouWillGetAnException:
                    throw new Exception("must choose the right ExecuteType ! -- QX_Frame");
            }
            return executeResult;
        }
        #endregion

        #region public query method region

        protected WcfQueryResult QueryAll(WcfQueryObject query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            System.Type[] typeArguments = new System.Type[] { query.db_type, query.tb_type };
            object[] parameters = new object[] { query };
            return new WcfQueryResult(_getEntities.MakeGenericMethod(typeArguments).Invoke(null, parameters)) { TotalCount = _totalCount };
        }
        protected WcfQueryResult QueryAllPaging<TBEntity, TKey>(WcfQueryObject query, Expression<Func<TBEntity, TKey>> orderBy) where TBEntity : class
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            if (orderBy == null)
            {
                throw new ArgumentNullException("if you want to paging must use OrderBy arguments  -- QX_Frame");
            }
            System.Type[] typeArguments = new System.Type[] { query.db_type, query.tb_type, typeof(TKey) };
            object[] parameters = new object[] { query, orderBy };
            return new WcfQueryResult(_getEntitiesPaging.MakeGenericMethod(typeArguments).Invoke(null, parameters)) { TotalCount = _totalCount };
        }

        protected int QueryCount(WcfQueryObject query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            System.Type[] typeArguments = new System.Type[] { query.db_type, query.tb_type };
            object[] parameters = new object[] { query };
            return (int)_getCount.MakeGenericMethod(typeArguments).Invoke(null, parameters);
        }

        protected WcfQueryResult QuerySingle(WcfQueryObject query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            System.Type[] typeArguments = new System.Type[] { query.db_type, query.tb_type };
            object[] parameters = new object[] { query };
            return new WcfQueryResult(_getEntity.MakeGenericMethod(typeArguments).Invoke(null, parameters)) { TotalCount = 1 };
        }

        protected WcfQueryResult QuerySql(WcfQueryObject query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            System.Type[] typeArguments = new System.Type[] { query.db_type, query.tb_type };
            object[] parameters = new object[] { query };
            return new WcfQueryResult(_executeSql.MakeGenericMethod(typeArguments).Invoke(null, parameters)) { TotalCount = 1 };
        }

        #endregion
    }
}
