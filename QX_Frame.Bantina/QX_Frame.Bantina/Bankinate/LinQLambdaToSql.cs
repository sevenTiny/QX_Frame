/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-09-07 16:57:42
 * Update:2017-09-07 16:57:42
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace QX_Frame.Bantina.Bankinate
{
    internal class LinQLambdaToSql
    {
        #region get where and order by statement method

        /// <summary>
        /// Generate Sql condition statement
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public static string ConvertWhere<T>(Expression<Func<T, bool>> where) where T : class
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" ");
            builder.Append(where.Parameters.FirstOrDefault().Name);
            builder.Append(" WHERE ");
            if (where.Body is BinaryExpression)
            {
                BinaryExpression be = ((BinaryExpression)where.Body);
                builder.Append(BinarExpressionProvider(be.Left, be.Right, be.NodeType));
            }
            else if (where.Body is MethodCallExpression)
            {
                MethodCallExpression be = ((MethodCallExpression)where.Body);
                builder.Append(ExpressionRouter(where.Body));
            }
            else
            {
                builder.Append("1=1");
            }
            return builder.ToString();
        }

        /// <summary>
        /// Generate Order By SqlStatement
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public static string ConvertOrderBy<T>(Expression<Func<T, object>> orderby) where T : class
        {
            if (orderby.Body is UnaryExpression)
            {
                UnaryExpression ue = ((UnaryExpression)orderby.Body);
                return ExpressionRouter(ue.Operand);
            }
            else
            {
                MemberExpression order = ((MemberExpression)orderby.Body);
                return order.Member.Name;
            }
        }

        #endregion

        #region private prepare method

        private static string BinarExpressionProvider(Expression left, Expression right, ExpressionType type)
        {
            StringBuilder builder = new StringBuilder();
            // deal left first
            builder.Append(ExpressionRouter(left));
            // add link signal
            builder.Append(ExpressionTypeCast(type));
            // deal right then
            builder.Append(ExpressionRouter(right));

            return builder.ToString();
        }

        private static string ExpressionRouter(Expression exp)
        {
            if (exp is BinaryExpression)
            {
                BinaryExpression be = ((BinaryExpression)exp);
                return BinarExpressionProvider(be.Left, be.Right, be.NodeType);
            }
            else if (exp is MemberExpression)
            {
                MemberExpression me = ((MemberExpression)exp);
                if (!exp.ToString().StartsWith("value"))
                {
                    return me.ToString();
                }
                else
                {
                    var result = Expression.Lambda(exp).Compile().DynamicInvoke();
                    if (result == null)
                    {
                        return "NULL";
                    }
                    else if (result is ValueType)
                    {
                        if (result is Guid)
                        {
                            return string.Concat('\'', result, '\'');
                        }
                        return result.ToString();
                    }
                    else if (result is string || result is DateTime || result is char)
                    {
                        return string.Concat('\'', result, '\'');
                    }
                }
            }
            else if (exp is NewArrayExpression)
            {
                NewArrayExpression ae = ((NewArrayExpression)exp);
                StringBuilder tmpstr = new StringBuilder();
                foreach (Expression ex in ae.Expressions)
                {
                    tmpstr.Append(ExpressionRouter(ex));
                    tmpstr.Append(",");
                }
                return tmpstr.ToString(0, tmpstr.Length - 1);
            }
            else if (exp is MethodCallExpression)
            {
                MethodCallExpression mce = (MethodCallExpression)exp;
                string value = ExpressionRouter(mce.Arguments[0]);
                if (mce.Method.Name.Equals("Equals"))
                {
                    return string.Concat(mce.Object.ToString(), " = ", value);
                }
                else if (mce.Method.Name.Equals("Contains"))
                {
                    return string.Concat(mce.Object.ToString(), " LIKE '%", value.Replace("'", ""), "%'");
                }
                else if (mce.Method.Name.Equals("StartsWith"))
                {
                    return string.Concat(mce.Object.ToString(), " LIKE '", value.Replace("'", ""), "%'");
                }
                else if (mce.Method.Name.Equals("EndsWith"))
                {
                    return string.Concat(mce.Object.ToString(), " LIKE '%", value.Replace("'", ""), "'");
                }
                return " ";
            }
            else if (exp is ConstantExpression)
            {
                ConstantExpression ce = ((ConstantExpression)exp);
                if (ce.Value == null)
                {
                    return "NULL";
                }
                else if (ce.Value is ValueType)
                {
                    if (ce.Value is bool)
                    {
                        return " 1=1 ";
                    }
                    return ce.Value.ToString();
                }
                else if (ce.Value is string || ce.Value is DateTime || ce.Value is char)
                {
                    return string.Concat("'", ce.Value.ToString(), "'");
                }
                return " ";
            }
            else if (exp is UnaryExpression)
            {
                UnaryExpression ue = ((UnaryExpression)exp);

                return ExpressionRouter(ue.Operand);
            }
            return null;
        }

        private static string ExpressionTypeCast(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return " AND ";

                case ExpressionType.Equal:
                    return "=";

                case ExpressionType.GreaterThan:
                    return ">";

                case ExpressionType.GreaterThanOrEqual:
                    return ">=";

                case ExpressionType.LessThan:
                    return "<";

                case ExpressionType.LessThanOrEqual:
                    return "<=";

                case ExpressionType.NotEqual:
                    return "<>";

                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return " Or ";

                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return "+";

                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return "-";

                case ExpressionType.Divide:
                    return "/";

                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return "*";

                default:
                    return null;
            }
        }

        #endregion
    }
}
