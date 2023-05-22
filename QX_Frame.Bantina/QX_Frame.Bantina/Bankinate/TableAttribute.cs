/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-8-7 10:19:35
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * Personal WebSit: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:-.
 * Thx , Best Regards ~
 *********************************************************/
using System;
namespace QX_Frame.Bantina.Bankinate
{
    /// <summary>
    /// Self Define Attribute , Support Properties/Class Support Extend Inherit
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// Real TableName : if not declare , use class name.
        /// </summary>
        public string TableName { get; set; }
    }
}
