using System;
using GameHall.SharedKernel.Core;
using GameHall.SharedKernel.Core.Commands;
using GameHall.SharedKernel.Infrastructure.CommandHandling;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using CommonConfigurator = GameHall.SharedKernel.Infrastructure.CommandHandling.CommonConfigurator;

namespace GameHall.SharedKernel.Tests.IntegrationTests.CommandHandling
{
    public class CommandHandlingPocTests
    {
        [Fact]
        public void ThatPublishedCommandsArePickedUpByConsumer()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json", false, true);
            var configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            CommonConfigurator.Configure(serviceCollection);
            SharedKernel.Core.CommonConfigurator.Configure(serviceCollection);
            SharedKernel.Infrastructure.CommandHandling.CommonConfigurator.Configure(serviceCollection);
            SharedKernel.Infrastructure.DataStorage.CommonConfigurator.Configure(serviceCollection);
            UserManagement.ApplicationServices.CommonConfigurator.Configure(serviceCollection);
            FrontEnd.Console.CommonConfigurator.Configure(serviceCollection,configuration);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var commandPublisher = serviceProvider.GetRequiredService<ICommandPublisher>();

            var commandSubscriber = serviceProvider.GetRequiredService<ICommandSubscriber>();

            commandPublisher.Publish(new CreateUser(Guid.NewGuid(), "anders"));


        }
    }
}
