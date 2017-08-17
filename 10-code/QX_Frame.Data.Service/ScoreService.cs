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
using QX_Frame.Data.Contract;
using QX_Frame.Data.Entities;

namespace QX_Frame.Data.Service
{
	/// <summary>
	/// class ScoreService
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
