using QX_Frame.Data.Entities;

/**
 * copyright qixiao code builder ->
 * version:4.2.0
 * author:qixiao(柒小)
 * create:2017-07-18 09:49:05
 **/

namespace QX_Frame.Data.Contract
{
	/// <summary>
	/// interface ITB_PeopleService
	/// </summary>
	public interface IPeopleService
	{
		bool Add(TB_People TB_People);
		bool Update(TB_People TB_People);
		bool Delete(TB_People TB_People);
	}
}
