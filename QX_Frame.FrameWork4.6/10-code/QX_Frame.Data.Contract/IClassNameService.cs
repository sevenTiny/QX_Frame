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

using QX_Frame.Data.Entities;

namespace QX_Frame.Data.Contract
{
	/// <summary>
	/// interface IClassNameService
	/// </summary>
	public interface IClassNameService
	{
		bool Add(TB_ClassName TB_ClassName);
		bool Update(TB_ClassName TB_ClassName);
		bool Delete(TB_ClassName TB_ClassName);
	}
}
