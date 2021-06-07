using GameHall.SharedKernel.Core;
using RabbitMQ.Client.Core.DependencyInjection.Services;

namespace GameHall.SharedKernel.Infrastructure.CommandHandling
{
    public class CommandSubscriber : ICommandSubscriber
    {
        private readonly IQueueService _queueService;

        public CommandSubscriber(IQueueService queueService)
        {
            _queueService = queueService;

            //await _queueService.SendAsync(
            //    @object: command,
            //    exchangeName: "exchange.name",
            //    routingKey: "routing.key");
        }
    }
}