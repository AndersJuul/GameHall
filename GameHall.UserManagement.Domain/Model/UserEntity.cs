using GameHall.SharedKernel.Core;

namespace GameHall.UserManagement.Domain.Model
{
    public class UserEntity : BaseAggregateEntity
    {
        public UserEntity(string name)
        {
            Name = name;
            Events.Add(new UserCreatedEvent(this));
        }

        public string Name { get; set; }
        public override void AddEvent(BaseDomainEvent domainEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}