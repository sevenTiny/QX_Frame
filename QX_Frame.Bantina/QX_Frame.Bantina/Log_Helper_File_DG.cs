using QX_Frame.Bantina.Extends;
using QX_Frame.Bantina.Configs;
using System;
using System.IO;

namespace QX_Frame.Bantina
{
    /// <summary>
    /// 20161030 14:27:23 qixiao
    /// </summary>
    public abstract class Log_Helper_File_DG
    {
        public static void Log(string logText, string logTitle = "QX_Frame General", Boolean isAppend = true)
        {
            OutPutStreamWriter(QX_Frame_Helper_DG_Config.Log_Location_General, logTitle, logText, isAppend);
        }
        public static void Log_Error(string logText, string logTitle = "QX_Frame Error", Boolean isAppend = true)
        {
            OutPutStreamWriter(QX_Frame_Helper_DG_Config.Log_Location_Error, logTitle, logText, isAppend);
        }
        public static void Log_Use(string logText, string logTitle = "QX_Frame USE", Boolean isAppend = true)
        {
            OutPutStreamWriter(QX_Frame_Helper_DG_Config.Log_Location_Use, logTitle, logText, isAppend);
        }

        //Out Put Method
        private static void OutPutStreamWriter(string logLocation,string logTitle,string logText,bool isAppend=true)
        {
            IO_Helper_DG.CreateDirectoryIfNotExist(logLocation);
            using (StreamWriter log = new StreamWriter($"{logLocation}Log_{DateTime.Now.ToDateString()}.Log", isAppend))
            {
                log.WriteLine();
                log.WriteLine($"{DateTime_Helper_DG.Get_DateTime_Now_24HourType()}   ---- {logTitle} Log !------");
                log.WriteLine(logText);
            }
        }
    }
}
