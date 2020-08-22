using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Worker;
using RabbitWorker.Data;
using System.Configuration;

class Program
{
    public static void Main()
    {
        var hostName = ConfigurationManager.AppSettings.Get("HostName");
        var userName = ConfigurationManager.AppSettings.Get("userName");
        var password = ConfigurationManager.AppSettings.Get("password");

        var factory = new ConnectionFactory()
        {
            HostName = hostName,
            UserName = userName,
            Password = password
        };

        // TODO: DI
        var context = new WorkerContext();
        context.Database.EnsureCreated();

        var processor = new MessageProcessor(context);

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Received Message: {0}", message);

                processor.ProcessMessage(message);

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: "esp8266_amqp",
                                 autoAck: false,
                                 consumer: consumer);

            while (true) { }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
