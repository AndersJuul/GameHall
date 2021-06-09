using System.Collections.Generic;
using EasyNetQ;
using GameHall.SharedKernel.Core.Commands;
using Microsoft.Extensions.DependencyInjection;
using ICommand = System.Windows.Input.ICommand;

namespace GameHall.UserManagement.Service
{
    public static class CommonConfiguratorum
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHostedService<Worker>();
            serviceCollection.AddSingleton<IBus>(c => RabbitHutch.CreateBus("host=localhost"));
            //serviceCollection.AddTransient<CreateUserHandler>();
        }
    }

    //public class CreateUserHandler:ICapSubscribe
    //{
    //public CreateUserHandler()
    //{
    //    HandledCommands = new List<GameHall.SharedKernel.Core.Commands.ICommand>();
    //}

    //[CapSubscribe(nameof(CreateUser))]
    //public void Handle(CreateUser createUser)
    //{
    //    HandledCommands.Add(createUser);
    //}

    //public List<GameHall.SharedKernel.Core.Commands.ICommand> HandledCommands { get; set; }
    
    //}
}