using System;
using System.Linq.Expressions;

namespace QX_Frame.App.Base
{
    public class WcfQueryObject<DB_Entity, TB_Entity> : WcfQueryObject
    {
        public WcfQueryObject()
        {
            base.SetType(typeof(DB_Entity), typeof(TB_Entity));//set TEntity to WcfQueryObject SetType
        }
        public Expression<Func<TProxy, bool>> BuildQueryFunc<TProxy>() where TProxy : TB_Entity
        {
            return this.QueryFunc<TProxy>();
        }
        protected virtual Expression<Func<TProxy, bool>> QueryFunc<TProxy>() where TProxy : TB_Entity
        {
            throw new NotImplementedException("QueryFunc must be Implemented ! --QX_Frame");
        }
        public string BuildQuerySqlStatement
        {
            get
            {
                return this.QuerySqlStatement;
            }
        }
        protected virtual string QuerySqlStatement
        {
            get
            {
                throw new NotImplementedException("QuerySqlStatement must be Implemented ! --QX_Frame");
            }
        }
    }
}
