using QX_Frame.App.Base;
using QX_Frame.Data.Contract;
using QX_Frame.Data.Entities;

/**
 * copyright qixiao code builder ->
 * version:4.2.0
 * author:qixiao(柒小)
 * create:2017-07-18 09:49:05
 **/

namespace QX_Frame.Data.Service
{
	/// <summary>
	/// class TB_PeopleService
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
