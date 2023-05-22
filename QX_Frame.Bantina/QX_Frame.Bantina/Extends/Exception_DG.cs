using Newtonsoft.Json.Linq;
using QX_Frame.Bantina.Configs;
using System;

/**
 * author:qixiao
 * create:2017-4-6 20:45:07
 * update:2017-5-16 17:40:36
 * desc:the exception class define
 * */
namespace QX_Frame.Bantina.Extends
{
    public class Exception_DG : ApplicationException
    {
        /// <summary>
        /// Exception_DG
        /// </summary>
        /// <param name="message">Error Message</param>
        public Exception_DG(string message) : base(message) { }

        public Exception_DG(string message, int errorCode) : base(message) { this.ErrorCode = errorCode; }

        public Exception_DG(string message, int errorCode, int errorLevel) : base(message) { this.ErrorCode = errorCode; this.ErrorLevel = errorLevel; }

        /// <summary>
        /// Exception_DG
        /// </summary>
        /// <param name="arguments">Error Arguments</param>
        /// <param name="message">Error Message</param>
        public Exception_DG(string arguments, string message) : base(message) { this.Arguments = arguments; }

        public Exception_DG(string arguments, string message, int errorCode) : base(message) { this.Arguments = arguments; this.ErrorCode = errorCode; }

        public Exception_DG(string arguments, string message, int errorCode, int errorLevel) : base(message) { this.Arguments = arguments; this.ErrorCode = errorCode; this.ErrorLevel = errorLevel; }


        /// <summary>
        /// Error Arguments
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public override string Message
        {
            get
            {
                return base.Message;
            }
        }

        /// <summary>
        /// Error Code
        /// </summary>
        public int ErrorCode { get; set; } = 0;

        /// <summary>
        /// Error Level
        /// </summary>
        public int ErrorLevel { get; set; } = 0;
    }
    public class Exception_DG_Internationalization : ApplicationException
    {
        public Exception_DG_Internationalization(int errorCode) : base("Refrence Message_DG")
        {
            if (string.IsNullOrEmpty(QX_Frame_Helper_DG_Config.International_ConfigFileLocation))
            {
                throw new Exception_DG("QX_Frame_Helper_DG_Config.International_ConfigFileLocation must be provide correctly ! -- QX_Frame.Bantina.Extends.Exception_DG line:69");
            }
            JObject jobject = IO_Helper_DG.Json_GetJObjectFromJsonFile(QX_Frame_Helper_DG_Config.International_ConfigFileLocation);//get json configuration file
            this.Message_DG = jobject[QX_Frame_Helper_DG_Config.International_Language][$"ERROR_{errorCode}"].ToString();
            this.ErrorCode = errorCode;
        }

        public Exception_DG_Internationalization(int errorCode, int errorLevel) : base("Refrence Message_DG")
        {
            if (string.IsNullOrEmpty(QX_Frame_Helper_DG_Config.International_ConfigFileLocation))
            {
                throw new Exception_DG("QX_Frame_Helper_DG_Config.International_ConfigFileLocation must be provide correctly ! -- QX_Frame.Bantina.Extends.Exception_DG line:29");
            }
            JObject jobject = IO_Helper_DG.Json_GetJObjectFromJsonFile(QX_Frame_Helper_DG_Config.International_ConfigFileLocation);//get json configuration file
            this.Message_DG = jobject[QX_Frame_Helper_DG_Config.International_Language][$"ERROR_{errorCode}"].ToString();
            this.ErrorCode = errorCode;
            this.ErrorLevel = errorLevel;
        }

        /// <summary>
        /// Error Arguments
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public override string Message
        {
            get
            {
                return base.Message;
            }
        }

        /// <summary>
        /// Message_DG
        /// </summary>
        public string Message_DG { get; set; }

        /// <summary>
        /// Error Code
        /// </summary>
        public int ErrorCode { get; set; } = 0;

        /// <summary>
        /// Error Level
        /// </summary>
        public int ErrorLevel { get; set; } = 0;
    }
}
