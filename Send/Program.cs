using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { 
    HostName = "localhost",
    UserName = "user",
    Password = "123456"
    
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

while (true)
{
    try
    {
        Console.WriteLine("Write your message:");
        var message = Console.ReadLine();
        var body = Encoding.UTF8.GetBytes(message ?? "No message");

        channel.BasicPublish(exchange: string.Empty,
                             routingKey: "hello",
                             basicProperties: null,
                             body: body);
        Console.WriteLine($" [x] Sent {message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}
