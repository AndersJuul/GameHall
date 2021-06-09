using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

namespace GameHall.FrontEnd.WebApi
{
    public static class CommonConfigurator
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IBus>(c => RabbitHutch.CreateBus("host=localhost"));
        }
    }
}