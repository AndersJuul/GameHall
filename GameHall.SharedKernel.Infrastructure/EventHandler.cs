using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using GameHall.SharedKernel.Core.Commands;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GameHall.SharedKernel.Infrastructure
{
    public abstract class EventHandler<T> : BackgroundService where T :ICommand
    {
        protected readonly IBus Bus;
        protected readonly ILogger Logger;

        protected EventHandler(ILogger logger, IBus bus)
        {
            Bus = bus;
            Logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var s = typeof(T).FullName;

            var commandSubscriber = await Bus
                .PubSub
                .SubscribeAsync<T>("", OnMessage, stoppingToken);

            Logger.LogInformation(s+ " Handler started at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                Logger.LogInformation(s+ " Handler running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public abstract Task OnMessage(T msg);
    }
}