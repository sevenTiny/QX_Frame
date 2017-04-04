using System;
using System.Linq.Expressions;

namespace QX_Frame.App.Base
{
    /**
     * author:qixiao
     * time:2017-4-4 11:21:51
     * update
     * */
    public class WcfQueryObject<DB_Entity, TB_Entity> : WcfQueryObject
    {
        public WcfQueryObject()
        {
            base.SetType(typeof(DB_Entity), typeof(TB_Entity));//set TEntity to WcfQueryObject SetType
        }
        /// <summary>
        /// build query condition func wcfservice use it
        /// </summary>
        /// <typeparam name="TProxy"></typeparam>
        /// <returns></returns>
        public Expression<Func<TB_Entity, bool>> BuildQueryFunc<TProxy>()
        {
            return this.QueryCondition;
        }
        //query condition default t=>true
        public virtual Expression<Func<TB_Entity, bool>> QueryCondition { get; set; } = t => true;
    }
}
