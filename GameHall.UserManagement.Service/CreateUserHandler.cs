using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using GameHall.SharedKernel.Core.Commands;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GameHall.UserManagement.Service
{
    public class CreateUserHandler : BackgroundService
    {
        private readonly IBus _bus;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(ILogger<CreateUserHandler> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var commandSubscriber = await _bus.PubSub.SubscribeAsync<CreateUser>("",
                msg => { }, stoppingToken);
            
            _logger.LogInformation("CreateUserHandler started at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("CreateUserHandler running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}