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

using QX_Frame.Data.Entities;

namespace QX_Frame.Data.Contract
{
	/// <summary>
	/// interface IPeopleService
	/// </summary>
	public interface IPeopleService
	{
		bool Add(TB_People TB_People);
		bool Update(TB_People TB_People);
		bool Delete(TB_People TB_People);
	}
}
