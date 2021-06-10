using GameHall.SharedKernel.Core;

namespace GameHall.UserManagement.Domain.Model
{
    public class UserCreatedEvent : BaseDomainEvent
    {
        public UserEntity UserEntity { get; }

        public UserCreatedEvent(UserEntity userEntity)
        {
            UserEntity = userEntity;
        }
    }
}