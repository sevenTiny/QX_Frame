using Newtonsoft.Json.Linq;
using QX_Frame.Bantina;
using QX_Frame.Bantina.Configs;
using QX_Frame.Bantina.Extends;
using System;
using System.Diagnostics;

namespace Test.ConsoleApp1.NETFramework461.Config
{
    public class ConfigBootStrap
    {
        /// <summary>
        /// constructor
        /// </summary>
        public ConfigBootStrap()
        {
            Trace.WriteLine("configuration bootstraping ...");

            JObject jobject_qx_frame_config = IO_Helper_DG.Json_GetJObjectFromJsonFile("../../config/qx_frame.config.json");//get json configuration file

            QX_Frame_Helper_DG_Config.ConnectionString_DB_QX_Frame_Default = jobject_qx_frame_config["database"]["connectionStrings"]["QX_Frame_Default"].ToString();
            QX_Frame_Helper_DG_Config.DataBaseType = QX_Frame.Bantina.Options.Opt_DataBaseType.SqlServer;
            QX_Frame_Helper_DG_Config.Log_Location_General = jobject_qx_frame_config["log"]["Log_Location_General"].ToString();
            QX_Frame_Helper_DG_Config.Log_Location_Error = jobject_qx_frame_config["log"]["Log_Location_Error"].ToString();
            QX_Frame_Helper_DG_Config.Log_Location_Use = jobject_qx_frame_config["log"]["Log_Location_Use"].ToString();
            QX_Frame_Helper_DG_Config.Cache_IsCache = jobject_qx_frame_config["cache"]["IsCache"].ToInt() == 1;
            QX_Frame_Helper_DG_Config.Cache_CacheExpirationTimeSpan_Minutes = jobject_qx_frame_config["cache"]["CacheExpirationTime_Minutes"].ToInt();
            QX_Frame_Helper_DG_Config.Cache_CacheServer = QX_Frame.Bantina.Options.Opt_CacheServer.Redis;
            QX_Frame_Helper_DG_Config.Cache_Redis_Host_ReadWrite_Array =jobject_qx_frame_config["cache"]["Cache_Redis_Host_ReadWrite_Array"].ToString().Split(',');
            QX_Frame_Helper_DG_Config.Cache_Redis_Host_OnlyRead_Array = jobject_qx_frame_config["cache"]["Cache_Redis_Host_OnlyRead_Array"].ToString().Split(',');
            QX_Frame_Helper_DG_Config.MSMQ_RabbitMQ_Host = jobject_qx_frame_config["rabbitmq"]["Host"].ToString();
            QX_Frame_Helper_DG_Config.MSMQ_RabbitMQ_UserName = jobject_qx_frame_config["rabbitmq"]["UserName"].ToString();
            QX_Frame_Helper_DG_Config.MSMQ_RabbitMQ_Password = jobject_qx_frame_config["rabbitmq"]["Password"].ToString();
            QX_Frame_Helper_DG_Config.MSMQ_RabbitMQ_VirtualHost = jobject_qx_frame_config["rabbitmq"]["VirtualHost"].ToString();
            QX_Frame_Helper_DG_Config.MSMQ_RabbitMQ_RequestedHeartBeat = Convert.ToUInt16(jobject_qx_frame_config["rabbitmq"]["RequestedHeartBeat"]);

            QX_Frame_Helper_DG_Config.International_ConfigFileLocation = @"../../config/qx_frame.internationalization.json";
            QX_Frame_Helper_DG_Config.International_Language = "english";


            Trace.WriteLine("configuration bootstrap succeed !");
        }

    }
}
