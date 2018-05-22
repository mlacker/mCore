using System;
using System.Threading.Tasks;
using mCore.Domain.Uow;
using Microsoft.EntityFrameworkCore;

namespace mCore.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Rollback()
        {
            throw new ApplicationException("Rollback transaction.");
        }

        public async Task RollbackAsync()
        {
            Rollback();

            await Task.CompletedTask;
        }
    }
}
