using System;
using GameHall.SharedKernel.Core.Commands;

namespace GameHall.SharedKernel.Core
{
    public interface ICommandSubscriber<T> where T : ICommand
    {
        void Register(Action<T> action, object o);
    }
}