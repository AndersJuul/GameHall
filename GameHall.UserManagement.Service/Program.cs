using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GameHall.UserManagement.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var serviceCollection = new ServiceCollection();

                    SharedKernel.Core.CommonConfigurator.Configure(serviceCollection);
                    ApplicationServices.CommonConfigurator.Configure(serviceCollection);

                    CommonConfigurator.Configure(services);
                });
        }
    }
}