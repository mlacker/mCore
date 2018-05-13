using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using mCore.Application.ViewModels;
using mCore.Services.Process.Application.Definition.ViewModels;
using mCore.Services.Process.Core.Definition;

namespace mCore.Services.Process.Application.Definition
{
    public class ProcessAppService
    {
        private readonly IProcessRepository processRepository;

        public ProcessAppService(
            IMapper mapper,
            IProcessRepository processRepository)
        {
            Mapper = mapper;
            this.processRepository = processRepository;
        }

        public IMapper Mapper { get; }

        public async Task<PagedListViewModel<ProcessDefinitionViewModel>> GetProcessDefinitionList(PagedFilterViewModel filter)
        {
            var models = await processRepository.PagedList(filter);

            return Mapper.Map<PagedListViewModel<ProcessDefinitionViewModel>>(models);
        }
    }
}
