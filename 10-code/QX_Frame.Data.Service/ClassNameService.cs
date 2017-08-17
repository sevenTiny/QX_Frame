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
using QX_Frame.Data.Contract;
using QX_Frame.Data.Entities;

namespace QX_Frame.Data.Service
{
	/// <summary>
	/// class ClassNameService
	/// </summary>
	public class ClassNameService:WcfService, IClassNameService
	{
		private TB_ClassName _TB_ClassName;
		/// <summary>
		/// construction method
		/// </summary>
		public ClassNameService()
		{}

		public ClassNameService(TB_ClassName TB_ClassName)
		{
			this._TB_ClassName = TB_ClassName;
		}
		public bool Add(TB_ClassName TB_ClassName)
		{
			return TB_ClassName.Add(TB_ClassName);
		}
		public bool Update(TB_ClassName TB_ClassName)
		{
			return TB_ClassName.Update(TB_ClassName);
		}
		public bool Delete(TB_ClassName TB_ClassName)
		{
			return TB_ClassName.Delete(TB_ClassName);
		}
	}
}
