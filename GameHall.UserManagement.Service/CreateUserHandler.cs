using System;
using System.Threading.Tasks;
using EasyNetQ;
using GameHall.SharedKernel.Core.Commands;
using GameHall.UserManagement.ApplicationServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameHall.UserManagement.Service
{
    public class CreateUserHandler : SharedKernel.Infrastructure.EventHandler<CreateUser>
    {
        private readonly IServiceProvider _serviceProvider;

        public CreateUserHandler(ILogger<CreateUserHandler> logger, IBus bus, IServiceProvider serviceProvider) :
            base(logger, bus)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task OnMessage(CreateUser msg)
        {
            Logger.LogInformation("Handling message: {msg}",msg);
            var createUserService = _serviceProvider.GetRequiredService<ICreateUserService>();
            await createUserService.CreateUser(msg.Id, msg.Name);
        }
    }
}