/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:5.0.0
 * Author:qixiao(柒小)
 * Create:2017-09-30 09:38:10
 * Update:2017-09-30 09:38:10
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
using System;
using System.Data;

namespace CSharp_FlowchartToCode_DG.QX_Frame.Helper
{
    internal class SQLServerTypeConvert
    {
        public static string SqlTypeStringToJavaTypeString(string sqlServerTypeString)
        {
            switch (sqlServerTypeString)
            {
                case "bit": return "Boolean";
                case "binary": return "byte[]";
                case "int": return "int";
                case "smallint": return "short";
                case "bigint": return "long";
                case "tinyint": return "byte";
                case "float": return "float";
                case "decimal": return "decimal";
                case "numeric": return "decimal";
                case "smallmoney": return "decimal";
                case "money": return "decimal";
                case "char": return "string";
                case "nchar": return "string";
                case "varchar": return "string";
                case "nvarchar": return "string";
                case "ntext": return "string";
                case "text": return "string";
                case "smalldatetime": return "DateTime";
                case "datetime": return "DateTime";
                case "timestamp": return "long";
                case "image": return "object";
                case "real": return "Single";
                case "sql_variant": return "Object";
                case "uniqueidentifier": return "UUID";
                case "varbinary": return "Object";
                case "xml": return "Object";
                default: return "Object";
            }
        }

        /// <summary>
        /// 将string类型的sql数据转化成C#中的数据，返回值为stirng
        /// </summary>
        /// <param name="sqlTypeString"></param>
        /// <returns></returns>
        public static string SqlTypeStringToNetTypeString(string sqlTypeString)
        {
            SqlDbType dbTpe = SqlTypeString2SqlType(sqlTypeString);
            Type type = SqlType2CsharpType(dbTpe);
            return type.Name;
        }


        // SqlDbType转换为C#数据类型
        private static Type SqlType2CsharpType(SqlDbType sqlType)
        {
            switch (sqlType)
            {
                case SqlDbType.BigInt:
                    return typeof(long);
                case SqlDbType.Binary:
                    return typeof(byte[]);
                case SqlDbType.Bit:
                    return typeof(Boolean);
                case SqlDbType.Char:
                    return typeof(String);
                case SqlDbType.DateTime:
                    return typeof(DateTime);
                case SqlDbType.Decimal:
                    return typeof(Decimal);
                case SqlDbType.Float:
                    return typeof(Double);
                case SqlDbType.Image:
                    return typeof(Object);
                case SqlDbType.Int:
                    return typeof(int);
                case SqlDbType.Money:
                    return typeof(Decimal);
                case SqlDbType.NChar:
                    return typeof(String);
                case SqlDbType.NText:
                    return typeof(String);
                case SqlDbType.NVarChar:
                    return typeof(String);
                case SqlDbType.Real:
                    return typeof(Single);
                case SqlDbType.SmallDateTime:
                    return typeof(DateTime);
                case SqlDbType.SmallInt:
                    return typeof(Int16);
                case SqlDbType.SmallMoney:
                    return typeof(Decimal);
                case SqlDbType.Text:
                    return typeof(String);
                case SqlDbType.Timestamp:
                    return typeof(long);
                case SqlDbType.TinyInt:
                    return typeof(Byte);
                case SqlDbType.Udt://自定义的数据类型
                    return typeof(Object);
                case SqlDbType.UniqueIdentifier:
                    return typeof(Guid);
                case SqlDbType.VarBinary:
                    return typeof(Object);
                case SqlDbType.VarChar:
                    return typeof(String);
                case SqlDbType.Variant:
                    return typeof(Object);
                case SqlDbType.Xml:
                    return typeof(Object);
                default:
                    return null;
            }
        }

        // sql server数据类型（如：varchar）
        // 转换为SqlDbType类型
        private static SqlDbType SqlTypeString2SqlType(string sqlTypeString)
        {
            SqlDbType dbType = SqlDbType.Variant;//默认为Object

            switch (sqlTypeString)
            {
                case "int":
                    dbType = SqlDbType.Int;
                    break;
                case "varchar":
                    dbType = SqlDbType.VarChar;
                    break;
                case "bit":
                    dbType = SqlDbType.Bit;
                    break;
                case "datetime":
                    dbType = SqlDbType.DateTime;
                    break;
                case "decimal":
                    dbType = SqlDbType.Decimal;
                    break;
                case "float":
                    dbType = SqlDbType.Float;
                    break;
                case "image":
                    dbType = SqlDbType.Image;
                    break;
                case "money":
                    dbType = SqlDbType.Money;
                    break;
                case "ntext":
                    dbType = SqlDbType.NText;
                    break;
                case "nvarchar":
                    dbType = SqlDbType.NVarChar;
                    break;
                case "smalldatetime":
                    dbType = SqlDbType.SmallDateTime;
                    break;
                case "smallint":
                    dbType = SqlDbType.SmallInt;
                    break;
                case "text":
                    dbType = SqlDbType.Text;
                    break;
                case "bigint":
                    dbType = SqlDbType.BigInt;
                    break;
                case "binary":
                    dbType = SqlDbType.Binary;
                    break;
                case "char":
                    dbType = SqlDbType.Char;
                    break;
                case "nchar":
                    dbType = SqlDbType.NChar;
                    break;
                case "numeric":
                    dbType = SqlDbType.Decimal;
                    break;
                case "real":
                    dbType = SqlDbType.Real;
                    break;
                case "smallmoney":
                    dbType = SqlDbType.SmallMoney;
                    break;
                case "sql_variant":
                    dbType = SqlDbType.Variant;
                    break;
                case "timestamp":
                    dbType = SqlDbType.Timestamp;
                    break;
                case "tinyint":
                    dbType = SqlDbType.TinyInt;
                    break;
                case "uniqueidentifier":
                    dbType = SqlDbType.UniqueIdentifier;
                    break;
                case "varbinary":
                    dbType = SqlDbType.VarBinary;
                    break;
                case "xml":
                    dbType = SqlDbType.Xml;
                    break;
            }
            return dbType;
        }
    }
}
