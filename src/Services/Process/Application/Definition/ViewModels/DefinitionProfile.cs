using AutoMapper;
using mCore.Services.Process.Core.Definition;

namespace mCore.Services.Process.Application.Definition.ViewModels
{
    public class DefinitionProfile : Profile
    {
        public DefinitionProfile()
        {
            CreateMap<ProcessDefinition, ProcessDefinitionViewModel>();
        }
    }
}
