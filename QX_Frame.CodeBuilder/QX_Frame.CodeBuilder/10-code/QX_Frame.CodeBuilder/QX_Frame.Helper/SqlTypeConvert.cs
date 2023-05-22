/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:5.0.0
 * Author:qixiao(柒小)
 * Create:2017-09-30 11:27:47
 * Update:2017-09-30 11:27:47
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
using CSharp_FlowchartToCode_DG.Options;
using QX_Frame.Bantina.Options;

namespace CSharp_FlowchartToCode_DG.QX_Frame.Helper
{
    public  class SqlTypeConvert
    {
        /// <summary>
        /// SqlTypeToLanguageType
        /// </summary>
        /// <param name="dataBaseType"></param>
        /// <param name="language"></param>
        /// <param name="typeString"></param>
        /// <returns></returns>
        public static string SqlTypeToLanguageType(Opt_DataBaseType dataBaseType, Opt_Language language,string typeString)
        {
            switch (dataBaseType)
            {
                case Opt_DataBaseType.SqlServer:
                    switch (language)
                    {
                        case Opt_Language.Java:return SQLServerTypeConvert.SqlTypeStringToJavaTypeString(typeString);
                        case Opt_Language.Net:return SQLServerTypeConvert.SqlTypeStringToNetTypeString(typeString);
                        case Opt_Language.Python: return string.Empty;
                        case Opt_Language.JavaScript: return string.Empty;
                        case Opt_Language.Ruby: return string.Empty;
                        case Opt_Language.Go: return string.Empty;
                        case Opt_Language.VB: return string.Empty;
                        default: return string.Empty;
                    }
                case Opt_DataBaseType.MySql:
                    switch (language)
                    {
                        case Opt_Language.Java: return MySqlTypeConvert.SqlTypeStringToJavaTypeString(typeString);
                        case Opt_Language.Net: return MySqlTypeConvert.SqlTypeStringToNetTypeString(typeString);
                        case Opt_Language.Python: return string.Empty;
                        case Opt_Language.JavaScript: return string.Empty;
                        case Opt_Language.Ruby: return string.Empty;
                        case Opt_Language.Go: return string.Empty;
                        case Opt_Language.VB:return string.Empty;
                        default: return string.Empty;
                    }
                case Opt_DataBaseType.Oracle: return string.Empty;
                default:return string.Empty;
            }
        }
    }
}
