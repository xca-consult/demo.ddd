using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.DDD.Domain;

namespace Integration.Database
{
    public interface IReaderRepository<TEntity>
       where TEntity : IAggregate
    {
        Task<TEntity> FindByIdAsync(Guid id);
    }

    public interface IWriteRepository<in TEntity>
        where TEntity : IAggregate
    {
        Task AddAsync(TEntity entity);
    }

    public interface IReaderWriterRepository<TEntity> : IReaderRepository<TEntity>, IWriteRepository<TEntity>
         where TEntity : IAggregate
    {
    }
}