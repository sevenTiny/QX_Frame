using System;

/**
 * author:qixiao
 * create:2017-8-7 09:35:24
 * */
namespace QX_Frame.Helper_DG.Extends
{
    public static class LambdaToSqlStatementClass
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
