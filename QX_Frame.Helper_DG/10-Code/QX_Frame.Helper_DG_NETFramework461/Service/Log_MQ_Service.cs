using QX_Frame.Helper_DG.Log;
using System.Net.Http;

/**
 * author:qixiao
 * create:2017-7-24 18:24:06
 * */
namespace QX_Frame.Helper_DG.Service
{
    public class Log_MQ_Service
    {
        private RabbitMQ_Service_DG rabbitMQ = new RabbitMQ_Service_DG();

        public void Log(LogType logType)
        {
            using (Log.Log log = new Log.Log(logType))
            {
                this.rabbitMQ.BootStrap("log", "topic_exchange_log").SendMessage(log.ToJson());
            }
        }

        public void Log(LogType logType, string message)
        {
            using (Log.Log log = new Log.Log(logType, message))
            {
                this.rabbitMQ.BootStrap("log", "topic_exchange_log").SendMessage(log.ToJson());
            }
        }

        public void Log(LogType logType, HttpRequestMessage request)
        {
            using (Log.Log log = new Log.Log(logType, request))
            {
                this.rabbitMQ.BootStrap("log", "topic_exchange_log").SendMessage(log.ToJson());
            }
        }

        public void Log(LogType logType, HttpRequestMessage request, string message)
        {
            using (Log.Log log = new Log.Log(logType, request, message))
            {
                this.rabbitMQ.BootStrap("log", "topic_exchange_log").SendMessage(log.ToJson());
            }
        }
    }
}
