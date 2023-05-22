using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QX_Frame.Bantina
{
    /**
     * author:qixiao
     * create:2017-4-10 18:05:59
     * */
    public static class Ip_Helper_DG
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage ="System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        public static  string GetIpAddressFromRequest(this HttpRequestMessage request)
        {
            // Web-hosting. Needs reference to System.Web.dll
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            // Self-hosting. Needs reference to System.ServiceModel.dll. 
            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            // Self-hosting using Owin. Needs reference to Microsoft.Owin.dll. 
            if (request.Properties.ContainsKey(OwinContext))
            {
                dynamic owinContext = request.Properties[OwinContext];
                if (owinContext != null)
                {
                    return owinContext.Request.RemoteIpAddress;
                }
            }
            return null;
        }

        /// <summary>
        /// Get Server Host Name
        /// </summary>
        /// <returns></returns>
        public static string GetServerHostName()
        {
            return Dns.GetHostName();
        }

        /// <summary>
        /// Get Server Ip Address
        /// </summary>
        /// <returns></returns>
        public static string GetServerIpAddress()
        {
            string hostName = Dns.GetHostName();

            IPHostEntry localhost = Dns.GetHostByName(hostName);

            //IPHostEntry localhost = Dns.GetHostEntry(hostName);   //获取IPv6地址

            IPAddress localaddr = localhost.AddressList[0];

            return localaddr.ToString();
        }
    }
}
