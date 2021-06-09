using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Core.DependencyInjection;

namespace GameHall.FrontEnd.Console
{
    public static class CommonConfigurator
    {
        public static void Configure(IServiceCollection serviceCollection, IConfigurationRoot configuration)
        {
            var rabbitMqSection = configuration.GetSection("RabbitMq");
            serviceCollection
                .AddRabbitMqClient(rabbitMqSection);
            
            serviceCollection.AddSingleton<IConnection>(c =>
            {
                var connection = new ConnectionFactory().CreateConnection();
                return connection;
            });

            serviceCollection.AddSingleton<IBus>(c => RabbitHutch.CreateBus("host=localhost"));

            //serviceCollection.AddCap(options =>
            //{
            //    options.UseInMemoryStorage();
            //    options.UseRabbitMQ("localhost");
            //    options.UseDashboard();
            //    options.ConsumerThreadCount = 0;
            //});
        }
    }
}