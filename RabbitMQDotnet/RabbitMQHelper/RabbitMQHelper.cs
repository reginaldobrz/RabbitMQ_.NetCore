using System;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client.Events;
using System.Threading.Tasks;
using System.Linq;

namespace RabbitMQApp.RabbitMQHelper
{
    public class RabbitMqHelper : IRabbitMQHelper
    {
        public ConnectionFactory GetConnectionFactory()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "127.0.0.1",
                Port=5672,
                UserName = "guest",
                Password = "guest"
            };
            return connectionFactory;
        }

        public string RetrieveSingleMessage(string queueName, IConnection connection)
        {
            BasicGetResult data;
            using (var channel = connection.CreateModel())
            {
                data = channel.BasicGet(queueName, true);
            }
            return data != null ? System.Text.Encoding.UTF8.GetString(data.Body) : null;
        }

        public uint RetrieveMessageCount(string queueName, IConnection connection)
        {
            uint data;
            using (var channel = connection.CreateModel())
            {
                data = channel.MessageCount(queueName);
            }
            return data;
        }

        public IConnection CreateConnection(ConnectionFactory connectionFactory)
        {
            return connectionFactory.CreateConnection();
        }

        public QueueDeclareOk CreateQueue(string queueName, IConnection connection)
        {
            QueueDeclareOk queue;
            using (var channel = connection.CreateModel())
            {
                queue = channel.QueueDeclare(queueName, true, false, false, null);
            }
            return queue;
        }

        public List<string> RetrieveMessageList(string queueName, IConnection connection)
        {
            
            int count = 0;
            var messageList = new List<string>();

            using (var channel = connection.CreateModel())
            {
                var messageCount = channel.MessageCount(queueName);
                channel.QueueDeclare(queueName, true, false, false, null);

                var consumer = new QueueingBasicConsumer(channel);

                const bool autoAck = false;
                channel.BasicConsume(queueName, autoAck, consumer);

                while (count < messageCount)
                {
                    var dequeue = consumer.Queue.Dequeue();

                    var body = dequeue.Body;
                    var message = Encoding.UTF8.GetString(body);

                    messageList.Add(message);
                    channel.BasicAck(dequeue.DeliveryTag, false);
                    count++;
                }
            }

            return messageList;
        }

        public bool WriteMessageOnQueue(string message, string queueName, IConnection connection)
        {
            using (var channel = connection.CreateModel())
            {
                channel.BasicPublish(string.Empty, queueName, null, Encoding.ASCII.GetBytes(message));
            }
            return true;
        }
    }
}