using System.Text;
using geolocation.Interfaces;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace geolocation.Services
{
    class MessageReceiver : IMessageReceiver
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private readonly IServiceProvider _services;
        private readonly IConfiguration _configuration;
        public MessageReceiver(IServiceProvider services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        public void StartReceiving()
        {
            string hostname = _configuration.GetValue<string>("RabbitMQ:HostName");
            string username = _configuration.GetValue<string>("RabbitMQ:UserName");
            string password = _configuration.GetValue<string>("RabbitMQ:Password");
            int port = _configuration.GetValue<int>("RabbitMQ:Port");

            _factory = new ConnectionFactory() { HostName = hostname, Port = port };
            _factory.UserName = username;
            _factory.Password = password;

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "geolocation",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += OnMessageReceived;
            _channel.BasicConsume(queue: "geolocation",
                                autoAck: true,
                                consumer: consumer);
        }

        public void StopReceiving()
        {
            _channel.Close();
            _connection.Close();
        }

        private void OnMessageReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var payload = Encoding.UTF8.GetString(body);
            Console.WriteLine("[x] Received from RabbitMQ: {0}", payload);

            var json = JObject.Parse(payload);

            var userId = json["userId"].Value<int>();
            var visibility = json["visibility"].Value<bool>();


            using (var scope = _services.CreateScope())
            {
                var geolocationRepository = scope.ServiceProvider.GetRequiredService<IGeolocationRepository>();
                geolocationRepository.ChangeVisibility(userId, visibility);
            }
            
        }

    }
}