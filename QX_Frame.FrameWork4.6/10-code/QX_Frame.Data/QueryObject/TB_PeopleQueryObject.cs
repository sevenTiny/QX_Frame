/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-08-17 10:30:27
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * Personal WebSit: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Thx , Best Regards ~
 *********************************************************/

using QX_Frame.App.Base;
using QX_Frame.Data.Entities;
using System;
using System.Linq.Expressions;

namespace QX_Frame.Data.QueryObject
{
	/// <summary>
	///class TB_PeopleQueryObject
	/// </summary>
	public class TB_PeopleQueryObject:WcfQueryObject<DB_QX_Frame_Test, TB_People>
	{
		/// <summary>
		/// construction method
		/// </summary>
		public TB_PeopleQueryObject()
		{}

		// PK（identity）  
		public Guid Uid { get;set; }

		// 
		public String Name { get;set; }

		// 
		public Int32 Age { get;set; }

		// 
		public Int32 ClassId { get;set; }

		//query condition // null default
		public override Expression<Func<TB_People, bool>> QueryCondition {get { return base.QueryCondition; } set { base.QueryCondition = value; } }

		//query condition func // true default //if QueryCondition != null this will be override !!!
		protected override Expression<Func<TB_People, bool>> QueryConditionFunc()
		{
			Expression<Func<TB_People, bool>> func = t => true;

			if (!string.IsNullOrEmpty(""))
			{
				func = func.And(t => true);
			}

            if (!string.IsNullOrEmpty(this.Name))
            {
                func = func.And(tt => tt.Name.Contains(this.Name));
            }

            if (this.Age!=0)
            {
                func = func.And(tt => tt.Age == this.Age);
            }

			return func;
		}
	}
}
