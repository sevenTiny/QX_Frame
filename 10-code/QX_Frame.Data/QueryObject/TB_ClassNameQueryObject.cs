/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-08-17 10:30:23
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
	///class TB_ClassNameQueryObject
	/// </summary>
	public class TB_ClassNameQueryObject:WcfQueryObject<DB_QX_Frame_Test, TB_ClassName>
	{
		/// <summary>
		/// construction method
		/// </summary>
		public TB_ClassNameQueryObject()
		{}

		// PK（identity）  
		public Int32 ClassId { get;set; }

		// 
		public String ClassName { get;set; }

		//query condition // null default
		public override Expression<Func<TB_ClassName, bool>> QueryCondition {get { return base.QueryCondition; } set { base.QueryCondition = value; } }

		//query condition func // true default //if QueryCondition != null this will be override !!!
		protected override Expression<Func<TB_ClassName, bool>> QueryConditionFunc()
		{
			Expression<Func<TB_ClassName, bool>> func = t => true;

			if (!string.IsNullOrEmpty(""))
			{
				func = func.And(t => true);
			}

			return func;
		}
	}
}
