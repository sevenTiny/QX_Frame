using System;

namespace QX_Frame.Bantina
{
    /*time:2016-10-30 21:07:41 author:qixiao*/

    public abstract class DateTime_Helper_DG
    {
        public static string Get_DateTime_Now_24HourType()
        {
            DateTime dt;
            DateTime.TryParseExact(DateTime.Now.ToString(), "dd/M/yyyy tt hh:mm:ss", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out dt);
            dt = DateTime.Now;
            return dt.ToString("yyyyMMdd HH:mm:ss");
        }
        /// <summary>
        /// get current time stamp
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        public static long GetTimeStampByDateTimeUtc(DateTime dateTimeUtcNow)
        {
            TimeSpan ts = dateTimeUtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        /// <summary>
        /// CodeExecuteTimeCaculate
        /// Create:2017-8-23 09:50:28
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static double CodeExecuteTimeCaculate(Action action)
        {
            //instance a StopWatch
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw start
            sw.Start();
            //Execute Code
            action();
            //sw stop
            sw.Stop();
            //return result
            return sw.Elapsed.TotalMilliseconds;
        }
    }
}
