using Microsoft.Extensions.DependencyInjection;

namespace GameHall.SharedKernel.Core
{
    public static class CommonConfigurator
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped<ICommandPublisher, CommandPublisher>();
        }
    }
}