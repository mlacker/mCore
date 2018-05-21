using System.Collections.Generic;
using System.Threading.Tasks;
using mCore.Domain.Entities;
using mCore.Domain.Repositories;

namespace mCore.Services.Process.Core.Definition
{
    public interface IProcessRepository : IRepository<ProcessDefinition>
    {
        Task<PaginatedItems<ProcessDefinition>> PaginatedList(PaginatedFilter filter);

        Task<IEnumerable<Option>> CategoryOptions();
    }
}
