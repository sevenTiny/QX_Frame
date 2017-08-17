using QX_Frame.App.Base.Options;
using QX_Frame.Helper_DG;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QX_Frame.App.Base
{
    /**
     * author:qixiao
     * time:2017-3-2 16:27:49
     * desc:WcfQueryObject Base
     * */
    public abstract class WcfQueryObject
    {
        private Type _tb_type;
        private Type _db_type;
        protected WcfQueryObject()
        {
        }
        protected void SetType(Type db_type, Type tb_type)
        {
            _db_type = db_type;
            _tb_type = tb_type;
        }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 0;
        public bool IsDESC { get; set; } = false;
        public Type tb_type
        {
            get
            {
                return _tb_type;
            }
        }
        public Type db_type
        {
            get
            {
                return _db_type;
            }
        }
        #region sql query
        public string SqlConnectionString { get; set; } = Sql_Helper_DG.ConnString_Default; // the sql query connstr // QX_Frame_Default default
        public string SqlStatementTextOrSpName { get; set; } = null;   //the sql statement // String.Null default
        public CommandType SqlCommandType { get; set; } = CommandType.Text;    //the CommandType //CommandType.Text default
        public ExecuteType SqlExecuteType { get; set; } = ExecuteType._ChooseOthers_IfYouChooseThisYouWillGetAnException; //the sql execute type // _ChooseOthers_IfYouChooseThisYouWillGetAnException default
        public SqlParameter[] SqlParameters { get; set; } = null;  //the sql parameter //Null default
        #endregion
    }
}
