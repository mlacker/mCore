using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using mCore.Domain.Entities;
using mCore.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace mCore.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity, IAggregateRoot
    {
        protected Repository(DbContext context)
        {
            Context = context;
            Entities = context.Set<TEntity>();
        }

        protected DbContext Context { get; }

        protected DbSet<TEntity> Entities { get; }

        public TEntity Get(Guid key)
        {
            return GetAsync(key).GetAwaiter().GetResult();
        }

        public Task<TEntity> GetAsync(Guid key)
        {
            return Entities.FindAsync(key);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            var query = Entities.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            return Task.FromResult(GetAll());
        }

        public Guid Insert(TEntity entity)
        {
            return InsertAsync(entity).GetAwaiter().GetResult();
        }

        public async Task<Guid> InsertAsync(TEntity entity)
        {
            await Entities.AddAsync(entity);

            return entity.Id;
        }

        public void InsertOrUpdate(TEntity entity)
        {
            InsertOrUpdateAsync(entity).RunSynchronously();
        }

        public Task InsertOrUpdateAsync(TEntity entity)
        {
            Entities.Attach(entity);

            return Task.CompletedTask;
        }

        public void Update(TEntity entity)
        {
            UpdateAsync(entity).RunSynchronously();
        }

        public Task UpdateAsync(TEntity entity)
        {
            Entities.Update(entity);

            return Task.CompletedTask;
        }

        public void Delete(Guid key)
        {
            DeleteAsync(key).RunSynchronously();
        }

        public async Task DeleteAsync(Guid key)
        {
            var entity = await GetAsync(key);

            if (entity is ISoftDelete)
            {
                (entity as ISoftDelete).IsDeleted = true;

                await UpdateAsync(entity);
            }
            else
            {
                Entities.Remove(entity);
            }
        }

        protected async Task<PaginatedItems<TEntity>> ToPagedListAsync(IOrderedQueryable<TEntity> query, PaginatedFilter filter)
        {
            return new PaginatedItems<TEntity>(
                await query.Skip(filter.Start).Take(filter.Size).ToListAsync(),
                await query.CountAsync());
        }
    }
}
