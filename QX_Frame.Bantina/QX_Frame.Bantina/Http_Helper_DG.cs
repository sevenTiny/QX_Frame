using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

/**
 * author:qixiao
 * create:2017-6-28 11:45:41
 * */
namespace QX_Frame.Bantina
{

    /// <summary>
    /// Http Client Helper
    /// </summary>
    public class Http_Helper_DG
    {
        public static string CommonHttpRequest(string uri, string data,  string type,string contentType= "application/x-www-form-urlencoded")
        {
            //构造http请求的对象
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(uri);
            //转成网络流
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(data);
            //设置
            myRequest.Method = type;
            myRequest.ContentLength = buf.Length;
            myRequest.ContentType = contentType;
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;


            // 发送请求
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();
            // 获得接口返回值
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string ReturnXml = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return ReturnXml;
        }

        #region Delete方式
        public static string Delete(string uri,string data, string contentType = "application/x-www-form-urlencoded")
        {
            return CommonHttpRequest(uri, data, "DELETE",contentType);
        }

        public static string Delete(string uri)
        {
            //Web访问对象64
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(uri);
            myRequest.Method = "DELETE";
            // 获得接口返回值68
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            //string ReturnXml = HttpUtility.UrlDecode(reader.ReadToEnd());
            string ReturnXml = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return ReturnXml;
        }
        #endregion

        #region Put方式
        public string Put(string uri, string data,  string contentType = "application/x-www-form-urlencoded")
        {
            return CommonHttpRequest(uri, data, "PUT", contentType);
        }
        #endregion

        #region POST方式实现

        public string Post(string uri,string data, string contentType = "application/x-www-form-urlencoded")
        {
            return CommonHttpRequest(uri, data,  "POST",contentType);
        }

        #endregion

        #region GET方式实现
        public static string Get(string uri)
        {
            //构造一个Web请求的对象
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(uri);
            // 获得接口返回值68
            //获取web请求的响应的内容
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();

            //通过响应流构造一个StreamReader
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            //string ReturnXml = HttpUtility.UrlDecode(reader.ReadToEnd());
            string ReturnXml = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return ReturnXml;
        }
        #endregion
    }
}
