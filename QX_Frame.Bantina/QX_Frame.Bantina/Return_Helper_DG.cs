using System.Net;

namespace QX_Frame.Bantina
{
    /**
     * 2016-11-3 00:19:31   author:qixiao -- create
     * 2017-1-19 11:48:47   author:qixiao -- update
     * 2017-4-6 20:28:11    author:qixiao -- update
     * */
    public abstract class Return_Helper_DG
    {
        public static object IsSuccess_Msg_Data_HttpCode(bool isSuccess, string msg,dynamic data, HttpStatusCode httpCode=HttpStatusCode.OK)
        {
            return new { isSuccess = isSuccess,msg= msg, httpCode = httpCode, data= data};
        }
        public static object Success_Msg_Data_DCount_HttpCode(string msg, dynamic data = null, int dataCount = 0, HttpStatusCode httpCode = HttpStatusCode.OK)
        {
            return new { isSuccess = true, msg = msg, httpCode = httpCode, data = data, dataCount = dataCount};
        }
        public static object Error_Msg_Ecode_Elevel_HttpCode(string msg,int errorCode=0,int errorLevel=0, HttpStatusCode httpCode = HttpStatusCode.InternalServerError)
        {
            return new { isSuccess = false,msg = msg, httpCode = httpCode, errorCode = errorCode, errorLevel = errorLevel};
        }
    }
}