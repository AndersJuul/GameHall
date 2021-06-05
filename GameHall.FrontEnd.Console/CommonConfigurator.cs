using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection;

namespace GameHall.FrontEnd.Console
{
    public static class CommonConfigurator
    {
        public static void Configure(IServiceCollection serviceCollection, IConfigurationRoot configuration)
        {
            //var factory = new ConnectionFactory
            //{
            //    UserName = "guest", Password = "guest", VirtualHost = "dev", HostName = "localhost"
            //};

            //var conn = factory.CreateConnection();

            var rabbitMqSection = configuration.GetSection("RabbitMq");
            var exchangeSection = configuration.GetSection("RabbitMqExchange");
            serviceCollection
                .AddRabbitMqClient(rabbitMqSection)
                .AddProductionExchange("exchange.name", exchangeSection);
        }
    }
}