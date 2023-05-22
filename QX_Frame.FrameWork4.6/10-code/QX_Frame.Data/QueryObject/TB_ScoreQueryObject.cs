/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-08-17 10:30:29
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
	///class TB_ScoreQueryObject
	/// </summary>
	public class TB_ScoreQueryObject:WcfQueryObject<DB_QX_Frame_Test, TB_Score>
	{
		/// <summary>
		/// construction method
		/// </summary>
		public TB_ScoreQueryObject()
		{}

		// PK（identity）  
		public Guid Uid { get;set; }

		// 
		public Double Score1 { get;set; }

		// 
		public Double Score2 { get;set; }

		// 
		public Double Score3 { get;set; }

		//query condition // null default
		public override Expression<Func<TB_Score, bool>> QueryCondition {get { return base.QueryCondition; } set { base.QueryCondition = value; } }

		//query condition func // true default //if QueryCondition != null this will be override !!!
		protected override Expression<Func<TB_Score, bool>> QueryConditionFunc()
		{
			Expression<Func<TB_Score, bool>> func = t => true;

			if (!string.IsNullOrEmpty(""))
			{
				func = func.And(t => true);
			}

			return func;
		}
	}
}
