using System;
using Autofac;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using QX_Frame.Helper_DG_Framework;
using QX_Frame.App.Base;
using System.Data.Entity;

namespace QX_Frame.App.Web
{
    //author:qixiao
    //time:2017-1-13 14:09:05
    public abstract class WcfService : Dependency, IWcfService
    {
        private static IContainer _container;
        static WcfService()
        {
            _container = Dependency.Factory();
        }
        protected static ChannelFactory<TService> Wcf<TService>()
        {
            return new ChannelFactory<TService>(_container);
        }
        //use reflector to getMethod
        private static readonly MethodInfo _getCount = typeof(WcfService).GetMethod("GetCount", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly MethodInfo _getEntities = typeof(WcfService).GetMethod("GetEntities", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly MethodInfo _getEntity = typeof(WcfService).GetMethod("GetEntity", BindingFlags.NonPublic | BindingFlags.Static);
        private static int _totalCount { get; set; } = 0;//the query result count

        private static int GetCount<DBEntity, TBEntity>(WcfQueryObject<DBEntity, TBEntity> query) where DBEntity : DbContext where TBEntity : class
        {
            int count = 0;
            IQueryable<TBEntity> source = EF_Helper_DG<DBEntity>.selectAll<TBEntity>(query.BuildQueryFunc<TBEntity>(), out count);
            return count;
        }

        private static object GetEntities<DBEntity, TBEntity>(WcfQueryObject<DBEntity, TBEntity> query) where DBEntity : DbContext where TBEntity : class
        {
            IQueryable<TBEntity> source = null;
            int count = 0;
            if (query.QueryOrderBy != null)
            {
                if (query.PageIndex >= 0 && query.PageSize > 0)
                {
                    source = EF_Helper_DG<DBEntity>.selectAllPaging(query.PageIndex, query.PageSize, query.QueryOrderBy, query.BuildQueryFunc<TBEntity>(), out count, query.IsDESC);
                }
                else
                {
                    source = EF_Helper_DG<DBEntity>.selectAll(query.QueryOrderBy, query.BuildQueryFunc<TBEntity>(), out count, query.IsDESC);
                }
            }
            else
            {
                source = EF_Helper_DG<DBEntity>.selectAll(query.BuildQueryFunc<TBEntity>(), out count);
            }
            _totalCount = count;
            return source.ToList();
        }

        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "type")]
        private static object GetEntity<DBEntity, TBEntity>(WcfQueryObject<DBEntity, TBEntity> query) where DBEntity : DbContext where TBEntity : class
        {
            return EF_Helper_DG<DBEntity>.selectSingle(query.BuildQueryFunc<TBEntity>());
        }

        //query method
        public WcfQueryResult QueryAll(WcfQueryObject query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            System.Type[] typeArguments = new System.Type[] { query.db_type, query.tb_type };
            object[] parameters = new object[] { query };
            return new WcfQueryResult(_getEntities.MakeGenericMethod(typeArguments).Invoke(null, parameters)) { TotalCount = _totalCount };
        }

        public int QueryCount(WcfQueryObject query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            System.Type[] typeArguments = new System.Type[] { query.db_type, query.tb_type };
            object[] parameters = new object[] { query };
            return (int)_getCount.MakeGenericMethod(typeArguments).Invoke(null, parameters);
        }

        public WcfQueryResult QuerySingle(WcfQueryObject query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            System.Type[] typeArguments = new System.Type[] { query.db_type, query.tb_type };
            object[] parameters = new object[] { query };
            return new WcfQueryResult(_getEntity.MakeGenericMethod(typeArguments).Invoke(null, parameters)) { TotalCount = 1 };
        }
    }
}
