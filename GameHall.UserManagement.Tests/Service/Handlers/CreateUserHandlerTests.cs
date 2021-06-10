using System;
using System.Threading.Tasks;
using AutoFixture;
using EasyNetQ;
using GameHall.SharedKernel.Core.Commands;
using GameHall.UserManagement.ApplicationServices;
using GameHall.UserManagement.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace GameHall.UserManagement.Tests.Service.Handlers
{
    public class CreateUserHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly ICreateUserService _createUserService;
        private readonly IServiceProvider _serviceProvider;

        public CreateUserHandlerTests()
        {
            _fixture = new Fixture();
            _createUserService = Substitute.For<ICreateUserService>();
            _serviceProvider = new ServiceCollection()
                .AddScoped(c => _createUserService)
                .BuildServiceProvider();
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange
            var createUser = _fixture.Create<CreateUser>();
            var sut = new CreateUserHandler(
                Substitute.For<ILogger<CreateUserHandler>>(),
                Substitute.For<IBus>(),
                _serviceProvider);

            // Act
            await sut.OnMessage(createUser);

            // Assert
            await _createUserService.Received(1).CreateUser(createUser.Id, createUser.Name);
        }
    }
}