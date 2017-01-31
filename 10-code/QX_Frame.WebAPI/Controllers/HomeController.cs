using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace QX_Frame.WebAPI.Controllers
{
    public class HomeController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Json(new { home = "213", userId = "123456" });
        }
    }
}