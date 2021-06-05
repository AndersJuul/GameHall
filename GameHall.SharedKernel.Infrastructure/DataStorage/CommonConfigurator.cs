using GameHall.SharedKernel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace GameHall.SharedKernel.Infrastructure.DataStorage
{
    public static class CommonConfigurator
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRepository, RepositoryMemory>();
        }
    }
}