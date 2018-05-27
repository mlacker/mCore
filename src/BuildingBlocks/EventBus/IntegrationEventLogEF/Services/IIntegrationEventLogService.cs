using System.Data.Common;
using System.Threading.Tasks;
using mCore.BuildingBlocks.EventBus.Events;

namespace mCore.BuildingBlocks.IntegrationEventLogEF.Services
{
    public interface IIntegrationEventLogService
    {
        Task SaveEventAsync(IntegrationEvent @event, DbTransaction transaction);

        Task MarkEventAsPublishedAsync(IntegrationEvent @event);
    }
}
