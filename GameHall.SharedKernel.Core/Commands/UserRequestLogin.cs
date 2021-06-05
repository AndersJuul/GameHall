using System;

namespace GameHall.SharedKernel.Core.Commands
{
    public class UserRequestLogin : ICommand
    {
        public Guid SessionId { get; }
        public string UserName { get; }

        public UserRequestLogin(Guid sessionId, string userName)
        {
            SessionId = sessionId;
            UserName = userName;
        }
    }
}