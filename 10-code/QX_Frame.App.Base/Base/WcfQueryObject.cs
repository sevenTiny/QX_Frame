using System;

namespace QX_Frame.App.Base
{
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
    }
}
