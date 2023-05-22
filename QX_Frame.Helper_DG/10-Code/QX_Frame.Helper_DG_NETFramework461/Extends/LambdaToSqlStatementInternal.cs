using System;

/**
 * author:qixiao
 * create:2017-8-7 09:34:31
 * */
namespace QX_Frame.Helper_DG.Extends
{
    /// <summary>
    /// Internal class
    /// </summary>
    internal static class LambdaToSqlStatementInternal
    {
        public static string LambdaToSqlStatement_Contains(this string lambdaString)
        {
            while (lambdaString.Contains(".Contains"))
            {
                int indexOf_Contains = lambdaString.IndexOf(".Contains('");
                int indexOf_ContainsEnd = indexOf_Contains + lambdaString.Substring(indexOf_Contains).IndexOf("\')") + 2;

                string str_front = lambdaString.Substring(0, indexOf_Contains);
                string value = lambdaString.Substring(indexOf_Contains, indexOf_ContainsEnd - indexOf_Contains);
                string str_back = lambdaString.Substring(indexOf_ContainsEnd);

                value = value.Replace(".Contains(\'", " LIKE (\'%").Replace("\')", "%\')");

                lambdaString = String.Concat(str_front, value, str_back);
            }
            return lambdaString;
        }
        public static string LambdaToSqlStatement_StartsWith(this string lambdaString)
        {
            while (lambdaString.Contains(".StartsWith"))
            {
                int indexOf_StartsWith = lambdaString.IndexOf(".StartsWith('");
                int indexOf_StartsWithEnd = indexOf_StartsWith + lambdaString.Substring(indexOf_StartsWith).IndexOf("\')") + 2;

                string str_front = lambdaString.Substring(0, indexOf_StartsWith);
                string value = lambdaString.Substring(indexOf_StartsWith, indexOf_StartsWithEnd - indexOf_StartsWith);
                string str_back = lambdaString.Substring(indexOf_StartsWithEnd);

                value = value.Replace(".StartsWith(\'", " LIKE (\'").Replace("\')", "%\')");

                lambdaString = String.Concat(str_front, value, str_back);
            }
            return lambdaString;
        }
        public static string LambdaToSqlStatement_EndsWith(this string lambdaString)
        {
            return lambdaString.Replace(".EndsWith(\'", " LIKE (\'%");
        }
        public static string LambdaToSqlStatement_Quotes(this string lambdaString)
        {
            return lambdaString.Replace('\"', '\'');
        }
        public static string LambdaToSqlStatement_Arrows(this string lambdaString)
        {
            return lambdaString.Replace("=>", " where ");
        }
        public static string LambdaToSqlStatement_ArrowsRemove(this string lambdaString)
        {
           return lambdaString.Substring(lambdaString.IndexOf("=>") + 2);
        }

        public static string LambdaToSqlStatement_AndAlso(this string lambdaString)
        {
            return lambdaString.Replace("AndAlso", "AND");
        }
        public static string LambdaToSqlStatement_OrElse(this string lambdaString)
        {
            return lambdaString.Replace("OrElse", "OR");
        }
        public static string LambdaToSqlStatement_Equls(this string lambdaString)
        {
            return lambdaString.Replace(".Equals", "=");
        }
        public static string LambdaToSqlStatement_EqulsMark(this string lambdaString)
        {
            return lambdaString.Replace("==", "=");
        }
        public static string LambdaToSqlStatement_True(this string lambdaString)
        {
            return lambdaString.Replace("True", "1=1");
        }

    }
}
