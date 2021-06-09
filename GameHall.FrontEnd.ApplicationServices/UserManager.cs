using System;
using System.Threading.Tasks;
using GameHall.SharedKernel.Core;

namespace GameHall.FrontEnd.ApplicationServices
{
    public class UserManager:IUserManager
    {
        private readonly IRepository _repository;

        public UserManager(IRepository repository )
        {
            _repository = repository;
        }

        public async Task Login(Guid sessionId, string userName)
        {
            var user = _repository.GetById<User>(Guid.Empty);
            await user.Login(sessionId);
            await _repository.CommitAsync();

        }
    }

    public interface IUserManager
    {
    }

    public class User:BaseEntity
    {
        public Task Login(Guid sessionId)
        {
            return Task.CompletedTask;
        }
    }

    public class UserLoggedInDomainEvent : BaseDomainEvent
    {
        public Guid UserId { get; }
        public Guid SessionId { get; }

        public UserLoggedInDomainEvent(Guid userId, Guid sessionId)
        {
            UserId = userId;
            SessionId = sessionId;
        }
    }
}
