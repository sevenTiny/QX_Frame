using QX_Frame.App.WebApi;
using QX_Frame.Helper_DG;
using QX_Frame.Helper_DG.Extends;
using System.Web.Http;

namespace QX_Frame.WebApi.Controllers
{
    /*
     * author:qixiao
     * time:2017-2-27 10:32:57
     **/
    public class Test1Controller : WebApiControllerBase
    {
        //access http://localhost:3999/api/Test1  get method
        public IHttpActionResult GetTest()
        {
            //throw new Exception_DG_Internationalization(1001);
            return OK("Get Test");
        }
        //access http://localhost:3999/api/Test1  post method
        public IHttpActionResult PostTest(dynamic queryData)
        {
            return Json(new { IsSuccess = true, Msg = "this is post method",Data=queryData });
        }
        //access http://localhost:3999/api/Test1  put method
        public IHttpActionResult PutTest()
        {
            return Json(new { IsSuccess = true, Msg = "this is put method" });
        }
        //access http://localhost:3999/api/Test1  delete method
        public IHttpActionResult DeleteTest()
        {
            return Json(new { IsSuccess = true, Msg = "this is delete method" });
        }
    }
}
