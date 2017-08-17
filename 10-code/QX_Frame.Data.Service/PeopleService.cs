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
using QX_Frame.Data.Contract;
using QX_Frame.Data.Entities;

namespace QX_Frame.Data.Service
{
	/// <summary>
	/// class PeopleService
	/// </summary>
	public class PeopleService:WcfService, IPeopleService
	{
		private TB_People _TB_People;
		/// <summary>
		/// construction method
		/// </summary>
		public PeopleService()
		{}

		public PeopleService(TB_People TB_People)
		{
			this._TB_People = TB_People;
		}
		public bool Add(TB_People TB_People)
		{
			return TB_People.Add(TB_People);
		}
		public bool Update(TB_People TB_People)
		{
			return TB_People.Update(TB_People);
		}
		public bool Delete(TB_People TB_People)
		{
			return TB_People.Delete(TB_People);
		}
	}
}
