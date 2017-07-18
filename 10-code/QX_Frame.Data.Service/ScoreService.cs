using QX_Frame.App.Base;
using QX_Frame.Data.Contract;
using QX_Frame.Data.Entities;

/**
 * copyright qixiao code builder ->
 * version:4.2.0
 * author:qixiao(柒小)
 * create:2017-07-18 09:49:07
 **/

namespace QX_Frame.Data.Service
{
	/// <summary>
	/// class TB_ScoreService
	/// </summary>
	public class ScoreService:WcfService, IScoreService
	{
		private TB_Score _TB_Score;
		/// <summary>
		/// construction method
		/// </summary>
		public ScoreService()
		{}

		public ScoreService(TB_Score TB_Score)
		{
			this._TB_Score = TB_Score;
		}
		public bool Add(TB_Score TB_Score)
		{
			return TB_Score.Add(TB_Score);
		}
		public bool Update(TB_Score TB_Score)
		{
			return TB_Score.Update(TB_Score);
		}
		public bool Delete(TB_Score TB_Score)
		{
			return TB_Score.Delete(TB_Score);
		}
	}
}
