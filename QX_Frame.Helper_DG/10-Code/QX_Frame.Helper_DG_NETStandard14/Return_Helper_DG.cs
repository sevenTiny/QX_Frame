using System.Net;

/**
 * author:qixiao
 * create:2016-11-3 00:19:31
 * update:2017-1-19 11:48:47
 * update:2017-4-6 20:28:11
 * update:2017-5-19 09:55:03
 * */
namespace QX_Frame.Helper_DG
{
    public abstract class Return_Helper_DG
    {
        public static object IsSuccess_Msg_Data_HttpCode(bool isSuccess, string msg,object data, HttpStatusCode httpCode=HttpStatusCode.OK)
        {
            return new { isSuccess = isSuccess,msg= msg, httpCode = httpCode, data= data};
        }
        public static object Success_Msg_Data_DCount_HttpCode(string msg, object data = null, int dataCount = 0, HttpStatusCode httpCode = HttpStatusCode.OK)
        {
            return new { isSuccess = true, msg = msg, httpCode = httpCode, data = data, dataCount = dataCount};
        }
        public static object Error_Msg_Ecode_Elevel_HttpCode(string msg,int errorCode=0,int errorLevel=0, HttpStatusCode httpCode = HttpStatusCode.InternalServerError)
        {
            return new { isSuccess = false,msg = msg, httpCode = httpCode, errorCode = errorCode, errorLevel = errorLevel};
        }
    }
}