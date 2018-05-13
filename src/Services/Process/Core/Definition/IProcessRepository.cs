using System.Threading.Tasks;
using mCore.Application.ViewModels;
using mCore.Domain.Repositories;

namespace mCore.Services.Process.Core.Definition
{
    public interface IProcessRepository : IRepository<ProcessDefinition>
    {
        Task<PagedListViewModel<ProcessDefinition>> PagedList(PagedFilterViewModel filter);
    }
}
