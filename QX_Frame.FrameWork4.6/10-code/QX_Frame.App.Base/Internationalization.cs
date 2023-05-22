using Newtonsoft.Json.Linq;
using QX_Frame.Helper_DG;
using QX_Frame.Helper_DG.Configs;
using QX_Frame.Helper_DG.Extends;
/**
 * author:qixiao
  * * crete:2017-5-16 14:08:31
 * */
namespace QX_Frame.App.Base
{
    public class Internationalization
    {
        /// <summary>
        /// get string value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(string key)
        {
            return GetJObject()[QX_Frame_Helper_DG_Config.International_Language][key].ToString();
        }
        /// <summary>
        /// get int value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            return GetJObject()[QX_Frame_Helper_DG_Config.International_Language][key].ToInt();
        }
        /// <summary>
        /// get double value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static double GetDouble(string key)
        {
            return GetJObject()[QX_Frame_Helper_DG_Config.International_Language][key].ToDouble();
        }

        /// <summary>
        /// Get JObject from json config file
        /// </summary>
        /// <returns></returns>
        private static JObject GetJObject()
        {
            if (string.IsNullOrEmpty(QX_Frame_Helper_DG_Config.International_ConfigFileLocation))
            {
                throw new Helper_DG.Extends.Exception_DG("QX_Frame_Config.International_ConfigFileLocation must be provide correctly ! -- QX_Frame.Helper_DG.Extends.Exception_DG line:18");
            }
            return File_Helper_DG.Json_GetJObjectFromJsonFile(QX_Frame_Helper_DG_Config.International_ConfigFileLocation);//get json configuration file
        }
    }
}
