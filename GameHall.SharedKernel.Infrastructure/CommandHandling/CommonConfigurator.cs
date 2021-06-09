using GameHall.SharedKernel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace GameHall.SharedKernel.Infrastructure.CommandHandling
{
    public static class CommonConfigurator
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped<ICommandPublisher, CommandPublisher>();
            //serviceCollection.AddScoped<ICommandSubscriber, CommandSubscriber>();
        }
    }
}