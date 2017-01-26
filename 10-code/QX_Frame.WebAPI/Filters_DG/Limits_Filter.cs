﻿using QX_Frame.Helper_DG_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace QX_Frame.WebAPI.Filters_DG
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]//多次调用
    public class Limits_Filter: ActionFilterAttribute
    {
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]//多次调用
    public class Test : ActionFilterAttribute
    {
        public string Roles { get; set; }
        public override object TypeId { get; }
        public string Users { get; set; }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            var key = actionContext.ActionArguments.Keys.FirstOrDefault();
            var result = actionContext.ActionArguments[key].ToString();
            var obj = Convert_Helper_DG.Json_To_T<object>(result);
            var ll = obj;
            HttpContext.Current.Session[""] = 1;
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, Return_Helper_DG.Error_EMsg_Ecode_Elevel_HttpCode( "the user not login", 1));
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}