using System.Net;

namespace QX_Frame.Helper_DG_Framework
{
    /*2016-11-3 00:19:31 author:qixiao*/
    /*2017-1-19 11:48:47 author:qixiao -- update*/
    public abstract class Return_Helper_DG
    {
        public static object Object_Success_Desc_Data_DCount_HttpCode_EMsg_ECode_ELevel(bool isSuccess, string description,dynamic data,int dataCount, HttpStatusCode httpStatusCode,string errorMessage,int errorCode,int errorLevel=0)
        {
            return new { isSuccess = isSuccess,description= description, httpStatusCode = httpStatusCode, data= data, dataCount= dataCount, errorMessage = errorMessage, errorCode= errorCode, errorLevel= errorLevel };
        }
        public static object Success_Desc_Data_DCount_HttpCode(string description, dynamic data = null, int dataCount = 0, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new { isSuccess = true, description = description, httpStatusCode = httpStatusCode, data = data, dataCount = dataCount};
        }
        public static object Error_EMsg_Ecode_Elevel_HttpCode(string errorMessage,int errorCode,int errorLevel=0, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        {
            return new { isSuccess = false,httpStatusCode = httpStatusCode,errorMessage = errorMessage, errorCode = errorCode, errorLevel = errorLevel };
        }
    }
}
