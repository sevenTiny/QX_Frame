using QX_Frame.Helper_DG;
using QX_Frame.Helper_DG.Extends;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace QX_Frame.App.WebApi.Filters
{
    //author:qixiao
    //create:2017-1-23 10:35:19
    //update:2017-4-6 21:09:33
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]//use multiple
    public class ExceptionAttribute_DG : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Log_Helper_DG.Log_Error($"{actionExecutedContext.Exception.Message} -- error : {actionExecutedContext.Exception.StackTrace} ", $"{actionExecutedContext.Exception.GetType().ToString()}");

            string Message = actionExecutedContext.Exception.Message;
            HttpStatusCode HttpCode = HttpStatusCode.InternalServerError;   //the default HttpStatusCode
            int ErrorCode = 0;
            int ErrorLevel = 0;


            if (actionExecutedContext.Exception is Exception_DG)
            {
                Exception_DG exception = actionExecutedContext.Exception as Exception_DG;   //实例化一个T类型对象
                ErrorCode = exception.ErrorCode;
                ErrorLevel = exception.ErrorLevel;

                if (!string.IsNullOrEmpty(exception.Arguments))
                {
                    Message = Message + " Arguments:" + exception.Arguments;
                }
            }
            else if (actionExecutedContext.Exception is Exception_DG_Internationalization)
            {
                Exception_DG_Internationalization exception = actionExecutedContext.Exception as Exception_DG_Internationalization;   //实例化一个T类型对象
                ErrorCode = exception.ErrorCode;
                ErrorLevel = exception.ErrorLevel;
                Message = exception.Message_DG;

                if (!string.IsNullOrEmpty(exception.Arguments))
                {
                    Message = Message + " Arguments:" + exception.Arguments;
                }
            }
            else if (actionExecutedContext.Exception is NotImplementedException)
            {
                HttpCode = HttpStatusCode.NotImplemented;
            }
            else if (actionExecutedContext.Exception is TimeoutException)
            {
                HttpCode = HttpStatusCode.RequestTimeout;
            }
            else if (actionExecutedContext.Exception is ArgumentException)
            {
                HttpCode = HttpStatusCode.MethodNotAllowed;
            }
            else if (actionExecutedContext.Exception is System.IO.FileNotFoundException)
            {
                HttpCode = HttpStatusCode.NotFound;
            }

            //.....

            else
            {
                HttpCode = HttpStatusCode.InternalServerError;
            }

            object ErrorObject = Return_Helper_DG.Error_Msg_Ecode_Elevel_HttpCode($"{Message}", ErrorCode, ErrorLevel, HttpCode);

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(ErrorObject);

            base.OnException(actionExecutedContext);
        }
    }
}