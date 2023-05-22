using QX_Frame.Bantina.Configs;
using RabbitMQ.Client;
using System;
using System.Text;

namespace QX_Frame.Bantina.Service
{
    public interface IMSMQ_Service_DG
    {
        void BootStrap();
        bool SendMessage(string message);
    }
    public class RabbitMQ_Service_DG: IMSMQ_Service_DG
    {
        public string QueueName { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeType { get; set; } = "topic";//direct，fanout，topic，headers
        public string RoutingKey { get; set; } = "*";

        private ConnectionFactory ConnectFactory { get; set; }

        #region Set Properties
        public RabbitMQ_Service_DG SetExchangeName(string exchangeName)
        {
            this.ExchangeName = exchangeName;
            return this;
        }
        public RabbitMQ_Service_DG SetExchangeType(string exchangeType)
        {
            this.ExchangeType = exchangeType;
            return this;
        }
        public RabbitMQ_Service_DG SetRoutingKey(string routingKey)
        {
            this.RoutingKey = routingKey;
            return this;
        }
        #endregion

        /// <summary>
        /// BootStrap
        /// </summary>
        void IMSMQ_Service_DG.BootStrap()
        {
            throw new NotImplementedException("Please Execute Overload Function That Have Arguments !");
        }

        /// <summary>
        /// BootStrap
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="exchangeName"></param>
        public RabbitMQ_Service_DG BootStrap(string queueName, string exchangeName)
        {
            this.QueueName = queueName;
            this.ExchangeName = exchangeName;
            this.ConnectFactory = new ConnectionFactory
            {
                UserName = QX_Frame_Helper_DG_Config.MSMQ_RabbitMQ_UserName,
                Password = QX_Frame_Helper_DG_Config.MSMQ_RabbitMQ_Password,
                VirtualHost = QX_Frame_Helper_DG_Config.MSMQ_RabbitMQ_VirtualHost,
                RequestedHeartbeat = QX_Frame_Helper_DG_Config.MSMQ_RabbitMQ_RequestedHeartBeat,
                Endpoint = new AmqpTcpEndpoint(new Uri(QX_Frame_Helper_DG_Config.MSMQ_RabbitMQ_Host))
            };
            return this;
        }

        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SendMessage(string message)
        {
            if (this.ConnectFactory == null)
            {
                throw new NullReferenceException("Must Execute BootStrap First !");
            }
            if (string.IsNullOrEmpty(this.ExchangeName))
            {
                throw new ArgumentException("ExchangeName Must Be Support , Please Call BootStrap(string queueName, string exchangeName) Or SetExchangeName(string exchangeName) To Setting Up !");
            }
            using (var connection = this.ConnectFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //set exchange
                    channel.ExchangeDeclare(this.ExchangeName, this.ExchangeType);
                    //declare queue，set durable exclusive deleteAuto
                    channel.QueueDeclare(this.QueueName, true, false, false, null);
                    //binding queue name.exchange,routing
                    channel.QueueBind(this.QueueName, this.ExchangeName, this.RoutingKey);

                    var properties = channel.CreateBasicProperties();
                    //durable
                    properties.Persistent = true;

                    byte[] bytes = Encoding.UTF8.GetBytes(message);
                    //Send Message
                    channel.BasicPublish(this.ExchangeName, this.RoutingKey, properties, bytes);
                }
            }
            return true;
        }
    }
}
