using System;
using System.Web;

/**
 * author:qixiao
 * create:2017-6-17 17:25:51
 * */
namespace QX_Frame.Bantina
{
    public abstract class Cookie_Helper_DG
    {
        /// <summary>
        /// Add Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        /// <param name="expireTime"></param>
        public static void Add(string cookieName, string cookieValue,DateTime expireTime)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(cookieName)
            {
                Value = cookieValue,
                Expires = expireTime
            });
        }

        /// <summary>  
        /// let Cookie expire
        /// </summary>  
        /// <param name="cookieName">cookieName</param>  
        public static void ExpireCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            cookie = HttpContext.Current.Request.Cookies[cookieName];
        }

        /// <summary>  
        /// Get Cookie By CookieName
        /// </summary>  
        /// <param name="cookieName">cookieName</param>  
        /// <returns></returns>
        public static string GetCookie(string cookieName)
        {
            return HttpContext.Current.Request.Cookies[cookieName]?.Value;
        }
    }
}
