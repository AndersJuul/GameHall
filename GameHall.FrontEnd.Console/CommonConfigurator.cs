using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection;

namespace GameHall.FrontEnd.Console
{
    public static class CommonConfigurator
    {
        public static void Configure(IServiceCollection serviceCollection, IConfigurationRoot configuration)
        {
            var rabbitMqSection = configuration.GetSection("RabbitMq");
            var exchangeSection = configuration.GetSection("RabbitMqExchange");
            serviceCollection
                .AddRabbitMqClient(rabbitMqSection)
                .AddProductionExchange("exchange.name", exchangeSection);
        }
    }
}