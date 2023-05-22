
/**
 * author:qixiao
 * create:2016-11-3 11:50:08
 * update:2017-5-15 15:41:16
 * */
namespace QX_Frame.Bantina
{
    public abstract class Convert_Helper_DG 
    {
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
