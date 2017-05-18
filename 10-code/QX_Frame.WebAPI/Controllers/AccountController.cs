using QX_Frame.App.WebApi;
using QX_Frame.Data.Entities.QX_Frame;
using QX_Frame.Data.QueryObject;
using QX_Frame.Data.Service.QX_Frame;
using QX_Frame.WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;
//using the static class  support in .NET FrameWork 4.6.*

namespace QX_Frame.WebApi.Controllers
{
    public class AccountController : WebApiControllerBase
    {
        public IHttpActionResult GetString()
        {
            using (var fact = Wcf<UserAccountService>())
            {
                var channel = fact.CreateChannel();
                List<tb_UserAccount> list = channel.QueryAll(new tb_UserAccountQueryObject()).Cast<List<tb_UserAccount>>();
                return Json(list);
            } 
        }
        public IHttpActionResult PostString()
        {
            using (var fact=Wcf<UserAccountService>())
            {
                var channel = fact.CreateChannel();
                int count;
                List<tb_UserAccount> userAccountList = channel.QueryAllPaging<tb_UserAccount, string>(new tb_UserAccountQueryObject() { PageIndex = 1, PageSize = 2 }, t => t.loginId).Cast<List<tb_UserAccount>>(out count);
                return Json(Helper_DG.Return_Helper_DG.Success_Msg_Data_DCount_HttpCode("success return !", userAccountList, count));
            }
        }
        public IHttpActionResult PutString()
        {
            using (var fact = Wcf<UserAccountService>())
            {
                var channel = fact.CreateChannel();
                List<tb_UserAccount> userAccountList = channel.QuerySql(new tb_UserAccountQueryObject() { SqlStatementTextOrSpName = "select * from tb_UserAccount", SqlExecuteType = App.Base.Options.ExecuteType.Execute_List_T }).Cast<List<tb_UserAccount>>();
                return Json(Helper_DG.Return_Helper_DG.Success_Msg_Data_DCount_HttpCode("user list", userAccountList, userAccountList.Count));
            }
        }
        public IHttpActionResult DeleteString()
        {
            using (var fact = Wcf<UserAccountService>())
            {
                var channel = fact.CreateChannel();
                int result = channel.QuerySql(new tb_UserAccountQueryObject()
                {
                    SqlStatementTextOrSpName = "insert into tb_UserAccount values(@uid,@loginId,@pwd)",
                    SqlParameters = new SqlParameter[] {
                        new SqlParameter("@uid", Guid.NewGuid()),
                        new SqlParameter("@loginId","4444"),
                        new SqlParameter("@pwd","5555")
                    },
                    SqlExecuteType = App.Base.Options.ExecuteType.ExecuteNonQuery
                }).Cast<dynamic>();
                return Json(Helper_DG.Return_Helper_DG.Success_Msg_Data_DCount_HttpCode("user list", result));
            }
        }
    }
    
}
