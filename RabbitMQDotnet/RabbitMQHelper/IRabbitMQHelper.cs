using System.Collections.Generic;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQApp.RabbitMQHelper
{
    public interface IRabbitMQHelper
    {
        ConnectionFactory GetConnectionFactory();

        string RetrieveSingleMessage(string queueName, IConnection connection);

        uint RetrieveMessageCount(string queueName, IConnection connection);

        IConnection CreateConnection(ConnectionFactory connectionFactory);

        QueueDeclareOk CreateQueue(string queueName, IConnection connection);

        List<string> RetrieveMessageList(string queueName, IConnection connection);

        bool WriteMessageOnQueue(string message, string queueName, IConnection connection);
    }
}