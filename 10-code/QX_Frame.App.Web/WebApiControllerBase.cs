using Newtonsoft.Json.Linq;
using QX_Frame.Helper_DG;
using QX_Frame.Helper_DG.Configs;
using QX_Frame.Helper_DG.Extends;
using System.Web.Mvc;

namespace QX_Frame.App.Web
{
    public abstract class WebControllerBase: WcfService
    {
        /// <summary>
        /// GetLB_XXX from config json file
        /// </summary>
        /// <param name="LB_Code"></param>
        /// <returns></returns>
        protected string GetLB_XXX(int LB_Code)
        {
            if (string.IsNullOrEmpty(QX_Frame_Helper_DG_Config.International_ConfigFileLocation))
            {
                throw new Exception_DG("QX_Frame_Helper_DG_Config.International_ConfigFileLocation must be provide correctly ! -- QX_Frame.Helper_DG.Extends.Exception_DG line:18");
            }
            JObject jobject = File_Helper_DG.Json_GetJObjectFromJsonFile(QX_Frame_Helper_DG_Config.International_ConfigFileLocation);//get json configuration file
            return jobject[QX_Frame_Helper_DG_Config.International_Language][$"LB_{LB_Code}"].ToString();
        }

        /// <summary>
        /// GetMSG_XXX from config json file
        /// </summary>
        /// <param name="MSG_Code"></param>
        /// <returns></returns>
        protected string GetMSG_XXX(int MSG_Code)
        {
            if (string.IsNullOrEmpty(QX_Frame_Helper_DG_Config.International_ConfigFileLocation))
            {
                throw new Exception_DG("QX_Frame_Helper_DG_Config.International_ConfigFileLocation must be provide correctly ! -- QX_Frame.Helper_DG.Extends.Exception_DG line:18");
            }
            JObject jobject = File_Helper_DG.Json_GetJObjectFromJsonFile(QX_Frame_Helper_DG_Config.International_ConfigFileLocation);//get json configuration file
            return jobject[QX_Frame_Helper_DG_Config.International_Language][$"MSG_{MSG_Code}"].ToString();
        }

        /// <summary>
        /// GetERROR_XXX from config json file
        /// </summary>
        /// <param name="ERROR_Code"></param>
        /// <returns></returns>
        protected string GetERROR_XXX(int ERROR_Code)
        {
            if (string.IsNullOrEmpty(QX_Frame_Helper_DG_Config.International_ConfigFileLocation))
            {
                throw new Exception_DG("QX_Frame_Helper_DG_Config.International_ConfigFileLocation must be provide correctly ! -- QX_Frame.Helper_DG.Extends.Exception_DG line:18");
            }
            JObject jobject = File_Helper_DG.Json_GetJObjectFromJsonFile(QX_Frame_Helper_DG_Config.International_ConfigFileLocation);//get json configuration file
            return jobject[QX_Frame_Helper_DG_Config.International_Language][$"ERROR_{ERROR_Code}"].ToString();
        }

        /// <summary>
        /// Get Json_DG Allow Get
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JsonResult Json_DG(object data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
