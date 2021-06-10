using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using EasyNetQ;
using GameHall.FrontEnd.WebApi.Endpoints.User;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace GameHall.FrontEnd.Tests.WebApi.Endpoints.User
{
    public class CreateUserEndpointTests
    {
        private readonly Fixture _fixture;

        public CreateUserEndpointTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task HandleAsync_ValidRequest_Succeeds()
        {
            // Arrange
            var sut = new CreateUserEndpoint(Substitute.For<IBus>());
            var validCreateUserRequest = _fixture.Create<CreateUserRequest>();

            // Act
            var result = await sut.HandleAsync(validCreateUserRequest, CancellationToken.None);

            // Assert
            Assert.True(result.Result is OkObjectResult);
        }
    }
}