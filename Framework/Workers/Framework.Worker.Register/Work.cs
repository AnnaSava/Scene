using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Worker.Register
{
    public class Work
    {
        private readonly string hostName;
        private readonly string queueName;

        public Work(string hostName, string queueName)
        {
            this.hostName = hostName;
            this.queueName = queueName;
        }

        public void Execute(Action<string, IServiceCollection> callbackAction,IServiceCollection services)
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    using (var logChannel = connection.CreateModel())
                    {
                        using (var resChannel = connection.CreateModel())
                        {
                            var consumer = new EventingBasicConsumer(channel);
                            consumer.Received += (model, ea) =>
                            {
                                var body = ea.Body.ToArray();
                                var message = Encoding.UTF8.GetString(body);

                                try
                                {
                                    callbackAction(message, services);
                                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("[{0}] Error: {1}", DateTime.Now, e.Message);
                                }

                            };

                            // TODO вылетает эксепшен, если очередь еще не создана
                            channel.BasicConsume(queue: queueName,
                                                 autoAck: false,
                                                 consumer: consumer);

                            Console.WriteLine(" Press [enter] to exit.");
                            Console.ReadLine();
                        }
                    }
                }
            }
        }
    }
}
