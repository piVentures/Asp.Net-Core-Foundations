using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


Console.WriteLine("Welcome to the ticketing service");

var factory = new ConnectionFactory()
{
      HostName = "localhost",
      UserName = "user",
      Password = "mypass",
      VirtualHost = "/"
};

// Create a connection to the RabbitMQ server
       await using var conn = await factory.CreateConnectionAsync();
// Create a channel within the connection
        await using var channel =  await conn.CreateChannelAsync();
 
        await channel.QueueDeclareAsync("bookings", durable: true, exclusive: false);

      var consumer = new AsyncEventingBasicConsumer(channel);

    consumer.ReceivedAsync += async (model, eventArgs) =>
    {
        var body = eventArgs.Body.ToArray();

        var message = Encoding.UTF8.GetString(body);

        Console.WriteLine($"new ticket processing is initiated - {message}");

    
    };

   await channel.BasicConsumeAsync(
      "bookings",
        true,
        consumer);

Console.ReadKey();