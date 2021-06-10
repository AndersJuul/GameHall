﻿
using Microsoft.Extensions.DependencyInjection;

namespace GameHall.UserManagement.ApplicationServices
{
    public static class CommonConfiguratorxxxxxxx
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICreateUserService, CreateUserService>();
        }
    }
}