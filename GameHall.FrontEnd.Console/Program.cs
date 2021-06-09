using System;
using System.Threading.Tasks;
using EasyNetQ;
using GameHall.SharedKernel.Core.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace GameHall.FrontEnd.Console
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true);
            var configuration = builder.Build();

            var serviceCollection = new ServiceCollection();

            SharedKernel.Core.CommonConfigurator.Configure(serviceCollection);
            SharedKernel.Infrastructure.CommandHandling.CommonConfigurator.Configure(serviceCollection);
            SharedKernel.Infrastructure.DataStorage.CommonConfigurator.Configure(serviceCollection);
            UserManagement.ApplicationServices.CommonConfigurator.Configure(serviceCollection);

            CommonConfigurator.Configure(serviceCollection, configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200")))
                .CreateLogger();

            Log.Logger.Debug("Started");
            var bus = serviceProvider.GetRequiredService<IBus>();
            await bus.PubSub.PublishAsync(new CreateUser(Guid.NewGuid(), "aju"));
            await Task.Delay(3000);
        }
    }
}