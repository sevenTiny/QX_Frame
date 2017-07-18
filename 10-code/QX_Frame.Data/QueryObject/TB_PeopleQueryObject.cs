using QX_Frame.App.Base;
using QX_Frame.Data.Entities;
using System;
using System.Linq.Expressions;

/**
 * copyright qixiao code builder ->
 * version:4.2.0
 * author:qixiao(柒小)
 * create:2017-07-18 09:49:05
 **/

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

			return func;
		}
	}
}
