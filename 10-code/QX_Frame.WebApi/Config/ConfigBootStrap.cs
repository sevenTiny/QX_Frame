using Newtonsoft.Json.Linq;
using QX_Frame.Helper_DG;
using QX_Frame.Helper_DG.Configs;
using QX_Frame.Helper_DG.Extends;
using QX_Frame.Data.Configs;
using System.Diagnostics;

/**
 * author:qixiao 
 * create：2017-5-15 22:20:36
 **/
namespace QX_Frame.WebApi.Config
{
    public class ConfigBootStrap
    {
        /// <summary>
        /// constructor
        /// </summary>
        public ConfigBootStrap()
        {
            JObject jobject= File_Helper_DG.Json_GetJObjectFromJsonFile("../../config/qx_frame.config.json");//get json configuration file

            QX_Frame_Helper_DG_Config.ConnectionString_DB_QX_Frame_Default = jobject["database"]["connectionStrings"]["QX_Frame_Default"].ToString();
            QX_Frame_Helper_DG_Config.Log_Location_General= jobject["log"]["Log_Location_General"].ToString();
            QX_Frame_Helper_DG_Config.Log_Location_Error= jobject["log"]["Log_Location_Error"].ToString();
            QX_Frame_Helper_DG_Config.Log_Location_Use= jobject["log"]["Log_Location_Use"].ToString();
            QX_Frame_Helper_DG_Config.Cache_IsCache= jobject["cache"]["IsCache"].ToInt()==1;
            QX_Frame_Helper_DG_Config.Cache_CacheExpirationTime_Minutes = jobject["cache"]["CacheExpirationTime_Minutes"].ToInt();

            QX_Frame_Data_Config.ConnectionString_db_qx_frame= jobject["database"]["connectionStrings"]["db_qx_frame"].ToString();

            Trace.WriteLine("configuration bootstrap succeed !");
        }

    }
}
