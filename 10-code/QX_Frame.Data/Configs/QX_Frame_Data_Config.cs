/**
 * author:qixiao
 * create:2017-5-15 23:12:57
 * */
namespace QX_Frame.Data.Configs
{
    public class QX_Frame_Data_Config
    {
        public static string ConnectionString_DB_QX_Frame_Test { get; set; }

        public static int RequestExpireTime { get; set; }
        public static int AuthTokenExpireTime_days { get; set; }
        public static int AuthTokenExpireTime_hours { get; set; }
        public static int AuthTokenExpireTime_minutes { get; set; }

        public static string AppDomain { get; set; }
        public static string ApiDomain { get; set; }
    }
}
