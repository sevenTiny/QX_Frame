/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-08-22 13:55:29
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * Personal WebSit: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:-.
 * Thx , Best Regards ~
 *********************************************************/
using System;

namespace QX_Frame.Helper_DG.Bantina
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class ForeignKeyAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class ForeignTableAttribute : Attribute { }
}
