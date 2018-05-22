using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mCore.Data.Repositories;
using mCore.Domain.Entities;
using mCore.Services.Process.Core.Definition;
using Microsoft.EntityFrameworkCore;

namespace mCore.Services.Process.Data.Repositories
{
    public class ProcessDefinitionRepository : Repository<ProcessDefinition>, IProcessRepository
    {
        public ProcessDefinitionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PaginatedItems<ProcessDefinition>> PaginatedList(PaginatedFilter filter)
        {
            var query = GetAll();

            if (filter.ContainsKey("searchString"))
            {
                query = query.Where(m => m.Name.Contains(filter["searchString"]));
            }

            if (filter.ContainsKey("category"))
            {
                query = query.Where(m => m.Category == filter["category"]);
            }

            return await ToPagedListAsync(query.OrderByDescending(m => m.Id), filter);
        }

        public async Task<IEnumerable<Option>> CategoryOptions()
        {
            return await GetAll().Select(m => new Option(m.Category)).Distinct().ToListAsync();
        }
    }
}
