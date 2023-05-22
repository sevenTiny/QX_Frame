using Newtonsoft.Json.Linq;
using QX_Frame.Helper_DG;
using QX_Frame.Helper_DG.Configs;
using QX_Frame.Helper_DG.Extends;
using QX_Frame.App.Base.Configs;
using QX_Frame.Data.Configs;
using System.Diagnostics;

/**
 * author:qixiao 
 * create：2017-5-15 22:20:36
 **/
namespace QX_Frame.WebApiIIS.Config
{
    public class ConfigBootStrap
    {
        /// <summary>
        /// constructor
        /// </summary>
        public ConfigBootStrap()
        {
            JObject jobject_qx_frame_config= File_Helper_DG.Json_GetJObjectFromJsonFile(QX_Frame.Helper_DG.IO_Helper_DG.RootPath_MVC+ "Config/qx_frame.config.json");//get json configuration file

            QX_Frame_Helper_DG_Config.ConnectionString_DB_QX_Frame_Default = jobject_qx_frame_config["database"]["connectionStrings"]["QX_Frame_Default"].ToString();
            QX_Frame_Helper_DG_Config.Log_Location_General= jobject_qx_frame_config["log"]["Log_Location_General"].ToString();
            QX_Frame_Helper_DG_Config.Log_Location_Error= jobject_qx_frame_config["log"]["Log_Location_Error"].ToString();
            QX_Frame_Helper_DG_Config.Log_Location_Use= jobject_qx_frame_config["log"]["Log_Location_Use"].ToString();
            QX_Frame_Helper_DG_Config.Cache_IsCache = jobject_qx_frame_config["cache"]["IsCache"].ToInt() == 1;
            QX_Frame_Helper_DG_Config.Cache_CacheExpirationTimeSpan_Minutes = jobject_qx_frame_config["cache"]["CacheExpirationTime_Minutes"].ToInt();
            QX_Frame_Helper_DG_Config.Cache_CacheServer = QX_Frame.Helper_DG.Options.Opt_CacheServer.Redis;
            QX_Frame_Helper_DG_Config.Cache_Redis_Host_ReadWrite_Array = jobject_qx_frame_config["cache"]["Cache_Redis_Host_ReadWrite_Array"].ToString().Split(',');
            QX_Frame_Helper_DG_Config.Cache_Redis_Host_OnlyRead_Array = jobject_qx_frame_config["cache"]["Cache_Redis_Host_OnlyRead_Array"].ToString().Split(',');
            QX_Frame_Helper_DG_Config.International_ConfigFileLocation = @"../../config/qx_frame.internationalization.json";
            QX_Frame_Helper_DG_Config.International_Language = "english";

            QX_Frame_Data_Config.ConnectionString_DB_QX_Frame_Test= jobject_qx_frame_config["database"]["connectionStrings"]["DB_QX_Frame_Test"].ToString();

            Trace.WriteLine("configuration bootstrap succeed !");
        }

    }
}
