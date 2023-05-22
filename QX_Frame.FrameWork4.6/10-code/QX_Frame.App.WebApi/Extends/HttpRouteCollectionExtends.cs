using System;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Net.Http;

namespace QX_Frame.App.WebApi.Extends
{
    public static class HttpRouteCollectionExtends
    {
        public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, string[] namespaces)
        {
            return routes.MapHttpRoute(name, routeTemplate, defaults, null, null, namespaces);
        }
        public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, object constraints, HttpMessageHandler handler, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            var routeValue = new HttpRouteValueDictionary(new { Namespace = namespaces });//set route values
            var route = routes.CreateRoute(routeTemplate, new HttpRouteValueDictionary(defaults), new HttpRouteValueDictionary(constraints), routeValue, handler);
            routes.Add(name, route);
            return route;
        }
    }
}
