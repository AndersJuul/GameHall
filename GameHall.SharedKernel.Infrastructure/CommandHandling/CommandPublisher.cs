using System.Threading.Tasks;
using GameHall.SharedKernel.Core;
using GameHall.SharedKernel.Core.Commands;
using RabbitMQ.Client.Core.DependencyInjection.Services;

namespace GameHall.SharedKernel.Infrastructure.CommandHandling
{
    public class CommandPublisher : ICommandPublisher
    {
        private readonly IQueueService _queueService;

        public CommandPublisher(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public async Task Publish(ICommand command)
        {
            await _queueService.SendAsync(
                @object: command,
                exchangeName: "exchange.name",
                routingKey: "routing.key");
        }
    }
}