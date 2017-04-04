using QX_Frame.App.Base;
using QX_Frame.Data.Entities.QX_Frame;
using System;
using System.Linq.Expressions;

namespace QX_Frame.Data.QueryObject
{
    public class UserAccountQueryObject : WcfQueryObject<db_qx_frame, tb_userAccount>
    {
        //query condition // true default
        public override Expression<Func<tb_userAccount, bool>> QueryCondition { get => base.QueryCondition; set => base.QueryCondition = value; }
    }
    
}
