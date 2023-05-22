
using QX_Frame.Bantina.Options;
/**
* author:qixiao
* create:2017-5-15 17:24:34
* desc:Helper_DG configuration
* */
namespace QX_Frame.Bantina.Configs
{
    public interface IQX_Frame_MSMQ
    {
    }
    public class QX_Frame_Helper_DG_Config : IQX_Frame_MSMQ
    {
        #region database

        /// <summary>
        /// qx_frame default connection string
        /// </summary>
        public static string ConnectionString_DB_QX_Frame_Default { get; set; } = default(string);

        public static Opt_DataBaseType DataBaseType { get; set; } = Opt_DataBaseType.SqlServer;

        #endregion

        #region log

        public static Opt_LogType LogType { get; set; } = Opt_LogType.LocalFile;

        public static string Log_Location_General { get; set; } = @"Log_QX_Frame/Log_QX_Frame_General/";
        public static string Log_Location_Error { get; set; } = @"Log_QX_Frame/Log_QX_Frame_Error/";
        public static string Log_Location_Use { get; set; } = @"Log_QX_Frame/Log_QX_Frame_Use/";

        #endregion

        #region cache

        public static Opt_CacheServer Cache_CacheServer { get; set; } = Opt_CacheServer.HttpRuntimeCache;
        public static bool Cache_IsCache { get; set; } = true;
        public static int Cache_CacheExpirationTimeSpan_Minutes { get; set; } = 10;

        #region Redis Client Config
        /// <summary>
        /// Host Default 127.0.0.1
        /// </summary>
        public static string[] Cache_Redis_Host_ReadWrite_Array { get; set; } = { "127.0.0.1:6379" };
        /// <summary>
        /// Host Default 127.0.0.1
        /// </summary>
        public static string[] Cache_Redis_Host_OnlyRead_Array { get; set; } = { "127.0.0.1:6379" };

        #endregion

        #endregion

        #region Internationalization config

        public static string International_ConfigFileLocation { get; set; }
        public static string International_Language { get; set; } = "english";

        #endregion

        #region MSMQ

        public static string MSMQ_RabbitMQ_Host { get; set; }
        public static string MSMQ_RabbitMQ_UserName { get; set; }
        public static string MSMQ_RabbitMQ_Password { get; set; }
        public static string MSMQ_RabbitMQ_VirtualHost { get; set; }
        public static ushort MSMQ_RabbitMQ_RequestedHeartBeat { get; set; }

        #endregion

    }
}
