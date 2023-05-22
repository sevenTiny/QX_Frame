﻿using Newtonsoft.Json.Linq;
using QX_Frame.Helper_DG;
using QX_Frame.Helper_DG.Configs;
using QX_Frame.Helper_DG.Extends;

namespace QX_Frame.App.Base
{
    //author:qixiao
    //time:2017-1-2 20:45:40
    //summary: the Ioc override method

    public abstract class AppBase : WcfService
    {
        protected static string GetLB_XXX(int LB_Code)
        {
            if (string.IsNullOrEmpty(QX_Frame_Helper_DG_Config.International_ConfigFileLocation))
            {
                throw new Exception_DG("QX_Frame_Helper_DG_Config.International_ConfigFileLocation must be provide correctly ! -- QX_Frame.Helper_DG.Extends.Exception_DG line:18");
            }
            JObject jobject = File_Helper_DG.Json_GetJObjectFromJsonFile(QX_Frame_Helper_DG_Config.International_ConfigFileLocation);//get json configuration file
            return jobject[QX_Frame_Helper_DG_Config.International_Language][$"LB_{LB_Code}"].ToString();
        }
        protected static string GetMSG_XXX(int MSG_Code)
        {
            if (string.IsNullOrEmpty(QX_Frame_Helper_DG_Config.International_ConfigFileLocation))
            {
                throw new Exception_DG("QX_Frame_Helper_DG_Config.International_ConfigFileLocation must be provide correctly ! -- QX_Frame.Helper_DG.Extends.Exception_DG line:18");
            }
            JObject jobject = File_Helper_DG.Json_GetJObjectFromJsonFile(QX_Frame_Helper_DG_Config.International_ConfigFileLocation);//get json configuration file
            return jobject[QX_Frame_Helper_DG_Config.International_Language][$"MSG_{MSG_Code}"].ToString();
        }
        protected static string GetERROR_XXX(int ERROR_Code)
        {
            if (string.IsNullOrEmpty(QX_Frame_Helper_DG_Config.International_ConfigFileLocation))
            {
                throw new Exception_DG("QX_Frame_Helper_DG_Config.International_ConfigFileLocation must be provide correctly ! -- QX_Frame.Helper_DG.Extends.Exception_DG line:18");
            }
            JObject jobject = File_Helper_DG.Json_GetJObjectFromJsonFile(QX_Frame_Helper_DG_Config.International_ConfigFileLocation);//get json configuration file
            return jobject[QX_Frame_Helper_DG_Config.International_Language][$"ERROR_{ERROR_Code}"].ToString();
        }
    }
}
