using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using mCore.Application;
using mCore.Domain.Entities;
using mCore.Services.Process.Application.Definition.ViewModels;
using mCore.Services.Process.Core.Definition;

namespace mCore.Services.Process.Application.Definition
{
    public class ProcessAppService :ApplicationService
    {
        private readonly IProcessRepository processRepository;

        public ProcessAppService(
            IProcessRepository processRepository)
        {
            this.processRepository = processRepository;
        }

        public async Task<ProcessDefinitionViewModel> GetProcessDefinition(Guid id)
        {
            var process = await processRepository.GetAsync(id);

            return Mapper.Map<ProcessDefinitionViewModel>(process);
        }

        public async Task CreateProcessDefinition(ProcessDefinitionViewModel model)
        {
            var process = Mapper.Map<ProcessDefinition>(model);

            await processRepository.InsertAsync(process);
            await UnitOfWork.CommitAsync();
        }

        public async Task ModifyProcessDefinition(Guid id, ProcessDefinitionViewModel model)
        {
            var process = processRepository.Get(id);

            Mapper.Map(model, process);

            await processRepository.UpdateAsync(process);
            await UnitOfWork.CommitAsync();
        }

        public async Task DeleteProcessDefinition(Guid id)
        {
            await processRepository.DeleteAsync(id);
        }

        public async Task<PaginatedItems<ProcessDefinitionViewModel>> GetPaginatedList(PaginatedFilter filter)
        {
            var models = await processRepository.PaginatedList(filter);

            return Mapper.Map<PaginatedItems<ProcessDefinitionViewModel>>(models);
        }

        public Task<IEnumerable<Option>> CategoryOptions()
        {
            return processRepository.CategoryOptions();
        }
    }
}
