using QX_Frame.App.Base;
using QX_Frame.Data.Entities;
using System;
using System.Linq.Expressions;

namespace QX_Frame.Data.QueryObject
{
    /// <summary>
    ///class TB_PeopleQueryObject
    /// </summary>
    public class V_PeopleQueryObject : WcfQueryObject<DB_QX_Frame_Test, V_People>
    {
        /// <summary>
        /// construction method
        /// </summary>
        public V_PeopleQueryObject()
        { }

        // PK（identity）  
        public Guid Uid { get; set; }

        // 
        public String Name { get; set; }

        // 
        public Int32 Age { get; set; }

        // 
        public string ClassName { get; set; }

        //query condition // null default
        public override Expression<Func<V_People, bool>> QueryCondition { get { return base.QueryCondition; } set { base.QueryCondition = value; } }

        //query condition func // true default //if QueryCondition != null this will be override !!!
        protected override Expression<Func<V_People, bool>> QueryConditionFunc()
        {
            Expression<Func<V_People, bool>> func = t => true;

            if (!string.IsNullOrEmpty(""))
            {
                func = func.And(t => true);
            }

            return func;
        }
    }
}
