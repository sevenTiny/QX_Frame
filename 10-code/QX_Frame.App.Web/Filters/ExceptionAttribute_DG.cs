using QX_Frame.Helper_DG_Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace QX_Frame.App.Web.Filters
{
    //2017-1-23 10:35:19
    //author:qixiao
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]//use multiple
    public class ExceptionAttribute_DG :ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Log_Helper_DG.Log_Error($"{actionExecutedContext.Exception.Message} -- error : {actionExecutedContext.Exception.StackTrace} ", $"{actionExecutedContext.Exception.GetType().ToString()}");

            HttpStatusCode HttpCode = HttpStatusCode.OK;//the default HttpStatusCode

            if (actionExecutedContext.Exception is NotImplementedException)
            {
                HttpCode=HttpStatusCode.NotImplemented;
            }
            else if (actionExecutedContext.Exception is TimeoutException)
            {
                HttpCode = HttpStatusCode.RequestTimeout;
            }
            else if (actionExecutedContext.Exception is ArgumentException)
            {
                HttpCode = HttpStatusCode.MethodNotAllowed;
            }

            //.....

            else
            {
                HttpCode = HttpStatusCode.InternalServerError;
            }

            object ErrorObject = Return_Helper_DG.Error_EMsg_Ecode_Elevel_HttpCode($"{actionExecutedContext.Exception.Message}",HttpCode.ToInt(),1001, HttpCode);

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(ErrorObject);

            base.OnException(actionExecutedContext);
        }
    }
}