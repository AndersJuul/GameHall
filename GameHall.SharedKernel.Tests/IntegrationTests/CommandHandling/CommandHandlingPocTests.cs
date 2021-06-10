using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyNetQ;
using GameHall.SharedKernel.Core.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using CommonConfigurator = GameHall.SharedKernel.Infrastructure.CommandHandling.CommonConfigurator;

namespace GameHall.SharedKernel.Tests.IntegrationTests.CommandHandling
{
    public class CommandHandlingPocTests
    {
        [Fact]
        public async Task ThatPublishedCommandsArePickedUpByConsumer()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json", false, true);
            var configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            CommonConfigurator.Configure(serviceCollection);
            SharedKernel.Core.CommonConfigurator.Configure(serviceCollection);
            SharedKernel.Infrastructure.CommandHandling.CommonConfigurator.Configure(serviceCollection);
            SharedKernel.Infrastructure.DataStorage.CommonConfigurator.Configure(serviceCollection);
            UserManagement.ApplicationServices.CommonConfiguratorxxxxxxx.Configure(serviceCollection);
            FrontEnd.Console.CommonConfigurator.Configure(serviceCollection,configuration);
            //serviceCollection.AddTransient<DummyCommandHandler>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var bus = serviceProvider.GetRequiredService<IBus>();

            var called = false;
            var commandSubscriber = await bus.PubSub.SubscribeAsync<DummyCommand>("",
                msg =>
                {
                    called = true;
                });

            await bus.PubSub.PublishAsync(new DummyCommand(Guid.NewGuid(), "anders"));
            //serviceProvider.GetRequiredService<ICapPublisher<DummyCommand>>();
            //commandSubscriber.Register(c =>
            //{
            //    Console.WriteLine("");
            //}, this);
            //var sub = new DummyCommandHandler();
            //await commandPublisher.PublishAsync(nameof(DummyCommand),new DummyCommand(Guid.NewGuid(), "anders"));

            //Assert.Single(sub.HandledCommands);
            await Task.Delay(4000);
            //Assert.True(called);
        }
    }

    //public class DummyCommandHandler:ICapSubscribe
    //{
    //    public DummyCommandHandler()
    //    {
    //        HandledCommands = new List<ICommand>();
    //    }

    //    [CapSubscribe(nameof(DummyCommand))]
    //    public void Handle(DummyCommand dummyCommand)
    //    {
    //        HandledCommands.Add(dummyCommand);
    //    }

    //    public List<ICommand> HandledCommands { get; set; }
    //}


    public class DummyCommand : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }

        public DummyCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
