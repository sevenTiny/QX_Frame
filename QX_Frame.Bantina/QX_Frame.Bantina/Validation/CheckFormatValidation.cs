/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-09-14 15:51:02
 * Update:2017-09-14 15:51:02
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/

using QX_Frame.Bantina.Extends;

namespace QX_Frame.Bantina.Validation
{
    public static class CheckFormatValidation
    {
        /// <summary>
        /// Check match regex string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="regexString"></param>
        /// <param name="international_errorCode"></param>
        public static void CheckFromRegex(this string data, string regexString, int international_errorCode)
        {
            if (!data.IsCheckFromRegex(regexString)) { throw new Exception_DG_Internationalization(international_errorCode); }
        }
        public static void CheckEmail(this string data, int international_errorCode)
        {
            if (!data.IsEmail()) { throw new Exception_DG_Internationalization(international_errorCode); }
        }
        public static void CheckMobilePhone(this string data, int international_errorCode)
        {
            if (!data.IsMobilePhone()) { throw new Exception_DG_Internationalization(international_errorCode); }
        }
        public static void CheckTelPhone(this string data, int international_errorCode)
        {
            if (!data.IsTelPhone()) { throw new Exception_DG_Internationalization(international_errorCode); }
        }
        public static void CheckURL(this string data, int international_errorCode)
        {
            if (!data.IsURL()) { throw new Exception_DG_Internationalization(international_errorCode); }
        }
        public static void CheckIpAddress(this string data, int international_errorCode)
        {
            if (!data.IsIpAddress()) { throw new Exception_DG_Internationalization(international_errorCode); }
        }
        public static void CheckID_Card(this string data, int international_errorCode)
        {
            if (!data.IsID_Card()) { throw new Exception_DG_Internationalization(international_errorCode); }
        }
        //(字母开头，允许5-16字节，允许字母数字下划线)
        public static void CheckAccountName(this string data, int international_errorCode)
        {
            if (!data.IsAccountName()) { throw new Exception_DG_Internationalization(international_errorCode); }
        }
        //(以字母开头，长度在6~18之间，只能包含字母、数字和下划线)
        public static void CheckPassword(this string data, int international_errorCode)
        {
            if (!data.IsPassword()) { throw new Exception_DG_Internationalization(international_errorCode); }
        }
        //(必须包含大小写字母和数字的组合，不能使用特殊字符，长度在8-10之间)
        public static void CheckStrongCipher(this string data, int international_errorCode)
        {
            if (!data.IsStrongCipher()) { throw new Exception_DG_Internationalization(international_errorCode); }
        }
        public static void CheckDataFormat(this string data, int international_errorCode)
        {
            if (!data.IsDataFormat()) { throw new Exception_DG_Internationalization(international_errorCode); }
        }
        public static void CheckChineseCharactor(this string data, int international_errorCode)
        {
            if (!data.IsChineseCharactor()) { throw new Exception_DG_Internationalization(international_errorCode); }
        }
        public static void CheckQQ_Number(this string data, int international_errorCode)
        {
            if (!data.IsQQ_Number()) { throw new Exception_DG_Internationalization(international_errorCode); }
        }
        public static void CheckPostalCode(this string data, int international_errorCode)
        {
            if (!data.IsPostalCode()) { throw new Exception_DG_Internationalization(international_errorCode); }
        }
    }
}
