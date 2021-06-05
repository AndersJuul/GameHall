using GameHall.SharedKernel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace GameHall.FrontEnd.ApplicationServices
{
    public static class CommonConfigurator
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserManager, UserManager>();
        }
    }
}