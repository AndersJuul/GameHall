using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

namespace GameHall.UserManagement.Service
{
    public static class CommonConfigurator
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHostedService<CreateUserHandler>();
            serviceCollection.AddSingleton<IBus>(c => RabbitHutch.CreateBus("host=localhost"));
        }
    }
}