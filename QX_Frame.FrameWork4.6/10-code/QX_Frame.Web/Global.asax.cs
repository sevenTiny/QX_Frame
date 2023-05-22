using QX_Frame.Web.Config;
using System.Web.Mvc;
using System.Web.Routing;

namespace QX_Frame.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Start Insert

            new ConfigBootStrap();
            new ClassRegisters();

            //End Start Insert
        }
    }
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new App.Web.Filters.ExceptionAttribute_DG());
        }
    }
}
