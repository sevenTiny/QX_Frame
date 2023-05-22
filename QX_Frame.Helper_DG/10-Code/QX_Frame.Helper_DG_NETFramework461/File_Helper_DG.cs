using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

/**
 * author:qixiao
 * create:2017-5-15 15:21:17
 * */
namespace QX_Frame.Helper_DG
{
    /// <summary>
    /// the File_Helper_DG adjust to IO_Helper_DG
    /// </summary>
    [Obsolete("the File_Helper_DG adjust to IO_Helper_DG ,recommend to use IO_Helper_DGs")]
    public class File_Helper_DG
    {
        #region Txt File Opreation

        public static Boolean Txt_Write(string filePath, string text, bool isAppend = true)
        {
            IO_Helper_DG.CreateFileIfNotExist(filePath);
            using (StreamWriter log = new StreamWriter(filePath, isAppend))
            {
                log.Write(text);
            }
            return true;
        }
        public static Boolean Txt_WriteLine(string filePath, string text, bool isAppend = true)
        {
            IO_Helper_DG.CreateFileIfNotExist(filePath);
            using (StreamWriter log = new StreamWriter(filePath, isAppend))
            {
                log.WriteLine(text);
            }
            return true;
        }
        /// <summary>
        /// read file string from file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadFileStringContent(string filePath)
        {
            using (StreamReader stream = new StreamReader(filePath, Encoding.UTF8))
            {
                return stream.ReadToEnd().ToString();
            }
        }
        #endregion

        #region AppSetting（config files)

        public static string AppSetting_Get(string appSettingKey)
        {
            Configuration configuration = null;
            if (System.Web.HttpContext.Current != null)
            {
                configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            }
            else
            {
                configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            return configuration.AppSettings.Settings[appSettingKey].Value.Trim();
        }

        public static Boolean AppSetting_Add(string appSettingKey, string appSettingValue)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Add(appSettingKey, appSettingValue);
            config.Save(ConfigurationSaveMode.Modified);            // must save
            ConfigurationManager.RefreshSection("appSettings");     //must refresh
            return true;
        }

        public static Boolean AppSetting_Update(string appSettingKey, string appSettingValue)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[appSettingKey].Value = appSettingValue; //set value
            config.Save(ConfigurationSaveMode.Modified);            // must save
            ConfigurationManager.RefreshSection("appSettings");     //must refresh
            return true;
        }

        public static Boolean AppSetting_Delete(string appSettingKey)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(appSettingKey);
            config.Save(ConfigurationSaveMode.Modified);            // must save
            ConfigurationManager.RefreshSection("appSettings");     //must refresh
            return true;
        }

        #endregion

        #region ConnectionString -> get database connection string
        /// <summary>
        /// Get ConnectionString By Key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public static string ConnectionString_Get(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
        #endregion

        #region Init File

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="filePath">the ini filePath</param>
        /// <param name="section">section</param>
        /// <param name="key">key</param>
        /// <param name="value">insert value</param>
        /// <returns></returns>
        public static Boolean Ini_Add(string filePath, string section, string key, string value)
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
        public static Boolean Ini_Update(string filePath, string section, string key, string value)
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
        public static Boolean Ini_RemoveSection(string filePath, string section)
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
        public static Boolean Ini_RemoveValue(string filePath, string section, string key)
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
        public static int Ini_SelectIntValue(string filePath, string section, string key, int defaultValueIfNotFound = 0)
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
        public static string Ini_SelectStringValue(string filePath, string section, string key, string defaultValueIfNotFound = default(string), int size = 1024)
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

        #endregion

        #region Json File

        /// <summary>
        /// Get Json JObject From Json String
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static JObject Json_GetJObjectFromJsonString(string jsonText)
        {
            return (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonText);
        }
        /// <summary>
        /// Get Json JObject From Json File
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static JObject Json_GetJObjectFromJsonFile(string filePath)
        {
            JObject jObject = new JObject();
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("the file not found in the filePath");
            }
            string key = filePath;
            if (key.Contains("/")||key.Contains("\\"))
            {
                key.Replace("\\", "/");
                string[] array = key.Split('/');
                key = array[array.Length - 1];
            }
            object jobjectInCache = HttpRuntimeCache_Helper_DG.Cache_Get(key);
            if (jobjectInCache!=null)
            {
                 jObject=(JObject)jobjectInCache;
            }
            else
            {
                jObject= (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(ReadFileStringContent(filePath));
                HttpRuntimeCache_Helper_DG.Cache_Add(key,jObject);
            }
            return jObject;
        }
        #endregion
    }
}
