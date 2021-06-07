using System;

namespace GameHall.SharedKernel.Core.Commands
{
    public class CreateUser : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }

        public CreateUser(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}