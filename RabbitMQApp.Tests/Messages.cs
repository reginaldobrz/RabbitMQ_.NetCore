using RabbitMQ.Client;
using RabbitMQApp.RabbitMQHelper;
using Xunit;

namespace RabbitMQApp.Tests
{
    public class Messages
    {
        private readonly RabbitMqHelper _obj = new RabbitMqHelper();

        [Fact]
        public void CreateMessage_Success()
        {
            // Arrange
            var factory = _obj.GetConnectionFactory();
            var connection = _obj.CreateConnection(factory);
            var queue = _obj.CreateQueue("QueueName_Message_UnitTest", connection);

            // Act
            var ret = _obj.WriteMessageOnQueue("Message to Write", queue.QueueName, connection);

            // Assert
            Assert.True(ret);

            connection.Close();
            connection.Dispose();
        }

        [Fact]
        public void RetrieveMessage_Success()
        {
            // Arrange
            var factory = _obj.GetConnectionFactory();
            var connection = _obj.CreateConnection(factory);
            var queue = _obj.CreateQueue("QueueName_Message_UnitTest", connection);
            _obj.WriteMessageOnQueue("Message to Write", queue.QueueName, connection);

            // Act
            var ret = _obj.RetrieveSingleMessage("QueueName_Message_UnitTest", connection);

            // Assert
            Assert.True(ret != null);

            connection.Close();
            connection.Dispose();
        }

        [Fact]
        public void RetrieveMesssageList_Succcess()
        {
            // Arrange
            var factory = _obj.GetConnectionFactory();
            var connection = _obj.CreateConnection(factory);
            var queue = _obj.CreateQueue("QueueName_Message_UnitTest", connection);
            _obj.WriteMessageOnQueue("Message to Write", queue.QueueName, connection);

            // Act
            var ret = _obj.RetrieveMessageList("QueueName_Message_UnitTest", connection);

            // Assert
            Assert.True(ret != null);

            connection.Close();
            connection.Dispose();
        }
    }
}