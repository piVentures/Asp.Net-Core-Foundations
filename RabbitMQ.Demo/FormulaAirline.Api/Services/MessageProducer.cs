using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
namespace FormulaAirline.Api.Services;

    public class MessageProducer : IMessageProducer
{
    public async Task SendMessage<T>(T message)
    {
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

        var jsonString = JsonSerializer.Serialize(message);

        var body = Encoding.UTF8.GetBytes(jsonString);

        await channel.BasicPublishAsync(exchange: "", routingKey: "bookings", body: body);

    }

    public void SendingMessages<T>(T message)
    {
        throw new NotImplementedException();
    }
}



