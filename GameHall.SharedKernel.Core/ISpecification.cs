using System;
using System.Linq.Expressions;

namespace GameHall.SharedKernel.Core
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}