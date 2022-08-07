using Framework.User.Service.Contract;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Taskers
{
    public class RegisterTasker
    {
        private const string ComponentName = "app:rmqscene component:register";

        private readonly string _hostname;
        private readonly string _queuename;
        private IConnection _connection;

        public RegisterTasker(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queuename = "registrations";

            CreateConnection();
        }

        public void Send(string message)
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _queuename, durable: true, exclusive: false, autoDelete: false, arguments: null);

                    channel.BasicPublish(exchange: "",
                                         routingKey: _queuename,
                                         basicProperties: null,
                                         body: Encoding.UTF8.GetBytes(message));
                }
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    ClientProvidedName = ComponentName,
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }
    }
}
