using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using user.Interfaces;

namespace user.Services
{
    public class MessageService : IMessageService
    {
        private ConnectionFactory _factory;
        private IConnection _conn;
        private IModel _channel;
        private readonly IConfiguration _configuration;
        public MessageService(IConfiguration configuration)
        {
            _configuration = configuration;
            Console.WriteLine("Connecting to RabbitMQ");

            string hostname = _configuration.GetValue<string>("RabbitMQ:HostName");
            string username = _configuration.GetValue<string>("RabbitMQ:UserName");
            string password = _configuration.GetValue<string>("RabbitMQ:Password");
            int port = _configuration.GetValue<int>("RabbitMQ:Port");

            _factory = new ConnectionFactory() { HostName = hostname, Port = port };
            _factory.UserName = username;
            _factory.Password = password;
            _conn = _factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.QueueDeclare(queue: "geolocation",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
        }
        public bool Enqueue(int userId, bool visibility)
        {
            var message = new { userId = userId, visibility = visibility };
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: "",
                                routingKey: "geolocation",
                                basicProperties: null,
                                body: body);

            Console.WriteLine("Message Published to RabbitMQ");
            return true;
        }
    }
}