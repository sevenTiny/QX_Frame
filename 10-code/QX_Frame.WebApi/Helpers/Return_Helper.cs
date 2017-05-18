/**
 * author:qixiao
 * create:2017-5-17 14:32:16
 * */
namespace QX_Frame.WebApi.Helpers
{
    public class Return_Helper
    {
        /// <summary>
        /// return success result
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <param name="dataCount"></param>
        /// <param name="httpCode"></param>
        /// <returns></returns>
        public static object Success(string msg,dynamic data=null,int dataCount=0, System.Net.HttpStatusCode httpCode=System.Net.HttpStatusCode.OK)
        {
            return Helper_DG.Return_Helper_DG.Success_Msg_Data_DCount_HttpCode(msg, data, dataCount, httpCode);
        }
        /// <summary>
        /// return faild result
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorLevel"></param>
        /// <param name="httpCode"></param>
        /// <returns></returns>
        public static object Faild(string msg, int errorCode = 0, int errorLevel = 0, System.Net.HttpStatusCode httpCode = System.Net.HttpStatusCode.InternalServerError)
        {
            return Helper_DG.Return_Helper_DG.Error_Msg_Ecode_Elevel_HttpCode(msg, errorCode, errorLevel, httpCode);
        }
    }
}
