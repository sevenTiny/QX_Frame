using QX_Frame.App.WebApi.Filters;
using QX_Frame.WebApiIIS.Config;
using System.Web.Http;
using System.Web.Http.Cors;

namespace QX_Frame.WebApiIIS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //跨域配置 //need reference from nuget
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //if config the global filter input there need not write the attributes
            config.Filters.Add(new ExceptionAttribute_DG());

            new ConfigBootStrap();  //configuration bootstrap
            new ClassRegisters();   //register ioc menbers
        }
    }
}
