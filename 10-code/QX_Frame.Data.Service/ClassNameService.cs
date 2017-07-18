using QX_Frame.App.Base;
using QX_Frame.Data.Contract;
using QX_Frame.Data.Entities;

/**
 * copyright qixiao code builder ->
 * version:4.2.0
 * author:qixiao(柒小)
 * create:2017-07-18 09:49:02
 **/

namespace QX_Frame.Data.Service
{
	/// <summary>
	/// class TB_ClassNameService
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
