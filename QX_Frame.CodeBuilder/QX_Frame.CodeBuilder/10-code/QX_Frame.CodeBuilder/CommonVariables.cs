using CSharp_FlowchartToCode_DG.Entities;
using QX_Frame.Bantina;
using QX_Frame.Bantina.Options;
namespace CSharp_FlowchartToCode_DG
{
    public sealed class CommonVariables
    {

        public static string configFilePath = @"qixiaoSrc\QixiaoConfig.ini";          //配置文件的路径
        public static Opt_DataBaseType currentDataBaseType = Opt_DataBaseType.SqlServer;

        public static bool getServerInfoFinished = false;
        public static ServerInfo serverInfo=new ServerInfo ();

        /// <summary>
        /// SetCurrentDbConnection
        /// </summary>
        /// <param name="connectionStr"></param>
        /// <param name="dataBaseType"></param>
        public static void SetCurrentDbConnection(string connectionStr, Opt_DataBaseType dataBaseType)
        {
            Db_Helper_DG.ConnString_Default = connectionStr;
            Db_Helper_DG.dataBaseType = dataBaseType;
            CommonVariables.currentDataBaseType = dataBaseType;
        }
    }
}
