namespace QX_Frame.Helper_DG
{
    /*2016-11-3 11:50:08 author:qixiao*/
    public abstract class Convert_Helper_DG 
    {
        //public static T Json_TO_T<T>(string json)
        //{
        //    try
        //    {
        //        return new JavaScriptSerializer().Deserialize<T>(json);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log_Helper_DG.Log_Error(ex.ToString(), "Json Convert Error ");
        //        return default(T);
        //    }
        //}

        /// <summary>
        /// Json_To_T
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="jsonText">string jsonText</param>
        /// <returns></returns>
        public static T Json_To_T<T>(string jsonText)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonText);
        }

        public static string T_To_Json<T>(T t)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(t);
        }
    }
}
