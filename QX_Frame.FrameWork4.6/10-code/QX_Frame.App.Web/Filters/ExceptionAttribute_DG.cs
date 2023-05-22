using QX_Frame.Helper_DG;
using QX_Frame.Helper_DG.Extends;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace QX_Frame.App.Web.Filters
{
    //author:qixiao
    //create:2017-1-23 10:35:19
    //update:2017-4-6 21:09:33
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]//use multiple
    public class ExceptionAttribute_DG : HandleErrorAttribute
    {
        
    }
}