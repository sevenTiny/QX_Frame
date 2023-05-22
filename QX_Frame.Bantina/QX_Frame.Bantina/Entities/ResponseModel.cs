/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-09-18 15:11:56
 * Update:2017-09-18 15:11:56
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/

namespace QX_Frame.Bantina.Entities
{
    public class ResponseModel
    {
        public bool isSuccess { get; set; }
        public string msg { get; set; }
        public int httpCode { get; set; }
        public object data { get; set; }
    }
}
