using System;
using System.Threading.Tasks;
using GameHall.SharedKernel.Core;
using GameHall.UserManagement.ApplicationServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace GameHall.FrontEnd.Console
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");

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
                //.WriteTo.File(Path.Combine(ilitFolder.Path, "logfile.log"), rollingInterval: RollingInterval.Day)
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200")))
                .CreateLogger();

            var userService = serviceProvider.GetRequiredService<IUserService>();



            var userId = Guid.NewGuid();
            await userService.CreateUser(userId, "aju");
        }
    }
}
