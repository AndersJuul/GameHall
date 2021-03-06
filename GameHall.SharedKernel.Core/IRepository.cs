using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHall.SharedKernel.Core
{
    public interface IRepository
    {
        T GetById<T>(Guid id) where T : BaseEntity;
        List<T> List<T>(ISpecification<T> spec = null) where T : BaseEntity;
        T Add<T>(T entity) where T : BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
        Task CommitAsync();
        void Attach<T>(T entity) where T : BaseEntity;
    }
}