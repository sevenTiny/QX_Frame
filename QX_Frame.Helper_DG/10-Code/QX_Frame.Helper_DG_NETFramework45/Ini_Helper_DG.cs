using System.Text;
using System.Runtime.InteropServices;
using System;

namespace QX_Frame.Helper_DG
{
    //author:qixiao
    //time:2017-1-6 16:38:42
    public abstract class Ini_Helper_DG
    {
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="filePath">the ini filePath</param>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">insert value</param>
        /// <returns></returns>
        public static Boolean Add(string filePath,string section,string key,string value)
        {
            WritePrivateProfileString(section, key, value, filePath);
            return true;
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="filePath">the ini filePath</param>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">update value</param>
        /// <returns></returns>
        public static Boolean Update(string filePath, string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, filePath);
            return true;
        }
        /// <summary>
        /// Remove Section
        /// </summary>
        /// <param name="filePath">the ini filePath</param>
        /// <param name="section">section</param>
        /// <returns></returns>
        public static Boolean RemoveSection(string filePath, string section)
        {
            WritePrivateProfileString(section, null, null, filePath);
            return true;
        }
        /// <summary>
        /// Remove Value
        /// </summary>
        /// <param name="filePath">the ini filePath</param>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static Boolean RemoveValue(string filePath, string section,string key)
        {
            WritePrivateProfileString(section, key, null, filePath);
            return true;
        }
        /// <summary>
        /// get int value
        /// </summary>
        /// <param name="filePath">the ini filePath</param>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="ifNotFindDefaultReturn">default return if not found the value by key</param>
        /// <returns></returns>
        public static int selectIntValue(string filePath, string section, string key, int defaultValueIfNotFound = 0)
        {
            return GetPrivateProfileInt(section, key, defaultValueIfNotFound, filePath);
        }

        /// <summary>
        /// get string value
        /// </summary>
        /// <param name="filePath">the ini filePath</param>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="ifNotFindDefaultReturn">default return if not found the value by key</param>
        /// <param name="size">return lenth</param>
        /// <returns></returns>
        public static string selectStringValue(string filePath, string section, string key, string defaultValueIfNotFound = default(string), int size = 1024)
        {
            StringBuilder temp = new StringBuilder(size);
            GetPrivateProfileString(section, key, defaultValueIfNotFound, temp, size, filePath);
            return temp.ToString();
        }

        #region system operation
        /// <summary>   
        /// 读操作读取字符串         
        /// </summary>          
        /// <param name="section">要读取的段落名</param>         
        /// <param name="key">要读取的键</param>         
        /// <param name="defVal">读取异常的情况下的缺省值；如果Key值没有找到，则返回缺省的字符串的地址</param>         
        /// <param name="retVal">key所对应的值，如果该key不存在则返回空值</param>         
        /// <param name="size">返回值允许的大小</param>          
        /// <param name="filePath">INI文件的完整路径和文件名</param>         
        /// <returns></returns>         
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);


        /// <summary>          
        /// 读操作读取整数         
        /// </summary>          
        /// <param name="lpAppName">指向包含Section 名称的字符串地址</param>         
        /// <param name="lpKeyName">指向包含Key 名称的字符串地址</param>         
        /// <param name="nDefault">如果Key 值没有找到，则返回缺省的值是多少</param>          
        /// <param name="lpFileName">INI文件的完整路径和文件名</param>         
        /// <returns>返回获得的整数值</returns>         
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);


        /// <summary>         
        /// 写操作         
        /// </summary>
        /// <param name="section">要写入的段落名</param>          
        /// <param name="key">要写入的键，如果该key存在则覆盖写入</param>     
        /// <param name="val">key所对应的值</param>         
        /// <param name="filePath">INI文件的完整路径和文件名</param>         
        /// <returns></returns>         
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        #endregion
    }
}
