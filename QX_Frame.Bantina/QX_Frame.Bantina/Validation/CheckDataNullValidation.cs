/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-09-14 16:33:27
 * Update:2017-09-14 16:33:27
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
    public static class CheckDataNullValidation
    {
        public static bool IsNull(this object data)
        {
            return data == null;
        }
        public static object CheckNullGet(object data, int international_errorCode)
        {
            if (data == null)
                throw new Exception_DG_Internationalization(international_errorCode);
            return data;
        }
        public static void CheckNull(this object data, int international_errorCode)
        {
            if (data == null)
                throw new Exception_DG_Internationalization(international_errorCode);
        }

    }
}
