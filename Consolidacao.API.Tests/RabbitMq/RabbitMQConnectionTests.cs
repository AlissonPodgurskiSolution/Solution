using RabbitMQ.Client;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Consolidacao.API.Tests.RabbitMq
{
    public class RabbitMQConnectionTests
    {
        private readonly string _rabbitMqHostName = "localhost";
        private readonly int _rabbitMqPort = 5672;
        private readonly string _username = "guest";
        private readonly string _password = "guest";

        [Fact]
        public void ShouldConnectToRabbitMQ()
        {
            // Arrange
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqHostName,
                Port = _rabbitMqPort,
                UserName = _username,
                Password = _password,
                RequestedConnectionTimeout = TimeSpan.FromSeconds(10),
                AutomaticRecoveryEnabled = true,
                TopologyRecoveryEnabled = true
            };

            // Act
            using var connection = factory.CreateConnection();

            // Assert
            Assert.IsNotNull(connection);
            Assert.IsTrue(connection.IsOpen, "A conexão com o RabbitMQ deveria estar aberta.");
        }
    }
}