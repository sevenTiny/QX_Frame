/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-08-22 13:55:29
 * Update:2017-9-7 14:57:23
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * Personal WebSit: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:-.
 * Thx , Best Regards ~
 *********************************************************/
using System;

namespace QX_Frame.Bantina.Bankinate
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class ForeignTableAttribute : Attribute
    {
        /// <summary>
        /// Real TableName : if not declare , use class name.
        /// </summary>
        public string ForeignKeyFieldName { get; set; }
    }
}
