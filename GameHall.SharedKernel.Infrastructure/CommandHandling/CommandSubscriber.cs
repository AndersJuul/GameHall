using System;
using GameHall.SharedKernel.Core;
using GameHall.SharedKernel.Core.Commands;
using RabbitMQ.Client;
using RabbitMQ.Client.Core.DependencyInjection.Services;

namespace GameHall.SharedKernel.Infrastructure.CommandHandling
{
    public class CommandSubscriber : ICommandSubscriber<ICommand>
    {
        private readonly IQueueService _queueService;
        private readonly IConnection _connection;

        public CommandSubscriber(IQueueService queueService, IConnection connection)
        {
            _queueService = queueService;
            _connection = connection;

            //await _queueService.SendAsync(
            //    @object: command,
            //    exchangeName: "exchange.name",
            //    routingKey: "routing.key");
        }

        public void Register(Action<ICommand> action, object o)
        {
            _connection.CreateModel().QueueDeclare(o.GetType().FullName, true, true, false);
            throw new NotImplementedException();
        }
    }
}