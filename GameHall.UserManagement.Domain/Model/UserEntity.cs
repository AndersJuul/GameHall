using GameHall.SharedKernel.Core;

namespace GameHall.UserManagement.Domain.Model
{
    public class UserEntity : BaseEntity
    {
        public UserEntity(string name)
        {
            Name = name;
            Events.Add(new UserCreatedEvent(this));
        }

        public string Name { get; set; }
    }
}