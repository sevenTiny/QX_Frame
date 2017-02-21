using QX_Frame.App.Web;
using QX_Frame.Data.Entities.QX_Frame;
using QX_Frame.Data.QueryObject;
using QX_Frame.Data.Service.QX_Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace QX_Frame.WebAPI.Controllers
{
    public class HomeController : WebApiControllerBase
    {
        public IHttpActionResult Get()
        {
            //return Json(new { home = "213", userId = "123456" });

            tb_userAccount userAccount = null;
            using (var fact = Wcf<UserAccountService>())
            {
                var channel = fact.CreateChannel();
                userAccount = channel.QuerySingle(new UserAccountQueryObject()).Cast<tb_userAccount>();
            }
            return Json(userAccount);
        }
    }
}