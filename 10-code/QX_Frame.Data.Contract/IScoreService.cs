using QX_Frame.Data.Entities;

/**
 * copyright qixiao code builder ->
 * version:4.2.0
 * author:qixiao(柒小)
 * create:2017-07-18 09:49:07
 **/

namespace QX_Frame.Data.Contract
{
	/// <summary>
	/// interface ITB_ScoreService
	/// </summary>
	public interface IScoreService
	{
		bool Add(TB_Score TB_Score);
		bool Update(TB_Score TB_Score);
		bool Delete(TB_Score TB_Score);
	}
}
