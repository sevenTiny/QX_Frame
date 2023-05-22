using System;

namespace QX_Frame.Bantina.Extends
{
    /**
     *author:qixiao
     * time:2017-1-31 01:18:44
     **/
    public static class ToTypesExtends
    {
        #region Quick Method
        public static string ToHashString(this object obj)
        {
            return obj.GetHashCode().ToString();
        }
        #endregion

        #region Convert to aim type from object
        public static Boolean ToBoolean(this object obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static Int16 ToShort(this object obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int32 ToInt(this object obj)
        {
            return Convert.ToInt32(obj);
        }
        public static Int64 ToLong(this object obj)
        {
            return Convert.ToInt64(obj);
        }
        public static double ToDouble(this object obj)
        {
            return Convert.ToDouble(obj);
        }
        public static decimal ToDecimal(this object obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static DateTime ToDateTime(this object obj)
        {
            return Convert.ToDateTime(obj);
        }
        public static string ToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        #endregion

        #region Convert to string from DateTime
        public static string ToDateTimeString_24HourType(this DateTime dt,string separatorOfDate="",string separatorOfTime=":")
        {
            return dt.ToString($"yyyy{separatorOfDate}MM{separatorOfDate}dd HH{separatorOfTime}mm{separatorOfTime}ss");
        }
        public static string ToDateTimeString_12HourType(this DateTime dt, string separatorOfDate = "", string separatorOfTime = ":")
        {
            return dt.ToString($"yyyy{separatorOfDate}MM{separatorOfDate}dd hh{separatorOfTime}mm{separatorOfTime}ss");
        }
        public static string ToDateString(this DateTime dt, string separatorOfDate = "")
        {
            return dt.ToString($"yyyy{separatorOfDate}MM{separatorOfDate}dd");
        }
        public static string ToTimeString_24HourType(this DateTime dt, string separatorOfTime = ":")
        {
            return dt.ToString($"HH{separatorOfTime}mm{separatorOfTime}ss");
        }
        public static string ToTimeString_12HourType(this DateTime dt, string separatorOfTime = ":")
        {
            return dt.ToString($"hh{separatorOfTime}mm{separatorOfTime}ss");
        }

        #endregion
    }
}
