using QX_Frame.App.Web;
using QX_Frame.Data.DTO;
using QX_Frame.Helper_DG_Framework;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
//using the static class  support in .NET FrameWork 4.6.*
using static QX_Frame.Helper_DG_Framework.ProcessFlow_Helper_DG;

namespace QX_Frame.WebAPI.Controllers
{
    public class AccountController : WebApiControllerBase
    {
        public async Task<string> getString()
        {
            string name = await ProcessFlow_Helper_DG.channel_Async_Task<string>(() =>
            {
                return "this is task string";
            });

            return name;
        }
    }
}
