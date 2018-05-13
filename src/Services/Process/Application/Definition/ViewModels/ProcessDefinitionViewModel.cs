using System;
using mCore.Application.ViewModels;

namespace mCore.Services.Process.Application.Definition.ViewModels
{
    public class ProcessDefinitionViewModel : IEntityViewModel
    {
        public Guid? Id { get; private set; }

        public string Name { get; private set; }

        public string Category { get; private set; }

        public int CurrentVersion { get; private set; }

        public string CreatedUser { get; private set; }

        public string CreatedTime { get; private set; }

        public string IsSuspended { get; private set; }
    }
}
