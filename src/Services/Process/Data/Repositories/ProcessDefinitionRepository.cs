using System.Linq;
using System.Threading.Tasks;
using mCore.Application.ViewModels;
using mCore.Data.Repositories;
using mCore.Services.Process.Core.Definition;

namespace mCore.Services.Process.Data.Repositories
{
    public class ProcessDefinitionRepository : Repository<ProcessDefinition>, IProcessRepository
    {
        public ProcessDefinitionRepository(ApplicationDbContext context): base(context)
        {
        }

        public async Task<PagedListViewModel<ProcessDefinition>> PagedList(PagedFilterViewModel filter)
        {
            var query = await GetAllAsync();

            if (filter.ContainsKey("searchString"))
            {
                query = query.Where(m => m.Name.Contains(filter["searchString"]));
            }

            if (filter.ContainsKey("categroy"))
            {
                query = query.Where(m => m.Category == filter["Category"]);
            }

            return await ToPagedListAsync(query.OrderByDescending(m => m.Id), filter);
        }
    }
}
