/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-8-7 09:35:24
 * Update:2017-09-08 11:26:07
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
using System;

namespace QX_Frame.Bantina.Extends
{
    [Obsolete("this class migration to Bankinate.LinQLambdaToSql")]
    internal static class LambdaToSqlStatementClass
    {
        public static string LambdaToSqlStatement(this string lambdaString)
        => lambdaString
                .LambdaToSqlStatement_Arrows()
                .LambdaToSqlStatement_Quotes()
                .LambdaToSqlStatement_AndAlso()
                .LambdaToSqlStatement_OrElse()
                .LambdaToSqlStatement_Contains()
                .LambdaToSqlStatement_StartsWith()
                .LambdaToSqlStatement_EndsWith()
                .LambdaToSqlStatement_Equls()
                .LambdaToSqlStatement_EqulsMark()
                .LambdaToSqlStatement_True()
                ;

        /**
         * append:2017-8-7 16:51:45
         * */
        public static string LambdaToSqlStatementOrderBy(this string lambdaString)
        => lambdaString.LambdaToSqlStatement_ArrowsRemove();
    }
}
