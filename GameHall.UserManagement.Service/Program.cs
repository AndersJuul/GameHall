using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GameHall.SharedKernel.Core;

namespace GameHall.UserManagement.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var serviceCollection = new ServiceCollection();

                    SharedKernel.Core.CommonConfigurator.Configure(serviceCollection);
                    //SharedKernel.Infrastructure.CommandHandling.CommonConfigurator.Configure(serviceCollection);
                    //SharedKernel.Infrastructure.DataStorage.CommonConfigurator.Configure(serviceCollection);
                    //UserManagement.ApplicationServices.CommonConfigurator.Configure(serviceCollection);

                    //CommonConfiguratorum.Configure(serviceCollection,hostContext.Configuration);
                    CommonConfiguratorum.Configure(services);
                });

    }
}
