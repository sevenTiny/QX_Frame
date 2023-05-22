﻿/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-09-14 14:55:16
 * Update:2017-09-14 14:55:16
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
using System.Text.RegularExpressions;

namespace QX_Frame.Bantina.Validation
{
    public static class FormatValidation
    {
        /// <summary>
        /// check match regex string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="regexString"></param>
        /// <returns></returns>
        public static bool IsCheckFromRegex(this string data,string regexString)
        {
            return Regex.IsMatch(data, regexString);
        }
        public static bool IsEmail(this string data)
        {
            return Regex.IsMatch(data, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
        public static bool IsMobilePhone(this string data)
        {
            return Regex.IsMatch(data, @"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$");
        }
        public static bool IsTelPhone(this string data)
        {
            return Regex.IsMatch(data, @"^(\(\d{3,4}-)|\d{3.4}-)?\d{7,8}$");
        }
        public static bool IsURL(this string data)
        {
            return Regex.IsMatch(data, @"^((https?|ftp|news):\/\/)?([a-z]([a-z0-9\-]*[\.。])+([a-z]{2}|aero|arpa|biz|com|coop|edu|gov|info|int|jobs|mil|museum|name|nato|net|org|pro|travel)|(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]))(\/[a-z0-9_\-\.~]+)*(\/([a-z0-9_\-\.]*)(\?[a-z0-9+_\-\.%=&]*)?)?(#[a-z][a-z0-9_]*)?$");
        }
        public static bool IsIpAddress(this string data)
        {
            return Regex.IsMatch(data, @"^(1\d{2}|2[0-4]\d|25[0-5]|[1-9]\d|[1-9])\.(1\d{2}|2[0-4]\d|25[0-5]|[1-9]\d|\d)\.(1\d{2}|2[0-4]\d|25[0-5]|[1-9]\d|\d)\.(1\d{2}|2[0-4]\d|25[0-5]|[1-9]\d|\d)$");
        }
        public static bool IsID_Card(this string data)
        {
            return Regex.IsMatch(data, @"^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$");
        }
        //(字母开头，允许5-16字节，允许字母数字下划线)
        public static bool IsAccountName(this string data)
        {
            return Regex.IsMatch(data, @"^[a-zA-Z][a-zA-Z0-9_]{4,15}$");
        }
        //(以字母开头，长度在6~18之间，只能包含字母、数字和下划线)
        public static bool IsPassword(this string data)
        {
            return Regex.IsMatch(data, @"^[a-zA-Z]\w{5,17}$");
        }
        //(必须包含大小写字母和数字的组合，不能使用特殊字符，长度在8-10之间)
        public static bool IsStrongCipher(this string data)
        {
            return Regex.IsMatch(data, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,10}$");
        }
        public static bool IsDataFormat(this string data)
        {
            return Regex.IsMatch(data, @"^\d{4}-\d{1,2}-\d{1,2}");
        }
        public static bool IsChineseCharactor(this string data)
        {
            return Regex.IsMatch(data, @"[\u4e00-\u9fa5]");
        }
        public static bool IsQQ_Number(this string data)
        {
            return Regex.IsMatch(data, @"[1-9][0-9]{4,}");
        }
        public static bool IsPostalCode(this string data)
        {
            return Regex.IsMatch(data, @"[1-9]\d{5}(?!\d)");
        }
    }
}
