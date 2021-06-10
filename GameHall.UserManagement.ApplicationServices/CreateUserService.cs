using System;
using System.Threading.Tasks;
using GameHall.SharedKernel.Core;
using GameHall.UserManagement.Domain.Model;

namespace GameHall.UserManagement.ApplicationServices
{
    public class CreateUserService : ICreateUserService
    {
        private readonly IRepository _repository;

        public CreateUserService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateUser(Guid userId, string userName)
        {
            var userEntity = _repository.GetById<UserEntity>(userId);
            if (userEntity != null) return;

            userEntity = new UserEntity(userName);
            _repository.Add(userEntity);
            await _repository.CommitAsync();
        }
    }
}