using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using mCore.BuildingBlocks.EventBus.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace mCore.BuildingBlocks.IntegrationEventLogEF.Services
{
    public class IntegrationEventLogService : IIntegrationEventLogService
    {
        private readonly IntegrationEventLogContext integrationEventLogContext;
        private readonly DbConnection dbConnection;

        public IntegrationEventLogService(DbConnection dbConnection)
        {
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            this.integrationEventLogContext = new IntegrationEventLogContext(
                new DbContextOptionsBuilder<IntegrationEventLogContext>()
                    .UseSqlServer(dbConnection)
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                    .Options);
        }

        public Task SaveEventAsync(IntegrationEvent @event, DbTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction), $"A {typeof(DbTransaction).FullName} is required as a pre-requisite to save the event.");
            }

            var eventLogEntry = new IntegrationEventLogEntry(@event);

            integrationEventLogContext.Database.UseTransaction(transaction);
            integrationEventLogContext.IntegrationEventLogs.Add(eventLogEntry);

            return integrationEventLogContext.SaveChangesAsync();
        }

        public Task MarkEventAsPublishedAsync(IntegrationEvent @event)
        {
            var eventLogEntry = integrationEventLogContext.IntegrationEventLogs.Single(ie => ie.EventId == @event.Id);
            eventLogEntry.TimesSent++;
            eventLogEntry.State = EventStateEnum.Published;

            integrationEventLogContext.IntegrationEventLogs.Update(eventLogEntry);

            return integrationEventLogContext.SaveChangesAsync();
        }
    }
}
