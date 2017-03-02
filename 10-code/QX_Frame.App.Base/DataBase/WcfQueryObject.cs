using QX_Frame.App.Base.options;
using System;

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
        public QueryType SqlQueryType { get; set; } = QueryType.EntityFrameWork;   //the query type of query from db //QueryType.EntityFrameWork default

        #region sql query
        public string SqlConnectionString { get; set; } = null; // the sql query connstr // String.Null default
        public string SqlStatementSign { get; set; } = null;   //the sql statement sign of sql statement choice // String.Null default
        public ExecuteType ExecuteType { get; set; } = ExecuteType._ChooseOthers_IfYouChooseThisYouWillGetAnException; //the sql execute type // _ChooseOthers_IfYouChooseThisYouWillGetAnException default
        #endregion
    }
}
