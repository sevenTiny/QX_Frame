using System;

namespace QX_Frame.Helper_DG
{
    /**
     *author:qixiao
     * time:2017-1-31 01:18:44
     **/
    public static class ToTypesExtends
    {
        #region Convert to T from String
        
        public static Int32 ToInt(this string str)
        {
            return Convert.ToInt32(str);    //default int32
        }
        public static Int16 ToInt16(this string str)
        {
            return Convert.ToInt16(str);
        }
        public static Int32 ToInt32(this string str)
        {
            return Convert.ToInt32(str);
        }
        public static Int64 ToInt64(this string str)
        {
            return Convert.ToInt64(str);
        }
        public static DateTime ToDateTime(this string str)
        {
            return Convert.ToDateTime(str);
        }

        #endregion

        #region Convert to T from object

        public static Int32 ToInt(this object obj)
        {
            return Convert.ToInt32(obj);    //default int32
        }
        public static Int16 ToInt16(this object obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int32 ToInt32(this object obj)
        {
            return Convert.ToInt32(obj);
        }
        public static Int64 ToInt64(this object obj)
        {
            return Convert.ToInt64(obj);
        }
        public static DateTime ToDateTime(this object obj)
        {
            return Convert.ToDateTime(obj);
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
