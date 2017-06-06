using Newtonsoft.Json.Linq;
using QX_Frame.App.Base.Configs;
using QX_Frame.Data.Configs;
using QX_Frame.Helper_DG;
using QX_Frame.Helper_DG.Configs;
using QX_Frame.Helper_DG.Extends;
using System.Diagnostics;

namespace QX_Frame.FormApp.Config
{
    public class ConfigBootStrap
    {
        /// <summary>
        /// constructor
        /// </summary>
        public ConfigBootStrap()
        {
            JObject jobject_qx_frame_config = File_Helper_DG.Json_GetJObjectFromJsonFile("../../config/qx_frame.config.json");//get json configuration file

            QX_Frame_Helper_DG_Config.ConnectionString_DB_QX_Frame_Default = jobject_qx_frame_config["database"]["connectionStrings"]["QX_Frame_Default"].ToString();
            QX_Frame_Helper_DG_Config.Log_Location_General = jobject_qx_frame_config["log"]["Log_Location_General"].ToString();
            QX_Frame_Helper_DG_Config.Log_Location_Error = jobject_qx_frame_config["log"]["Log_Location_Error"].ToString();
            QX_Frame_Helper_DG_Config.Log_Location_Use = jobject_qx_frame_config["log"]["Log_Location_Use"].ToString();
            QX_Frame_Helper_DG_Config.Cache_IsCache = jobject_qx_frame_config["cache"]["IsCache"].ToInt() == 1;
            QX_Frame_Helper_DG_Config.Cache_CacheExpirationTimeSpan_Minutes = jobject_qx_frame_config["cache"]["CacheExpirationTime_Minutes"].ToInt();
            QX_Frame_Helper_DG_Config.Cache_CacheServer = QX_Frame.Helper_DG.Options.Opt_CacheServer.Redis;
            QX_Frame_Helper_DG_Config.Cache_Redis_Host = jobject_qx_frame_config["cache"]["Cache_Redis_Host"].ToString();
            QX_Frame_Helper_DG_Config.Cache_Redis_Port = jobject_qx_frame_config["cache"]["Cache_Redis_Port"].ToInt();
            QX_Frame_Helper_DG_Config.International_ConfigFileLocation = @"../../config/qx_frame.internationalization.json";
            QX_Frame_Helper_DG_Config.International_Language = "english";

            QX_Frame_Data_Config.ConnectionString_db_qx_frame = jobject_qx_frame_config["database"]["connectionStrings"]["db_qx_frame"].ToString();

            Trace.WriteLine("configuration bootstrap succeed !");
        }

    }
}
