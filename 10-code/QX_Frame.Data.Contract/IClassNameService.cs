using QX_Frame.Data.Entities;

/**
 * copyright qixiao code builder ->
 * version:4.2.0
 * author:qixiao(柒小)
 * create:2017-07-18 09:49:02
 **/

namespace QX_Frame.Data.Contract
{
	/// <summary>
	/// interface ITB_ClassNameService
	/// </summary>
	public interface IClassNameService
	{
		bool Add(TB_ClassName TB_ClassName);
		bool Update(TB_ClassName TB_ClassName);
		bool Delete(TB_ClassName TB_ClassName);
	}
}
