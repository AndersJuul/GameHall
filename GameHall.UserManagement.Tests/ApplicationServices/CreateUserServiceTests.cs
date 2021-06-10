using System;
using System.Threading.Tasks;
using AutoFixture;
using GameHall.SharedKernel.Core;
using GameHall.UserManagement.ApplicationServices;
using GameHall.UserManagement.Domain.Model;
using NSubstitute;
using Xunit;

namespace GameHall.UserManagement.Tests.ApplicationServices
{
    public class CreateUserServiceTests
    {
        private readonly Fixture _fixture;

        public CreateUserServiceTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task CreateUser_Nonexisting_SucceedsAndCommits()
        {
            // Arrange
            var userId = _fixture.Create<Guid>();
            var userName = _fixture.Create<string>();
            var repository = Substitute.For<IRepository>();
            var sut = new CreateUserService(repository);

            // Act
            await sut.CreateUser(userId, userName);

            // Assert
            repository.Received(1).GetById<UserEntity>(userId);
            repository.Received(1).Add(Arg.Any<UserEntity>());
            await repository.Received(1).CommitAsync();
        }
    }
}