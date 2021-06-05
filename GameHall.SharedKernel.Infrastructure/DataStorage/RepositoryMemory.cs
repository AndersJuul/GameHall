using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameHall.SharedKernel.Core;

namespace GameHall.SharedKernel.Infrastructure.DataStorage
{
    public class RepositoryMemory:IRepository
    {
        public T GetById<T>(Guid id) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public List<T> List<T>(ISpecification<T> spec = null) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public T Add<T>(T entity) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Attach<T>(T entity) where T : BaseEntity
        {
            throw new NotImplementedException();
        }
    }
}
