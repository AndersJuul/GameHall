using System.Threading.Tasks;
using GameHall.SharedKernel.Core;
using GameHall.SharedKernel.Core.Commands;
using RabbitMQ.Client;
using RabbitMQ.Client.Core.DependencyInjection.Services;

namespace GameHall.SharedKernel.Infrastructure.CommandHandling
{
    public class CommandPublisher : ICommandPublisher
    {
        private readonly IQueueService _queueService;
        private readonly IConnection _connection;

        public CommandPublisher(IQueueService queueService, IConnection connection)
        {
            _queueService = queueService;
            _connection = connection;
        }

        public async Task Publish(ICommand command)
        {
            var fullName = command.GetType().FullName;
            _connection.CreateModel().ExchangeDeclare(fullName, ExchangeType.Fanout,true, false );
            await _queueService.SendAsync(
                @object: command,
                exchangeName: fullName,
                routingKey: "routing.key");
        }
    }
}