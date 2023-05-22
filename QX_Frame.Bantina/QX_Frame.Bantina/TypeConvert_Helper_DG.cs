using System;
using System.Data;

namespace QX_Frame.Bantina
{
    public abstract class TypeConvert_Helper_DG
    {
        /// <summary>
        /// sql type string 转换为 C# 类型
        /// </summary>
        /// <param name="sqlTypeString"></param>
        /// <returns></returns>
        public static Type SqlDbTypeToCsharpType(string sqlTypeString)
        {
            SqlDbType dbType = SqlTypeStringToSqlType(sqlTypeString);
            Type type = SqlDbTypeToCsharpType(dbType);
            return type;
        }

        /// <summary>
        ///  sql type string 转换为 C# 类型字符串
        /// </summary>
        /// <param name="sqlTypeString"></param>
        /// <returns></returns>
        public static string SqlTypeStringToCsharpTypeString(string sqlTypeString)
        {
            SqlDbType dbType = SqlTypeStringToSqlType(sqlTypeString);
            Type type = SqlDbTypeToCsharpType(dbType);
            return type.Name;
        }
        /// <summary>
        /// sql dbtype to csharp type and create default value
        /// </summary>
        /// <param name="sqlTypeString"></param>
        /// <returns></returns>
        public static object SqlDbTypeToCsharpTypeDefaultValue(string sqlTypeString)
        {
            SqlDbType dbType = SqlTypeStringToSqlType(sqlTypeString);
            return SqlDbTypeToCsharpTypeDefaultValue(dbType);
        }

        // SqlDbType转换为C#数据类型
        private static Type SqlDbTypeToCsharpType(SqlDbType sqlDbType)
        {
            switch (sqlDbType)
            {
                case SqlDbType.BigInt:
                    return typeof(long);
                case SqlDbType.Binary:
                    return typeof(Object);
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

        //get Csharp Type default value from sql DbType
        private static object SqlDbTypeToCsharpTypeDefaultValue(SqlDbType sqlDbType)
        {
            switch (sqlDbType)
            {
                case SqlDbType.BigInt:
                    return default(long);
                case SqlDbType.Binary:
                    return default(Object);
                case SqlDbType.Bit:
                    return default(Boolean);
                case SqlDbType.Char:
                    return "";
                case SqlDbType.DateTime:
                    return default(DateTime);
                case SqlDbType.Decimal:
                    return default(Decimal);
                case SqlDbType.Float:
                    return default(Double);
                case SqlDbType.Image:
                    return default(Object);
                case SqlDbType.Int:
                    return default(int);
                case SqlDbType.Money:
                    return default(Decimal);
                case SqlDbType.NChar:
                    return "";
                case SqlDbType.NText:
                    return "";
                case SqlDbType.NVarChar:
                    return "";
                case SqlDbType.Real:
                    return default(Single);
                case SqlDbType.SmallDateTime:
                    return default(DateTime);
                case SqlDbType.SmallInt:
                    return default(Int16);
                case SqlDbType.SmallMoney:
                    return default(Decimal);
                case SqlDbType.Text:
                    return "";
                case SqlDbType.Timestamp:
                    return default(long);
                case SqlDbType.TinyInt:
                    return default(Byte);
                case SqlDbType.Udt://自定义的数据类型
                    return default(Object);
                case SqlDbType.UniqueIdentifier:
                    return Guid.NewGuid();
                case SqlDbType.VarBinary:
                    return default(Object);
                case SqlDbType.VarChar:
                    return "";
                case SqlDbType.Variant:
                    return default(Object);
                case SqlDbType.Xml:
                    return default(Object);
                default:
                    return null;
            }
        }

        // sql server数据类型（如：varchar）
        // 转换为SqlDbType类型
        private static SqlDbType SqlTypeStringToSqlType(string sqlTypeString)
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
