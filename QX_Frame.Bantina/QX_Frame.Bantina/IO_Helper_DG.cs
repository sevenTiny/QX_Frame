/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-1-6 17:11:40
 * Update:2017-8-18 10:43:33
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * Personal WebSit: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:-.
 * Thx , Best Regards ~
 *********************************************************/
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;


namespace QX_Frame.Bantina
{
    public abstract class IO_Helper_DG
    {
        public static bool CopyFile(string sourceFilePath, string newFilePath, bool allowCoverSameNameFiles = true)
        {
            File.Copy(sourceFilePath, newFilePath, allowCoverSameNameFiles);//允许覆盖同名文件
            return true;
        }
        //create Directory
        public static bool CreateDirectoryIfNotExist(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            return true;
        }
        //create file
        public static bool CreateFileIfNotExist(string filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            return true;
        }

        public static void OpenDirectory(string directoryPath)
        {
            System.Diagnostics.Process.Start("Explorer.exe", directoryPath);
        }

        #region Zip UnZip

        /// <summary>
        /// Zip File
        /// </summary>
        /// <param name="inputFilePath"></param>
        /// <param name="outPutFilePath"></param>
        /// <param name="zipLevel"></param>
        /// <returns></returns>
        public static bool ZipFile(string inputFilePath, string outPutFilePath="out.zip", int zipLevel = 6)
        {
            ZipOutputStream s = new ZipOutputStream(File.Create(outPutFilePath));
            s.SetLevel(zipLevel); // 0 - store only to 9 - means best compression
            ZipFile(inputFilePath, s);
            s.Finish();
            s.Close();
            return true;
        }

        /// <summary>
        /// Zip Path
        /// </summary>
        /// <param name="inputDirPath"></param>
        /// <param name="outPutFilePath"></param>
        /// <param name="zipLevel"></param>
        /// <returns></returns>
        public static bool ZipDir(string inputDirPath, string outPutFilePath = "out.zip", int zipLevel = 6)
        {
            if (inputDirPath[inputDirPath.Length - 1] != Path.DirectorySeparatorChar)
                inputDirPath += Path.DirectorySeparatorChar;
            ZipOutputStream s = new ZipOutputStream(File.Create(outPutFilePath));
            s.SetLevel(zipLevel); // 0 - store only to 9 - means best compression
            ZipDir(inputDirPath, s, inputDirPath);
            s.Finish();
            s.Close();
            return true;
        }

        private static void ZipDir(string strFile, ZipOutputStream s, string staticFile)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar) strFile += Path.DirectorySeparatorChar;
            Crc32 crc = new Crc32();
            string[] filenames = Directory.GetFileSystemEntries(strFile);
            foreach (string file in filenames)
            {
                if (Directory.Exists(file))
                {
                    ZipDir(file, s, staticFile);
                }
                else // 否则直接压缩文件
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempfile);

                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);

                    s.Write(buffer, 0, buffer.Length);
                }
            }
        }
        private static void ZipFile(string strFile, ZipOutputStream s)
        {
            Crc32 crc = new Crc32();
            //打开压缩文件
            FileStream fs = File.OpenRead(strFile);

            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            string tempfile = strFile.Substring(strFile.LastIndexOf("\\") + 1);
            ZipEntry entry = new ZipEntry(tempfile);

            entry.DateTime = DateTime.Now;
            entry.Size = fs.Length;
            fs.Close();
            crc.Reset();
            crc.Update(buffer);
            entry.Crc = crc.Value;
            s.PutNextEntry(entry);

            s.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// UnZipFile
        /// </summary>
        /// <param name="targetZipFile"></param>
        /// <param name="saveDir"></param>
        /// <returns></returns>
        public static string UnZipFile(string targetZipFile, string saveDir)
        {
            string rootFile = " ";
            //读取压缩文件(zip文件)，准备解压缩
            ZipInputStream s = new ZipInputStream(File.OpenRead(targetZipFile.Trim()));
            ZipEntry theEntry;
            string path = saveDir;
            //解压出来的文件保存的路径

            string rootDir = " ";
            //根目录下的第一个子文件夹的名称
            while ((theEntry = s.GetNextEntry()) != null)
            {
                rootDir = Path.GetDirectoryName(theEntry.Name);
                //得到根目录下的第一级子文件夹的名称
                if (rootDir.IndexOf("\\") >= 0)
                {
                    rootDir = rootDir.Substring(0, rootDir.IndexOf("\\") + 1);
                }
                string dir = Path.GetDirectoryName(theEntry.Name);
                //根目录下的第一级子文件夹的下的文件夹的名称
                string fileName = Path.GetFileName(theEntry.Name);
                //根目录下的文件名称
                if (dir != " ")
                //创建根目录下的子文件夹,不限制级别
                {
                    path = saveDir + "\\" + dir;
                    if (!Directory.Exists(saveDir + "\\" + dir))
                    {
                        //在指定的路径创建文件夹
                        Directory.CreateDirectory(path);
                    }
                }
                else if (dir == " " && fileName != "")
                //根目录下的文件
                {
                    path = saveDir;
                    rootFile = fileName;
                }
                else if (dir != " " && fileName != "")
                //根目录下的第一级子文件夹下的文件
                {
                    if (dir.IndexOf("\\") > 0)
                    //指定文件保存的路径
                    {
                        path = saveDir + "\\" + dir;
                    }
                }

                if (dir == rootDir)
                //判断是不是需要保存在根目录下的文件
                {
                    path = saveDir + "\\" + rootDir;
                }

                //以下为解压缩zip文件的基本步骤
                //基本思路就是遍历压缩文件里的所有文件，创建一个相同的文件。
                if (fileName != String.Empty)
                {
                    FileStream streamWriter = File.Create(path + "\\" + fileName);

                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        if (s.GetNextEntry() != null)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    streamWriter.Close();
                }
            }
            s.Close();

            return rootFile;
        }

        
        #endregion

        #region Path

        public static string DeskTopPath
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); }
        }

        public static string RootPath_Wcf
        {
            get { return System.Web.Hosting.HostingEnvironment.MapPath("~"); }
        }

        public static string RootPath_MVC
        {
            get { return System.Web.HttpContext.Current.Server.MapPath("~"); }
        }

        #endregion

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
            if (key.Contains("/") || key.Contains("\\"))
            {
                key.Replace("\\", "/");
                string[] array = key.Split('/');
                key = array[array.Length - 1];
            }
            object jobjectInCache = HttpRuntimeCache_Helper_DG.Cache_Get(key);
            if (jobjectInCache != null)
            {
                jObject = (JObject)jobjectInCache;
            }
            else
            {
                jObject = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(ReadFileStringContent(filePath));
                HttpRuntimeCache_Helper_DG.Cache_Add(key, jObject);
            }
            return jObject;
        }
        #endregion
    }
}
