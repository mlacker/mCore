namespace mCore.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Linq.Expressions;
    using mCore.Domain.Entities;

    public interface IRepository<TEntity> 
        where TEntity : IEntity, IAggregateRoot
    {
        IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<IQueryable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        TEntity Get(Guid key);

        Task<TEntity> GetAsync(Guid key);

        Guid Insert(TEntity entity);

        Task<Guid> InsertAsync(TEntity entity);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);

        void InsertOrUpdate(TEntity entity);

        Task InsertOrUpdateAsync(TEntity entity);

        void Delete(Guid key);

        Task DeleteAsync(Guid key);
    }
}
