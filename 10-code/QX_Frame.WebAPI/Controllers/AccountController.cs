using QX_Frame.App.Web;
using QX_Frame.Data.Entities.QX_Frame;
using QX_Frame.Data.QueryObject;
using QX_Frame.Data.Service.QX_Frame;
using System.Collections.Generic;
using System.Web.Http;
//using the static class  support in .NET FrameWork 4.6.*

namespace QX_Frame.WebAPI.Controllers
{
    public class AccountController : WebApiControllerBase
    {
        public IHttpActionResult GetString()
        {
            using (var fact=Wcf<UserAccountService>())
            {
                var channel = fact.CreateChannel();
               List<tb_userAccount> list = channel.QueryAll(new UserAccountQueryObject()).Cast<List<tb_userAccount>>();
                return Json(list);
            }
        }
        public IHttpActionResult PostString()
        {
            using (var fact=Wcf<UserAccountService>())
            {
                var channel = fact.CreateChannel();
                int count;
                List<tb_userAccount> userAccountList = channel.QueryAllPaging<tb_userAccount, string>(new UserAccountQueryObject() { PageIndex = 1, PageSize = 2 }, t => t.loginId).Cast<List<tb_userAccount>>(out count);
                return Json(Helper_DG_Framework.Return_Helper_DG.Success_Desc_Data_DCount_HttpCode("success return !", userAccountList, count));
            }
        }
    }
    
}
