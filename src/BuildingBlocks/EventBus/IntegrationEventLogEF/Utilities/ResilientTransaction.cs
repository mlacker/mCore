using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace mCore.BuildingBlocks.IntegrationEventLogEF.Utilities
{
    public class ResilientTransaction
    {
        private DbContext context;

        private ResilientTransaction(DbContext context) =>
            context = context ?? throw new ArgumentNullException(nameof(context));

        public static ResilientTransaction New(DbContext context) => new ResilientTransaction(context);

        public Task ExecuteAsync(Func<Task> action)
        {
            // Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction();
            // See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
            var strategy = context.Database.CreateExecutionStrategy();
            return strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    await action();
                    transaction.Commit();
                }
            });
        }
    }
}
